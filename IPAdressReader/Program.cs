
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Xml.Serialization;
using NetTools;

string command;
bool quitNow = false;

while (!quitNow)
{
    command = Console.ReadLine();
    switch (command)
    {
        case "--file-log":
            Console.WriteLine("путь к файлу с логами");
            TestReadFile("D:\\Desktop\\test.log");
            break;

        case "--file-output":
            Console.WriteLine("путь к файлу с результатом");
            TestWriteFile("path");
            break;

        case "--address-start":
            Console.WriteLine("нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса, необязательный параметр");
            TestIp("0.255.255.255");
            break;

        case "--address-mask":
            Console.WriteLine("маска подсети, задающая верхнюю границу диапазона десятичное число, необязательный параметр");
            TestMask("1");
            break;

        case "--time-start":
            Console.WriteLine("нижняя граница временного интервала");
            TestDateTime("12.12.2024");
            break;

        case "--time-end":
            Console.WriteLine("верхняя граница временного интервала");
            TestDateTime("12.12.2024");
            break;

        case "--help":
            Console.WriteLine(
                "--file-log - путь к файлу с логами\n" +
                "--file-output - путь к файлу с результатом\n" +
                "--address-start - нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса, необязательный параметр\n" +
                "--address-mask - маска подсети, задающая верхнюю границу диапазона десятичное число, необязательный параметр\n" +
                "--time-start - нижняя граница временного интервала\n" +
                "--time-end - верхняя граница временного интервала\n" +
                "--quit - завершение работы");
            break;

        case "--quit":
            quitNow = true;
            break;

        default:
            Console.WriteLine($"Unknown Command {command}");
            break;
    }
}

void TestReadFile(string path)
{
    var results = File.ReadLines(path);
}

void TestWriteFile(string path)
{

}

//DONE
IPAddress? TestIp(string ipAdd)
{
    Regex regex = new Regex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
    if (!regex.IsMatch(ipAdd))
        Console.WriteLine("NONE");

    IPAddress ip = IPAddress.Parse(ipAdd);



    return ip;
}

//DONE
void TestMask(string mask)
{
    int value = int.Parse(mask);
    if (value < 0 || value > 32)
        Console.WriteLine("NONE");
    //var rangeA = IPAddressRange.Parse("192.168.0.0/33");
    //bool t = rangeA.Contains(IPAddress.Parse("192.168.0.34"));
    //bool f = rangeA.Contains(IPAddress.Parse("192.168.10.1"));
}

//DONE
DateTime? TestDateTime(string date)
{
    Regex regex = new Regex(@"^(3[01]|[12][0-9]|0?[1-9])(\.)(1[0-2]|0?[1-9])\2([0-9]{2})?[0-9]{2}$");
    if (!regex.IsMatch(date))
        Console.WriteLine("NONE");
    return DateTime.Parse(date);
}