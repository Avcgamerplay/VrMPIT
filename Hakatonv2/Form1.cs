using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hakatonv2
{
    public partial class Form1 : Form
    {
        int sudId = 0;

        public List<Sud> suds = new List<Sud>();
        public Form1(Patient patient)
        {
            NetworkManager.StartServer();

            InitializeComponent();

            patient_name_label.Text = $"Пациент: {patient.FirstName}";

            //ShowMainPanel();

            //NetworkManager.StartServer();
            NetworkManager.onStringEvent += ProcessIncomingMessage;

            NetworkManager.onConnectionActivated += () =>
            {

            };

            NetworkManager.onConnectionDeleted += () =>
            {
                UpdateStatus("Quest 3 отключен");

                /*Invoke(new Action(() =>
                {
                    UpdateStatus("Quest 3 отключен");
                    //ShowMainPanel();
                    //ShowWaitingPanel();
                }));*/
            };

            NetworkManager.onConnectionWait += () =>
            {
                UpdateStatus("ожидаем подключения");
            };

            InitializeTable();
            LoadData();

            InitializeChart();
            AddDataToChart();
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
                    case "connected":
                        UpdateStatus("Quest 3 подключен!");
                        quest_status_test.Text = "Устройство подклчено";
                        ShowMainPanel();
                        break;

                    case "all_scene":
                        for(int i = 1; i < parts.Length; i++)
                        {
                            Scene newScene = new Scene();
                            newScene.Name = parts[i];

                            SceneManager.scenesOnQuest.Add(newScene);
                        }
                        break;

                    case "scene_not_added":
                        break;

                    case "scene_loading":
                        stream_button.Enabled = true;
                        pause_button.Enabled = true;
                        start_bp_button.Enabled = false;
                        start_emdr_button.Enabled = false;
                        break;

                    case "paused":
                        pause_button.Text = "Продолжить";
                        break;

                    case "unpaused":
                        pause_button.Text = "Пауза";
                        break;

                    case "sud":
                        start_bp_button.Enabled = true;
                        start_emdr_button.Enabled = true;
                        string sudLvl = parts[1];
                        string sceneMane = parts[2];
                        string volumeEffects = parts[3];
                        Sud newSud = new Sud();
                        newSud.sudLvl = sudLvl;
                        newSud.sceneName = sceneMane;
                        newSud.volume = volumeEffects;
                        newSud.sudId = sudId;

                        suds.Add(newSud);

                        LoadData();
                        AddDataToChart();

                        sudId++;
                        break;

                    case "bp_choose":
                        start_bp_button.Text = "Вернуться на сцену";
                        start_emdr_button.Enabled = false;
                        break;

                    case "emdrStart":
                        start_emdr_button.Text = "Вернуться на сцену";
                        start_bp_button.Text = "Безопасное место";
                        start_bp_button.Enabled = true;
                        break;

                        /*case "sud":
                            if (parts.Length >= 3)
                            {
                                string sceneType = parts[1];
                                int sudValue = int.Parse(parts[2]);
                                AddSUDRecord(sceneType, sudValue);
                            }
                            break;

                        case "paused":
                            btnPauseResume.Text = "Продолжить";
                            break;

                        case "resumed":
                            btnPauseResume.Text = "Пауза";
                            break;

                        case "scene_loaded":
                            if (parts.Length >= 2)
                            {
                                UpdateStatus($"Сцена загружена: {parts[1]}");
                            }
                            break;

                        case "session_ended":
                            EndSession();
                            break;*/
                }
            }));
        }

        private void UpdateStatus(string status)
        {
            //lblStatus.Text = status;
            if (status.Contains("подключен"))
            {
                lblStatus.ForeColor = Color.Green;
            }
            else if (status.Contains("ожидаем"))
            {
                lblStatus.ForeColor = Color.Orange;
            }
            else
            {
                lblStatus.ForeColor = Color.Black;
            }
        }

        void ShowMainPanel()
        {
            main_panel.Visible = true;
            base_panel.Visible = false;
        }

        void ShowLoginPanel()
        {
            main_panel.Visible = false;
            base_panel.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void main_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_control_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_open_choose_scene_Click(object sender, EventArgs e)
        {
            //if (NetworkManager.IsConnected() == false) return;

            SceneManager.StartSceneManager();

            /*if (client == null || !client.Connected)
            {
                MessageBox.Show("Нет подключения к Quest 3!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            FormSceneSelection sceneForm = new FormSceneSelection();
            if (sceneForm.ShowDialog() == DialogResult.OK)
            {
                Scene selectedScene = sceneForm.SelectedScene;
                already_scene_name.Text = selectedScene.Name;
                //btnStartScene.Enabled = !string.IsNullOrEmpty(txtPatientID.Text);

                // Отправляем команду на загрузку выбранной сцены
                NetworkManager.SendMessage($"load_scene|{selectedScene.Name}");
            }
        }

        private void SendAvailableScenes()
        {
            string sceneList = "scenes_list";
            foreach (var scene in SceneManager.scenes)
            {
                sceneList += $"|{scene.FileName}|{scene.Duration}|{scene.FileSize}";
            }
            NetworkManager.SendMessage(sceneList);
        }

        private void UpdateSceneAvailability(string fileName, bool available)
        {
            var scene = SceneManager.scenes.FirstOrDefault(s => s.FileName == fileName);
            if (scene != null)
            {
                scene.IsAvailableOnQuest = available;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage("pause");
        }

        private void start_bp_button_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage("go_bp");
        }

        private void start_emdr_button_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage("go_emdr");
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage("stop");
        }

        private void AddTableHeaders()
        {
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            string[] headers = { "SUD ID", "SUD", "Сцена", "Яркость", "Громкость", "?" };

            for (int i = 0; i < headers.Length; i++)
            {
                var headerLabel = new Label
                {
                    Text = headers[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                    BackColor = Color.LightGray
                };

                tableLayoutPanel1.Controls.Add(headerLabel, i, 0);
            }
        }

        private void InitializeTable()
        {
            // Очищаем таблицу
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            // Настраиваем колонки
            tableLayoutPanel1.ColumnCount = 6; // 5 текстов + 1 картинка

            // Ширины колонок (настройте под ваши нужды)
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));  // ID
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));  // Text1
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));  // Text2
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));  // Text3
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));  // Text4
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40)); // Image

            // Начальное количество строк
            tableLayoutPanel1.RowCount = 0;

            // Дополнительные настройки
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        }

        private void LoadData()
        {
            // Очищаем существующие данные (кроме заголовков если есть)
            ClearTableData();

            AddTableHeaders();

            for(int i = 1; i < suds.Count + 1; i++)
            {
                AddTableRow(
                    id: i.ToString(),
                    text1: $"{suds[i-1].sudId}",
                    text2: $"{suds[i-1].sceneName}",
                    text3: $"{suds[i-1].volume}",
                    text4: $"{1}",
                    image: SystemIcons.Information.ToBitmap(),
                    tooltip: $"Подробная информация для строки {i}"
                );
            }

        }

        private void AddTableRow(string id, string text1, string text2, string text3, string text4, Image image, string tooltip)
        {
            int rowIndex = tableLayoutPanel1.RowCount;

            // Добавляем новую строку
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            // Создаем элементы для каждой ячейки
            var lblID = CreateLabel(id, ContentAlignment.MiddleLeft);
            var lblText1 = CreateLabel(text1, ContentAlignment.MiddleLeft);
            var lblText2 = CreateLabel(text2, ContentAlignment.MiddleLeft);
            var lblText3 = CreateLabel(text3, ContentAlignment.MiddleLeft);
            var lblText4 = CreateLabel(text4, ContentAlignment.MiddleLeft);
            var picIcon = CreatePictureBox(image, tooltip, rowIndex);

            // Добавляем элементы в таблицу
            tableLayoutPanel1.Controls.Add(lblID, 0, rowIndex);
            tableLayoutPanel1.Controls.Add(lblText1, 1, rowIndex);
            tableLayoutPanel1.Controls.Add(lblText2, 2, rowIndex);
            tableLayoutPanel1.Controls.Add(lblText3, 3, rowIndex);
            tableLayoutPanel1.Controls.Add(lblText4, 4, rowIndex);
            tableLayoutPanel1.Controls.Add(picIcon, 5, rowIndex);
        }

        private Label CreateLabel(string text, ContentAlignment alignment)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = alignment,
                Margin = new Padding(3),
                AutoSize = false
            };
        }

        private PictureBox CreatePictureBox(Image image, string tooltip, int rowIndex)
        {
            var pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = rowIndex // Сохраняем индекс строки
            };

            // ToolTip
            var toolTip = new ToolTip();
            toolTip.SetToolTip(pictureBox, tooltip);

            // Обработчик клика
            pictureBox.Click += (s, e) =>
            {
                var pic = s as PictureBox;
                int clickedRow = (int)pic.Tag;
                MessageBox.Show($"Клик по картинке в строке {clickedRow + 1}");
            };

            return pictureBox;
        }

        private void ClearTableData()
        {
            // Удаляем все строки кроме заголовков (если они есть)
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.RowStyles.Clear();

            // Очищаем все контролы
            tableLayoutPanel1.Controls.Clear();
        }

        // Метод для обновления конкретной строки
        public void UpdateRow(int rowIndex, string text1, string text2, string text3, string text4)
        {
            if (rowIndex >= 0 && rowIndex < tableLayoutPanel1.RowCount)
            {
                // Получаем контролы из строки
                var label1 = tableLayoutPanel1.GetControlFromPosition(1, rowIndex) as Label;
                var label2 = tableLayoutPanel1.GetControlFromPosition(2, rowIndex) as Label;
                var label3 = tableLayoutPanel1.GetControlFromPosition(3, rowIndex) as Label;
                var label4 = tableLayoutPanel1.GetControlFromPosition(4, rowIndex) as Label;

                if (label1 != null) label1.Text = text1;
                if (label2 != null) label2.Text = text2;
                if (label3 != null) label3.Text = text3;
                if (label4 != null) label4.Text = text4;
            }
        }

        // Метод для удаления строки
        public void RemoveRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < tableLayoutPanel1.RowCount)
            {
                // Удаляем все контролы из строки
                for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++)
                {
                    var control = tableLayoutPanel1.GetControlFromPosition(col, rowIndex);
                    if (control != null)
                    {
                        tableLayoutPanel1.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                // Сдвигаем все строки ниже удаленной
                for (int row = rowIndex + 1; row < tableLayoutPanel1.RowCount; row++)
                {
                    for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++)
                    {
                        var control = tableLayoutPanel1.GetControlFromPosition(col, row);
                        if (control != null)
                        {
                            tableLayoutPanel1.SetRow(control, row - 1);
                        }
                    }
                }

                tableLayoutPanel1.RowCount--;
            }

        }

        private void InitializeChart()
        {
            // Очищаем существующие series
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Создаем область графика
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Настраиваем оси
            chartArea.AxisX.Title = "Время";
            chartArea.AxisY.Title = "SUD";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 10;
            chartArea.AxisY.Interval = 1; // Шаг сетки

            // Создаем series для кривой
            Series series = new Series();
            series.ChartType = SeriesChartType.Line; // Тип - линейный график
            series.Name = "Показатели SUD";
            series.Color = Color.Blue;
            series.BorderWidth = 2;
            /*series.MarkerStyle = MarkerStyle.Circle; // Маркеры в точках данных
            series.MarkerSize = 2;
            series.MarkerColor = Color.Red;*/

            chart1.Series.Add(series);

            // Заголовок графика
            chart1.Titles.Clear();
            chart1.Titles.Add("Мой график");
            chart1.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
        }

        private void AddDataToChart()
        {
            if (chart1.Series.Count == 0) return;

            Series series = chart1.Series[0];
            series.Points.Clear();

            for(int i = 0; i < suds.Count; i++)
            {
                series.Points.AddXY(i * 0.1f, suds[i].sudLvl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormShowStream saveDialog = new FormShowStream();

            saveDialog.FormClosed += (s, args) =>
            {
                Debug.WriteLine("Новая форма закрыта");
                saveDialog.Dispose(); // Освобождаем ресурсы при закрытии
            };

            saveDialog.Show(); // Форма будет существовать пока не закроется
        }

        private void effewcts_button_Click(object sender, EventArgs e)
        {
            FormSoundEdit saveDialog = new FormSoundEdit();

            saveDialog.FormClosed += (s, args) =>
            {
                Debug.WriteLine("Новая форма закрыта");
                saveDialog.Dispose();
            };

            saveDialog.Show();
        }
    }

    public class Sud
    {
        public int sudId;
        public string volume;
        public string sudLvl;
        public string sceneName;
    }
}
