using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public static class NetworkManager
    {
        static TcpListener server;
        static TcpClient client;

        public static NetworkStream stream { get; private set; }
        static Thread listenerThread;
        static bool isServerRunning = false;

        public delegate void StringDelegate(string[] message);
        public delegate void OnConnectionDeleted();
        public delegate void OnConnectionActivated();
        public delegate void OnConnectionWait();

        public static event StringDelegate onStringEvent;
        public static event OnConnectionDeleted onConnectionDeleted;
        public static event OnConnectionActivated onConnectionActivated;
        public static event OnConnectionWait onConnectionWait;

        private static HttpClient _httpClient;

        private static async void SendIp(string ip)
        {
            try
            {

                // Создаем содержимое запроса
                var content = new StringContent(ip, Encoding.UTF8, "text/plain");

                // Отправляем POST запрос
                var response = await _httpClient.PostAsync("http://45.82.152.135:5000/setIP", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static void StartServer()
        {

            if (isServerRunning)
            {
                return;
            }

            Debug.WriteLine("Запуск сервера");

            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);

            try
            {
                server = new TcpListener(IPAddress.Any, 13001);
                server.Start();
                isServerRunning = true;

                listenerThread = new Thread(new ThreadStart(ListenForClients));
                listenerThread.IsBackground = true;
                listenerThread.Start();

                onConnectionWait?.Invoke();

                IPEndPoint serverEndPoint = (IPEndPoint)server.LocalEndpoint;
                string serverIP = serverEndPoint.Address.ToString();
                int serverPort = serverEndPoint.Port;

                string hostName = Dns.GetHostName();
                IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
                IPAddress localIpAddress = hostEntry.AddressList[1];

                // `localIpAddress` будет содержать первый найденный IP-адрес
                string ipString = localIpAddress.ToString();

                SendIp(ipString);

                Debug.WriteLine(ipString);
                Debug.WriteLine(serverPort);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска сервера: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void ListenForClients()
        {
            while (isServerRunning)
            {
                try
                {
                    client = server.AcceptTcpClient();
                    stream = client.GetStream();

                    // Переключаемся на главную панель
                    onConnectionActivated?.Invoke();

                    // Запускаем поток для приема сообщений
                    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения: {ex.Message}");
                }
            }
        }

        public static bool IsConnected()
        {
            return !(client == null || !client.Connected);
        }

        static void ReceiveMessages()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            while (client != null && client.Connected)
            {
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.WriteLine(message);
                    onStringEvent?.Invoke(message.Split('/'));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка приема сообщения: {ex.Message}");
                    break;
                }
            }

            //Отключился
            onConnectionDeleted?.Invoke();
        }

        public static void SendMessage(string message)
        {
            if (client != null && client.Connected && stream != null)
            {
                try
                {
                    message += "/";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Отправлено: {message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки: {ex.Message}");
                }
            }
        }
        
    }
}
