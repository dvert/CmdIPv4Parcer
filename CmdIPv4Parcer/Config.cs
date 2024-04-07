using CommandLine;
using System.Net;

namespace CmdIPv4Parcer
{
    public class Config
    {
        /// <summary>
        /// Конфигурационные параметры для командной строки
        /// </summary>
        [Option("file-log", Required = true, HelpText = "путь к файлу с логами")]
        public string fileLog { get; set; }

        [Option("file-output", Required = true, HelpText = "путь к файлу с результатом")]
        public string fileOutput { get; set; }

        [Option("address-start", HelpText = "нижняя граница диапазона адресов")]
        public IPAddress adressStart { get; set; }

        [Option("address-mask", HelpText = "маска подсети, задающая верхнюю границу диапазона")]
        public int? adressMask { get; set; }

    }
}
