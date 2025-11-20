using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Hakatonv2
{
    public static class SceneManager
    {
        public static string scenesPath => Path.Combine(Application.StartupPath, "Scenes");
        public static List<Scene> scenes = new List<Scene>();

        public static void StartSceneManager()
        {
            LoadLocalScenes();
        }

        private static string ReadTextFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd(); // Читаем весь файл
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}");
                return string.Empty;
            }
        }

        private static void CopyVideoFile(string sourcePath, string destinationDirectory)
        {
            try
            {
                // Создаем директорию назначения, если её нет
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                // Формируем полный путь назначения
                string fileName = Path.GetFileName(sourcePath);
                string destinationPath = Path.Combine(destinationDirectory, fileName);

                // Копируем файл (перезаписываем если существует)
                File.Copy(sourcePath, destinationPath, true);

                MessageBox.Show($"Файл скопирован в: {destinationPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка копирования: {ex.Message}");
            }
        }

        public static void SaveLocalScene(Scene scene)
        {
            if (!Directory.Exists(scenesPath))
                Directory.CreateDirectory(scenesPath);


            string newPath = scenesPath + "/" + scenes.Count.ToString();

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            using (StreamWriter writer = new StreamWriter(newPath + "/" + "data.txt", false, Encoding.UTF8))
            {
                writer.WriteLine($"{scene.Name}|{"Локальная сцена"}|{scene.Duration}");
            }

            CopyVideoFile(scene.FilePath, newPath + "/");
        }

        public static void LoadLocalScenes()
        {
            scenes.Clear();
            if (!Directory.Exists(scenesPath))
                Directory.CreateDirectory(scenesPath);

            string[] directories = Directory.GetDirectories(scenesPath);

            Debug.WriteLine(scenesPath);

            foreach (string dirPath in directories)
            {
                string[] dataFile = ReadTextFile(dirPath + "/" + "data.txt").Split('|');
                Scene loadedScene = new Scene();
                loadedScene.Name = dataFile[0];
                loadedScene.Duration = dataFile[2];

                string[] videoFiles = Directory.GetFiles(dirPath, "*.*")
                .Where(f => f.ToLower().EndsWith(".mp4") ||
                           f.ToLower().EndsWith(".mov") ||
                           f.ToLower().EndsWith(".avi"))
                .ToArray();
                Debug.WriteLine(videoFiles.Length);
               // FileInfo info = new FileInfo(videoFiles[0]);

                //loadedScene.FilePath = videoFiles[0];
                //loadedScene.FileSize = info.Length;
                loadedScene.IsAvailableOnQuest = false;  // временно
                try
                {
                    loadedScene.PreviewImage = Image.FromFile(dirPath + "/" + "image.jpg");
                }
                catch
                {

                }

                scenes.Add(loadedScene);
            }

            return;
        }

        public static void AddScene(string filePath)
        {
            // Логика добавления новой сцены
        }

        public static void SyncWithQuest(List<Scene> questScenes)
        {
            // Синхронизация сценами с Quest 3
        }
    }
}
