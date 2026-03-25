using System.Net.Sockets;
using System.Text;

TcpClient client = new TcpClient();
try 
{
    Console.WriteLine("[CLIENT] Спроба підключення до сервера 127.0.0.1:8000...");
    client.Connect("127.0.0.1", 8000);
    Console.WriteLine("[CLIENT] Підключення встановлено успішно.");

    using NetworkStream stream = client.GetStream();

    byte[] buffer = new byte[1024];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string receivedMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.WriteLine($"[CLIENT] Отримано текст від сервера: \"{receivedMsg}\"");

    int count = receivedMsg.Length;
    string zeros = new string('0', count);
    Console.WriteLine($"[CLIENT] Довжина рядка: {count}. Готуємо {count} нулів для відповіді...");

    byte[] dataToSend = Encoding.UTF8.GetBytes(zeros);
    stream.Write(dataToSend, 0, dataToSend.Length);
    Console.WriteLine("[CLIENT] Нулі успішно відправлені на сервер.");
}
catch (Exception ex) 
{
    Console.WriteLine($"[CLIENT] ПОМИЛКА: {ex.Message}");
}
finally 
{
    client.Close();
    Console.WriteLine("[CLIENT] З'єднання розірвано.");
}