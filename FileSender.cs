using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class FileSender : IDisposable
{
    private TcpListener imageListener;
    private TcpClient client;
    private NetworkStream stream;
    private Thread imageReceiverThread;
    private bool isImageReceiverRunning = false;

    string selectedVideoPath;

    public float progress = 0;

    public void StartFileSender(string path)
    {
        selectedVideoPath = path;
        try
        {
            imageListener = new TcpListener(IPAddress.Any, 13003);
            imageReceiverThread = new Thread(new ThreadStart(StartServer));
            imageReceiverThread.IsBackground = true;
            imageReceiverThread.Start();
            isImageReceiverRunning = true;

            /*imageReceiverThread = new Thread(new ThreadStart(ImageReceiverWorker));
            imageReceiverThread.IsBackground = true;
            imageReceiverThread.Start();*/

            Debug.WriteLine("File sender started on port 13003");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error starting image receiver: {ex.Message}");
        }
    }

    private void StartServer()
    {
        try
        {
            imageListener.Start();

            while (isImageReceiverRunning)
            {
                if (imageListener.Pending())
                {
                    TcpClient client = imageListener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
                Thread.Sleep(100);
            }
        }
        catch (Exception ex)
        {
            if (isImageReceiverRunning)
            {
                isImageReceiverRunning = false;
            }
        }
    }

    private void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;

        try
        {
            using (NetworkStream stream = client.GetStream())

            using (FileStream fileStream = File.OpenRead(selectedVideoPath))
            {
                // Отправляем имя файла
                string fileName = Path.GetFileName(selectedVideoPath);
                byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLength = BitConverter.GetBytes(fileNameBytes.Length);
                stream.Write(fileNameLength, 0, 4);
                stream.Write(fileNameBytes, 0, fileNameBytes.Length);

                // Отправляем размер файла
                byte[] fileSizeBytes = BitConverter.GetBytes(fileStream.Length);
                stream.Write(fileSizeBytes, 0, 8);

                // Отправляем сам файл
                byte[] buffer = new byte[8192];
                int bytesRead;
                long totalBytes = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                    totalBytes += bytesRead;

                    progress = (float) Math.Round((double)(totalBytes / fileStream.Length * 100));
                }
            }

            client.Close();
        }
        catch (Exception ex)
        {
            client.Close();
        }
    }

    private void ImageReceiverWorker()
    {
        while (isImageReceiverRunning)
        {
            try
            {
                // Используем AcceptTcpClientAsync для асинхронного принятия подключения
                client = imageListener.AcceptTcpClient();
                stream = client.GetStream();

                Debug.WriteLine("Client connected for file transfer");

                // Теперь мы готовы отправлять файлы этому клиенту
                // Не нужно создавать отдельный поток для каждого клиента, если у нас один клиент
            }
            catch (ObjectDisposedException)
            {
                // Листнер был остановлен, это нормально при завершении
                break;
            }
            catch (Exception ex)
            {
                if (isImageReceiverRunning)
                {
                    Debug.WriteLine($"Image receiver error: {ex.Message}");
                }
            }
        }
    }

    public void StopFileSender()
    {
        isImageReceiverRunning = false;

        try
        {
            stream?.Close();
            client?.Close();
            imageListener?.Stop();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error stopping file sender: {ex.Message}");
        }
    }

    public bool IsClientConnected()
    {
        return client != null && client.Connected;
    }

    public void SendVideoFile(string filePath)
    {
        if (!IsClientConnected() || stream == null)
        {
            MessageBox.Show("Нет подключения к клиенту!");
            return;
        }

        if (!File.Exists(filePath))
        {
            MessageBox.Show($"Файл не существует: {filePath}");
            return;
        }

        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            long fileSize = fileInfo.Length;

            // Получаем длительность видео (если нужно)
            //TimeSpan duration = GetVideoDuration(filePath);

            // Отправляем команду о начале передачи файла с информацией о длительности
            string header = $"FILE_START|{fileName}|{fileSize}|";
            byte[] headerData = Encoding.UTF8.GetBytes(header);
            stream.Write(headerData, 0, headerData.Length);

            Debug.WriteLine($"Sending file: {fileName}, Size: {fileSize}");

            // Отправляем файл частями с прогрессом
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[8192]; // 8KB буфер
                int bytesRead;
                long totalBytesSent = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                    totalBytesSent += bytesRead;

                    // Обновляем прогресс каждые 5%
                    if (fileSize > 0)
                    {
                        int progress = (int)((double)totalBytesSent / fileSize * 100);
                        if (progress % 5 == 0 || totalBytesSent == fileSize)
                        {
                            Debug.WriteLine($"Отправлено: {progress}%");
                        }
                    }
                }
            }

            // Отправляем команду о завершении передачи
            string footer = "FILE_END|";
            byte[] footerData = Encoding.UTF8.GetBytes(footer);
            stream.Write(footerData, 0, footerData.Length);

            MessageBox.Show($"Файл {fileName} успешно отправлен!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка отправки файла: {ex.Message}");
            Debug.WriteLine($"SendVideoFile error: {ex}");
        }
    }

    private TimeSpan GetVideoDuration(string filePath)
    {
        try
        {
            // Здесь должна быть реализация получения длительности видео
            // Можно использовать Windows Media Player, FFmpeg, или другую библиотеку
            // Временно возвращаем TimeSpan.Zero

            // Пример с Windows Media Player (раскомментируйте если есть ссылка на WMPLib):
            /*
            var wmp = new WMPLib.WindowsMediaPlayer();
            var media = wmp.newMedia(filePath);
            return TimeSpan.FromSeconds(media.duration);
            */

            return TimeSpan.Zero;
        }
        catch
        {
            return TimeSpan.Zero;
        }
    }

    public void Dispose()
    {
        StopFileSender();
    }
}
