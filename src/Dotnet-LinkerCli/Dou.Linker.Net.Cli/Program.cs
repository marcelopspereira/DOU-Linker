﻿using System;
using System.IO;

namespace Dou.Linker.Net.Cli
{
  
    class Program
    {
        private static int i = 9;

       
        private static string file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample"+ i + ".xml";

        static void Main(string[] args)
        {
            var reader = new XmlReader();

            //Leitura do XML e armazenamento da linha na variavel line
            for (i = 9; i < 10; i++)
            {
                file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample" + i + ".xml";
                reader.ReadXmlFile(file);
                
            }

            //Analise da variavel line para captura dos termos (lei, portaria, projeto de lei e etc...)





            //geracao do arquivo de log no formato JSON e grafo (1 Artigo XML = 1 Documento JSON do Grafo)



            Console.WriteLine("Linker executado com sucesso...");
            Console.WriteLine(Linker.TitleLei);
            Console.WriteLine(Linker.BodyLei);

            for (int i = 0; i<Linker.LeiList.Count;i++)
            Console.WriteLine("-> " + Linker.LeiList[i]);
            Console.ReadLine();
        }
    }
}

