using System;
using System.IO;

namespace Dou.Linker.Net.Cli
{
  
    class Program
    {
        private static int i = 12;

       
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
            Console.WriteLine(Linker.TitleLei);


            //Impressao dos verbos de alteracao do CAPUT

            for (int i = 0; i < Linker.ActionLei.Count; i++)
                Console.WriteLine("-Altera-> " + Linker.ActionLei[i]);


            Console.WriteLine("\n");


            //Impressao das Leis sem referencias


            for (int i = 0; i<Linker.IDLei.Count;i++)
            Console.WriteLine("-Undefined-> " + Linker.IDLei[i]);
            Console.ReadLine();
        }
    }
}

