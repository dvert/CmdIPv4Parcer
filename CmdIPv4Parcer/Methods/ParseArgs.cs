using CommandLine;

namespace CmdIPv4Parcer.Methods
{
    public class ArgumentParcer
    {
        /// <summary>
        /// Получение данных запроса из командной строки
        /// </summary>
        public Config ParseArgs(string[] args)
        {
            var config = new Config();
            var parser = new Parser(p => p.HelpWriter = null);
            parser.ParseArguments<Config>(args)
                .WithParsed(x =>
                {
                    config.fileLog = x.fileLog;
                    config.fileOutput = x.fileOutput;
                    config.adressStart = x.adressStart;
                    config.adressMask = x.adressMask;
                });

          
            if (string.IsNullOrEmpty(config.fileLog) || string.IsNullOrEmpty(config.fileOutput))
            {
                throw new ArgumentException("Не указан путь к лог файлу и выходному файлу");
            }

            if (config.adressMask.HasValue && config.adressStart == null)
            {
                throw new ArgumentException("Параметр --address-mask можно использовать только с --address-start.");
            }

            return config;
        }
    }
}
