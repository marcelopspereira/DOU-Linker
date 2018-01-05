using System;
using System.IO;
using Dou.Linker.Net.Cli.Models;

namespace Dou.Linker.Net.Cli
{
  
    class Program
    {
        private static int i = 11;

       
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

            //Analise da variavel line para captura dos termos (lei, portaria, projeto de lei e etc...)





            //geracao do arquivo de log no formato JSON e grafo (1 Artigo XML = 1 Documento JSON do Grafo)


           

            Console.WriteLine("Linker executado com sucesso...");
            Console.WriteLine(TextExtractor.lei.Name);

            Console.WriteLine("\n");


            //Impressao das Leis filhas

            for (var i = 0; i<TextExtractor.lei.Child.Count;i++)
            Console.WriteLine(TextExtractor.lei.Child[i]);


            Console.WriteLine("\n");

            //Mostrando a rastreabilidade entre itens

            for (var i = 0; i < TextExtractor.lei.LinkItemChild.Count; i++)
            {
                Console.WriteLine(TextExtractor.lei.LinkItemChild[i]);

            }

            Console.ReadLine();
        }
    }
}

