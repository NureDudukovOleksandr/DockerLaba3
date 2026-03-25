using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
           
            TcpListener server = new TcpListener(IPAddress.Any, 8000);
            server.Start();

            Console.WriteLine("[SERVER] TCP Сервер запущено на 0.0.0.0:8000 (Docker-ready)");
            Console.WriteLine("[SERVER] Очікування підключення клієнта...");

            try
            {
                using TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"[SERVER] Клієнт під'єднався: {client.Client.RemoteEndPoint}");

                using NetworkStream stream = client.GetStream();

                
                string message = "Hello, test message for laba 2";
                byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine($"[SERVER] Відправлено текст клієнту: \"{message}\" ({dataToSend.Length} байт)");

                
                byte[] buffer = new byte[1024];
                Console.WriteLine("[SERVER] Очікування відповіді від клієнта...");
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                
                if (bytesRead > 0)
                {
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[SERVER] Отримано від клієнта: {response}");
                    Console.WriteLine($"[SERVER] Кількість отриманих символів: {response.Length}");
                }

                
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SERVER] Помилка: {ex.Message}");
            }
            finally
            {
                server.Stop();
                Console.WriteLine("[SERVER] Роботу завершено. Сокет закрито.");
            }
        }
    }
}