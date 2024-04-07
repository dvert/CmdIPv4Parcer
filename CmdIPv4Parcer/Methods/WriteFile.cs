using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CmdIPv4Parcer.Methods
{   
    /// <summary>
    /// Запись полученных данных после парсинга в файл
    /// </summary>
    public class WriteFile
    {
        public void WriteOutputToFile(Dictionary<IPAddress, int> ipCount, string putputFile)
        {
            try
            {
                using (var sw = new StreamWriter(putputFile,true))
                {
                    foreach (var pair in ipCount.OrderByDescending(p => p.Value))
                    {
                        sw.WriteLine($"{pair.Key}: {pair.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка записи результатов: {ex.Message}");
            }
        }
    }
}
