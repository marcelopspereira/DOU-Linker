﻿using System;
using System.IO;
using Dou.Linker.Net.Cli.Models;

namespace Dou.Linker.Net.Cli
{
  
    class Program
    {
        private static int i = 14;

       
        private static string file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample"+ i + ".xml";

        static void Main(string[] args)
        {
            var reader = new XmlReader();

            //Leitura do XML e armazenamento da linha na variavel line
           // for (i = 9; i < 13; i++)
         //   {
             //   file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample" + i + ".xml";
                reader.ReadXmlFile(file);

            //  }

            //geracao do arquivo de log no formato JSON e grafo (1 Artigo XML = 1 Documento JSON do Grafo)


           

            Console.WriteLine("Linker executado com sucesso...");
            

            Console.ReadLine();
        }
    }
}

