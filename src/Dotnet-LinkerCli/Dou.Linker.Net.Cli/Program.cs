using System;
using System.IO;

namespace Dou.Linker.Net.Cli
{
  
    class Program
    {
        private static string file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample1.xml";

        static void Main(string[] args)
        {
            var reader = new XmlReader();

            reader.ReadXmlFile(file);

            Console.ReadLine();
        }
    }
}

