using System.Net;
using System.Text.RegularExpressions;
using IPAdressReader;
using Mono.Options;
using Newtonsoft.Json;

FilterSettings filterSettings = new();

OptionSet options = new OptionSet() {
            { "file-json=", "Путь к файлу конфигурации",
              path => filterSettings = ReadJsonFile(path) },
            { "file-log=", "Путь к файлу с логами",
              path => filterSettings.PathIn = CheckPath(path) },
            { "file-output=", "Путь к файлу с результатом",
              path => filterSettings.PathOut = CheckPath(path) },
            { "address-start=", "Нижняя граница диапазона адресов, по умолчанию обрабатываются все адреса, необязательный параметр",
              ip => filterSettings.Ip = ConvertIp(ip) },
            { "address-mask=", "Маска подсети, задающая верхнюю границу диапазона десятичное число, необязательный параметр",
              mask => filterSettings.Mask = ConvertMask(mask)},
            { "time-start=", "Нижняя граница временного интервала",
              date => filterSettings.TimeStart = ConvertDate(date) },
            { "time-end=", "Верхняя граница временного интервала",
              date => filterSettings.TimeEnd = ConvertDate(date) },
        };

try
{
    options.Parse(args);
}
catch (OptionException e)
{
    Console.WriteLine($"Ошибка: {e.Message}");
    Console.WriteLine($"Параметр: {e.OptionName}");
    return;
}

try
{
    Services.WriteIpAddresses(filterSettings);
}
catch(OptionException e)
{
    Console.WriteLine($"Ошибка: {e.Message}");
}

string CheckPath(string path)
{
    if (path == "")
        throw new OptionException("Пустой путь!", "file-log или file-output");
    try
    {
        File.ReadAllText(path);
        return path;
    }
    catch (Exception e)
    {
        throw new OptionException(e.Message, "file-log или file-output");
    }

}

IPAddress? ConvertIp(string ipAdd)
{
    if(ipAdd == "")
        return null;

    Regex regex = new Regex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");

    if (!regex.IsMatch(ipAdd))
        throw new OptionException("Некорректный ip адрес! Формат: 255.255.255.255", "address-start");

    return IPAddress.Parse(ipAdd);
}

int? ConvertMask(string mask)
{
    if (mask == "")
        return null;

    int value = int.Parse(mask);
    if (value < 0 || value > 32)
        throw new OptionException("Некорректный формат маски! Формат: целое положительное десятичное число, не больше 32", "address-mask");
    //if(ip is null)
    //    throw new OptionException("Параметр нельзя использовать, если не задан address-start!", "address-mask");

    return value;
}

DateTime? ConvertDate(string date)
{
    if (date == "")
        return null;

    Regex regex = new Regex(@"^(3[01]|[12][0-9]|0?[1-9])(\.)(1[0-2]|0?[1-9])\2([0-9]{2})?[0-9]{2}$");
    if (!regex.IsMatch(date))
        throw new OptionException("Некорректный формат даты! Формат: дд.ММ.гггг", "time-start или time-end");

    return DateTime.Parse(date);
}

FilterSettings ReadJsonFile(string path)
{
    try
    {
        using StreamReader reader = new(CheckPath(path));
        var json = reader.ReadToEnd();
        Dictionary<string, string> settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;

        return new FilterSettings()
        {
            PathIn = CheckPath(settings["pathIn"]),
            PathOut = CheckPath(settings["pathOut"]),
            Ip = ConvertIp(settings["ip"]),
            Mask = ConvertMask(settings["mask"]),
            TimeStart = ConvertDate(settings["timeStart"]),
            TimeEnd = ConvertDate(settings["timeEnd"]),
        };
    }
    catch (Exception e)
    {
        throw new OptionException(e.Message, "file-json");
    }
}