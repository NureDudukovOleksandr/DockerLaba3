using System.Net;
using System.Net.Sockets;
using System.Text;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);

Console.WriteLine("[UDP CLIENT] Відправка стартового сигналу на сервер...");
client.SendTo(new byte[1], serverEP);

byte[] buffer = new byte[1024];
EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

Console.WriteLine("[UDP CLIENT] Очікування даних від сервера...");
int bytes = client.ReceiveFrom(buffer, ref remoteEP);
string msg = Encoding.UTF8.GetString(buffer, 0, bytes);
Console.WriteLine($"[UDP CLIENT] Отримано: \"{msg}\"");

string zeros = new string('0', msg.Length);
client.SendTo(Encoding.UTF8.GetBytes(zeros), serverEP);
Console.WriteLine($"[UDP CLIENT] Відправлено {msg.Length} нулів назад на сервер.");

client.Close();
Console.WriteLine("[UDP CLIENT] Роботу завершено.");