using CommandLine;
using CmdIPv4Parcer.Methods;

namespace CmdIPv4Parcer
{
    public class Program
    {
        public class Options
        {
            [Option('h', "help", Required = false, HelpText = "Get help")]
            public bool Help { get; set; }
        }

        public static void Main(string[] args)
        {
            var parcer = new ArgumentParcer();
            var read = new ReadFile();
            var write = new WriteFile();

            Parser.Default.ParseArguments<Options>(args)
                 .WithParsed<Options>(h =>
                 {
                     if (args.Length == 0)
                     {
                         Console.WriteLine("Не указаны параметры \n");
                         Console.WriteLine("Параметры: --file-log=<path> --file-output=<path> [--address-start=<IP>] [--address-mask=<mask>] \n");
                         Console.WriteLine("--file-log — путь к файлу с логами \n");
                         Console.WriteLine("--file-output — путь к файлу с результатом \n");
                         Console.WriteLine("--address-start —  нижняя граница диапазона адресов, \n необязательный параметр, по умолчанию обрабатываются все адреса\n");
                         Console.WriteLine("--address-mask — маска подсети, задающая верхнюю границу диапазона десятичное число. \n" +
                             "Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. \n" +
                             "Параметр нельзя использовать, если не задан address-start.");
                     }
                     else
                     {
                         try
                     {
                             var options = parcer.ParseArgs(args);
                             var ipCount = read.ReadDataFromFile(options.fileLog, options.adressStart, options.adressMask);
                             write.WriteOutputToFile(ipCount, options.fileOutput);

                             Console.WriteLine($"Анализ завершен. Результаты записаны в файл: {options.fileOutput}") ;
                         }
                         catch(Exception ex) 
                         {
                             var type = ex.GetType().ToString();

                             Console.WriteLine($"Тип ошибки : {type}");
                             Console.WriteLine($"Ошибка: {ex.Message}");
                         }
                 }
                 }
        );
        }
    }
}
