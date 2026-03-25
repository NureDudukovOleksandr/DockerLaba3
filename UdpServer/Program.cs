using System.Net;
using System.Net.Sockets;
using System.Text;

Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 8001);
server.Bind(localEP);

Console.WriteLine("[UDP SERVER] Слухаємо порт 8001...");

byte[] buffer = new byte[1024];
EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

Console.WriteLine("[UDP SERVER] Очікування сигналу від клієнта...");
server.ReceiveFrom(buffer, ref remoteEP);
Console.WriteLine($"[UDP SERVER] Отримано запит від: {remoteEP}");

string text = "Hello, test message for laba 2";
server.SendTo(Encoding.UTF8.GetBytes(text), remoteEP);
Console.WriteLine($"[UDP SERVER] Відправлено текст: \"{text}\"");

int received = server.ReceiveFrom(buffer, ref remoteEP);
string response = Encoding.UTF8.GetString(buffer, 0, received);
Console.WriteLine($"[UDP SERVER] Отримано відповідь (нулі): {response}");

server.Close();
Console.WriteLine("[UDP SERVER] Сокет закрито.");