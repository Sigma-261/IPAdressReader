using Mono.Options;
using NetTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPAdressReader;

public static class Services
{
    /// <summary>
    /// Запись ip адресов в файл, который выбрал пользовались
    /// </summary>
    /// <param name="filterSetting">Найстройки фильтров</param>
    public static void WriteIpAddresses(FilterSettings filterSetting)
    {
        Regex regexIp = new Regex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}");
        Regex regexDate = new Regex(@"\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])");

        var lines = File.ReadLines(filterSetting.PathIn).Where(x => regexIp.IsMatch(x) && regexDate.IsMatch(x));


        if (lines.Count() == 0)
        {
            throw new OptionException("Файл пустой или не подходит для обработки","");
        }

        if (filterSetting.TimeStart is not null && filterSetting.TimeEnd is not null)
        {
            lines = lines.Where(x => DateTime.Parse(regexDate.Match(x).Value) >= filterSetting.TimeStart && DateTime.Parse(regexDate.Match(x).Value) <= filterSetting.TimeEnd);
        }  

        string result = "";
        if (filterSetting.Ip is not null && filterSetting.Mask is not null)
        {
            var ipRange = IPAddressRange.Parse($"{filterSetting.Ip}/{filterSetting.Mask}");
            var numRequests = lines.Where(x => ipRange.Contains(IPAddress.Parse(regexIp.Match(x).Value))).Count();
            result = $"{filterSetting.Ip}:" + numRequests;
        }
        else
        {
            var results = lines.Select(x => IPAddress.Parse(regexIp.Match(x).Value)).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            result = string.Join("\n", results.Select(kv => kv.Key + ":" + kv.Value).ToArray());
        }

        File.WriteAllText(filterSetting.PathOut, result);
    }
}
