
using System.Windows.Input;

string command;
bool quitNow = false;
while (!quitNow)
{
    command = Console.ReadLine();
    switch (command)
    {
        case "--help":
            Console.WriteLine("help");
            break;

        case "--file-log":
            Console.WriteLine("путь к файлу с логами");
            break;

        case "--file-output":
            Console.WriteLine("путь к файлу с результатом");
            break;

        case "--address-start":
            Console.WriteLine("нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса");
            break;

        case "--address-mask":
            Console.WriteLine("маска подсети, задающая верхнюю границу диапазона десятичное число");
            break;

        case "--time-start":
            Console.WriteLine("нижняя граница временного интервала");
            break;

        case "--time-end":
            Console.WriteLine("верхняя граница временного интервала");
            break;

        case "/quit":
            quitNow = true;
            break;

        default:
            Console.WriteLine($"Unknown Command {command}");
            break;
    }
}