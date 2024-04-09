using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAdressReader;

/// <summary>
/// Настройки фильтров
/// </summary>
public class FilterSettings
{
    /// <summary>
    /// Путь для чтения из файла
    /// </summary>
    public string PathIn {  get; set; }

    /// <summary>
    /// Путь для записи в файл
    /// </summary>
    public string PathOut { get; set; }

    /// <summary>
    /// Ip адрес
    /// </summary>
    public IPAddress? Ip { get; set; }

    /// <summary>
    /// Маска подсети
    /// </summary>
    public int? Mask { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    public DateTime? TimeStart { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateTime? TimeEnd { get; set; }
}
