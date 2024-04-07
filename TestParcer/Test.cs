using System.Net;
using CmdIPv4Parcer;
using CmdIPv4Parcer.Methods;
using NUnit.Framework.Constraints;
using NUnit.Framework.Legacy;

namespace TestParcer
{
    public class Tests
    {
        [Test]
        public void StartAddressIsNull()
        {
            ReadFile read = new ReadFile();
            var address = IPAddress.Parse("192.168.0.1");
            var result = read.IsAddressInRange(address, null, null);

            ClassicAssert.True(result);
        }

        [Test]
        public void WritesToFile()
        {
            WriteFile write = new WriteFile();
            var ipCounts = new Dictionary<IPAddress, int>
            {
                { IPAddress.Parse("192.168.0.1"), 1 },
                { IPAddress.Parse("192.168.0.3"), 3 }
            };
            var outputFilePath = "output.txt";
            write.WriteOutputToFile(ipCounts, outputFilePath);

            ClassicAssert.True(File.Exists(outputFilePath));
            Assert.That("192.168.0.3: 3 \r\n 192.168.0.1: 1 \r\n ", Is.EqualTo(File.ReadAllText(outputFilePath)));
        }

       
    }
}
