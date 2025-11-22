using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Hakatonv2
{
    public class ImageReceiver
    {
        private TcpListener imageListener;
        private Thread imageReceiverThread;
        private bool isImageReceiverRunning = false;

        // Событие для обновления UI с новым изображением
        public event Action<Image> OnImageReceived;

        // Очередь для полученных изображений
        private System.Collections.Concurrent.ConcurrentQueue<Image> imageQueue =
            new System.Collections.Concurrent.ConcurrentQueue<Image>();

        public void StartImageReceiver()
        {
            try
            {
                imageListener = new TcpListener(IPAddress.Any, 13002);
                imageListener.Start();
                isImageReceiverRunning = true;

                imageReceiverThread = new Thread(new ThreadStart(ImageReceiverWorker));
                imageReceiverThread.IsBackground = true;
                imageReceiverThread.Start();

                Debug.WriteLine("Image receiver started on port 13002");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting image receiver: {ex.Message}");
            }
        }

        public void StopImageReceiver()
        {
            isImageReceiverRunning = false;
            imageListener?.Stop();
        }

        private void ImageReceiverWorker()
        {
            while (isImageReceiverRunning)
            {
                try
                {
                    if (imageListener.Pending())
                    {
                        TcpClient client = imageListener.AcceptTcpClient();
                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleImageClient));
                        clientThread.IsBackground = true;
                        clientThread.Start(client);
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
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

        private void HandleImageClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();

            try
            {
                while (client.Connected && isImageReceiverRunning)
                {
                    // Читаем размер изображения (4 байта)
                    byte[] sizeBytes = new byte[4];
                    int bytesRead = stream.Read(sizeBytes, 0, 4);
                    if (bytesRead != 4) break;

                    int imageSize = BitConverter.ToInt32(sizeBytes, 0);

                    // Читаем данные изображения
                    byte[] imageData = new byte[imageSize];
                    int totalRead = 0;

                    while (totalRead < imageSize)
                    {
                        bytesRead = stream.Read(imageData, totalRead, imageSize - totalRead);
                        if (bytesRead == 0) break;
                        totalRead += bytesRead;
                    }

                    if (totalRead == imageSize)
                    {
                        // Конвертируем байты в изображение
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image receivedImage = Image.FromStream(ms);

                            // Добавляем в очередь и вызываем событие
                            imageQueue.Enqueue(receivedImage);
                            OnImageReceived?.Invoke(receivedImage);

                            Debug.WriteLine($"Image received: {imageSize} bytes");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Image client handling error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public Image GetNextImage()
        {
            Image image = null;
            imageQueue.TryDequeue(out image);
            return image;
        }

        public bool HasImages()
        {
            return !imageQueue.IsEmpty;
        }
    }
}
