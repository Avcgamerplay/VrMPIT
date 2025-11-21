using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;
using LibVLCSharp.Shared;
using System.Collections;


namespace Hakatonv2
{
    public partial class FormSceneSelection : Form
    {
        //private TableLayoutPanel tableLayoutPanel;
        public Scene SelectedScene { get; private set; }

        private static FileSender fileSender;

        bool msgGetted;

        public FormSceneSelection()
        {
            //fileSender = new FileSender();
            InitializeComponent();

            NetworkManager.onStringEvent += ProcessIncomingMessage;

            // Запрашиваем список сцен с Quest 3

            RequestQuestScenes();

            //fileSender.StartFileSender();
        }

        private void RequestQuestScenes()
        {
            NetworkManager.SendMessage("get_all_scene");
        }

        private void ProcessIncomingMessage(string[] message)
        {
            Console.WriteLine($"Получено: {message[0]}");

            string[] parts = message[0].Split('|');
            string command = parts[0];

            Invoke(new Action(() =>
            {
                switch (command)
                {
                    case "all_scene":
                        for (int i = 1; i < parts.Length; i++)
                        {
                            Scene newScene = new Scene();
                            newScene.Name = parts[i];

                            for(int j = 0; j < SceneManager.scenes.Count; j++)
                            {
                                if (SceneManager.scenes[j].Name == newScene.Name)
                                {
                                    SceneManager.scenes[j].IsAvailableOnQuest = true;
                                }
                            }

                            SceneManager.scenesOnQuest.Add(newScene);
                        }
                        msgGetted = true;
                        InitializeGrid();
                        break;
                }
            }));
        }

        private void InitializeGrid()
        {
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.ColumnStyles.Clear();

            // Настройка сетки - 3 колонки
            tableLayoutPanel.ColumnCount = 3;
            for (int i = 0; i < 3; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            }

            // Вычисляем количество строк: делим количество сцен на 3 и округляем вверх
            int totalRows = (int)Math.Ceiling(SceneManager.scenes.Count / 3.0);
            tableLayoutPanel.RowCount = totalRows;

            // Добавляем стили строк: AutoSize для того, чтобы строка подстраивалась под содержимое
            for (int i = 0; i < totalRows; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            // Добавляем сцены
            for (int i = 0; i < SceneManager.scenes.Count; i++)
            {
                Scene scene = SceneManager.scenes[i];
                int row = i / 3;
                int column = i % 3;
                AddSceneToGrid(scene, row, column);
            }
        }

        private void RefreshGrid()
        {
            InitializeGrid();
        }

        private void AddSceneToGrid(Scene scene, int row, int column)
        {
            // Создаем панель для сцены - ВАЖНО: убираем Dock.Fill!
            Panel scenePanel = new Panel
            {
                // Убираем DockStyle.Fill - он мешает правильному определению размера
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                // Явно задаем размер панели
                Size = new Size(220, 250) // Подберите под ваш контент
            };

            // Превью
            PictureBox preview = new PictureBox
            {
                Size = new Size(200, 120),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = scene.PreviewImage,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Название
            Label nameLabel = new Label
            {
                Text = scene.Name,
                Location = new Point(10, 140),
                Size = new Size(200, 20),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Длительность
            Label durationLabel = new Label
            {
                Text = $"Длительность: {scene.Duration}",
                Location = new Point(10, 165),
                Size = new Size(200, 15),
                Font = new Font("Microsoft Sans Serif", 8),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Статус доступности на Quest
            Label statusLabel = new Label
            {
                Text = scene.IsAvailableOnQuest ? "✓ Доступна на Quest" : "⏳ Загрузка...",
                Location = new Point(10, 185),
                Size = new Size(200, 15),
                Font = new Font("Microsoft Sans Serif", 7),
                ForeColor = scene.IsAvailableOnQuest ? Color.Green : Color.Orange,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Кнопка запуска
            Button playButton = new Button
            {
                Text = "Запустить",
                Location = new Point(10, 205),
                Size = new Size(200, 30),
                Tag = scene,
                Enabled = scene.IsAvailableOnQuest
            };

            playButton.Click += PlayButton_Click;

            scenePanel.Controls.AddRange(new Control[]
            {
                preview, nameLabel, durationLabel, statusLabel, playButton
            });

            // Добавляем в таблицу
            tableLayoutPanel.Controls.Add(scenePanel, column, row);

            Debug.WriteLine($"Добавили сцену {scene.Name} в [{row}, {column}]");
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("кликнули");

            Button button = (Button)sender;
            SelectedScene = (Scene)button.Tag;

            if (SelectedScene.IsAvailableOnQuest)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Сцена еще не загружена на Quest 3. Дождитесь завершения загрузки.",
                              "Сцена не готова", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UploadSceneToQuest(Scene scene)
        {
            try
            {
                // Создаем папку для сцен, если не существует
                if (!Directory.Exists(SceneManager.scenesPath))
                    Directory.CreateDirectory(SceneManager.scenesPath);

                // Копируем файл в папку сцен
                string destinationPath = Path.Combine(SceneManager.scenesPath, scene.FileName);
                if (!File.Exists(destinationPath))
                {
                    File.Copy(scene.FilePath, destinationPath, true);
                    scene.FilePath = destinationPath;
                }

                // Отправляем команду на загрузку
                string uploadCommand = $"upload_scene|{scene.FileName}|{scene.FileSize}";
                NetworkManager.SendMessage(uploadCommand);

                // Запускаем поток для отправки файла
                Thread uploadThread = new Thread(() => SendSceneFile(scene));
                uploadThread.IsBackground = true;
                uploadThread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подготовке загрузки: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendSceneFile(Scene scene)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(scene.FilePath))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    long totalBytesSent = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        NetworkManager.stream.Write(buffer, 0, bytesRead);
                        totalBytesSent += bytesRead;

                        // Обновляем прогресс в UI
                        UpdateUploadProgress(scene, totalBytesSent, scene.FileSize);
                    }
                }

                // Файл отправлен
                NetworkManager.SendMessage($"upload_complete|{scene.FileName}");

                // Обновляем статус сцены
                scene.IsAvailableOnQuest = true;
                Invoke(new Action(() => RefreshGrid()));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки файла: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUploadProgress(Scene scene, long sent, long total)
        {
            // Можно добавить отображение прогресса загрузки
            float progress = (float)sent / total;
            Console.WriteLine($"Uploading {scene.FileName}: {progress:P1}");
        }

        private string GetVideoDuration(string filePath)
        {
            // В реальном приложении используйте библиотеку для получения длительности
            try
            {
                // Заглушка - в реальности получите длительность через FFmpeg
                return "5:00";
            }
            catch
            {
                return "Неизвестно";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fileSender?.Dispose();
        }

        private async void upload_scene_button_Click(object sender, EventArgs e)
        {

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Video files (*.mp4;*.avi;*.mov;*.wmv)|*.mp4;*.avi;*.mov;*.wmv|All files (*.*)|*.*";
            openDialog.Title = "Выберите 360 видео файл";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                FileInfo fileInfo = new FileInfo(filePath);

                foreach(Scene scene in SceneManager.scenes)
                {
                    if(scene.Name.ToLower() == fileName.ToLower())
                    {
                        MessageBox.Show("Сцена с таким именем уже существует!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Создаем новую сцену
                Scene newScene = new Scene
                {
                    Name = Path.GetFileNameWithoutExtension(fileName),
                    Duration = GetVideoDuration(filePath),
                    FileName = fileName,
                    FilePath = filePath,
                    FileSize = fileInfo.Length,
                    PreviewImage = null,
                    IsAvailableOnQuest = false,
                };

                // Добавляем в список
                SceneManager.scenes.Add(newScene);

                // Обновляем сетку
                RefreshGrid();

                SceneManager.SaveLocalScene(newScene);

                // Запускаем отправку на Quest 3
                //UploadSceneToQuest(newScene);

                fileSender.SendVideoFile(filePath);
            }
        }
    }
}
