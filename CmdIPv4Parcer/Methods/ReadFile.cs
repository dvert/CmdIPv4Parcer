using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CmdIPv4Parcer.Methods
{
    /// <summary>
    /// Чтение данных из лог файла
    /// </summary>
    public class ReadFile
    {
        public Dictionary<IPAddress, int> ReadDataFromFile(string logFile, IPAddress addressStart, int? addressMask)
        {
            var ipCount = new Dictionary<IPAddress, int>();
            try
            {
                if (!File.Exists(logFile))
                {
                    throw new FileNotFoundException("Лог файл не найден", logFile);
                }

                using (var reader = new StreamReader(logFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(' ');
                        if (parts.Length != 2)
                            continue;

                        var ipAddress = IPAddress.Parse(parts[0]);
                        var timestamp = DateTime.Parse(parts[1]);

                        if (IsAddressInRange(ipAddress, addressStart, addressMask))
                        {
                            if (ipCount.ContainsKey(ipAddress))
                                ipCount[ipAddress]++;
                            else
                                ipCount[ipAddress] = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка чтения лог-файла {ex}");
            }

            return ipCount;
        }

        /// <summary>
        /// Условие, если задана маска
        /// </summary>
        public bool IsAddressInRange(IPAddress address, IPAddress addressStart, int? addressMask)
        {
            if (addressStart is null)
                return true;

            if (addressMask.HasValue)
            {
                var mask = IPAddress.Parse($"255.255.255.{addressMask}");
                var addressBytes = address.GetAddressBytes();
                var startBytes = addressStart.GetAddressBytes();

                for (int i = 0; i < 4; i++)
                {
                    if ((addressBytes[i] & mask.GetAddressBytes()[i]) != (startBytes[i] & mask.GetAddressBytes()[i]))
                        return false;
                }

                return true;
            }
            return address.ToString().CompareTo(addressStart) >= 0;

        }
    }
}
