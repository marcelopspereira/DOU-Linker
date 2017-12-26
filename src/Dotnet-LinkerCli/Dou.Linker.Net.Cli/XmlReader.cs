using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dou.Linker.Net.Cli
{
    public class XmlReader
    {
        string article = "";
        public void ReadXmlFile(string xmlFile)
        {
            try
            {
                // Fará a leitura do XML dos artigos e deverá retornar a lei correspondente do XML através da lógica de Parse.

                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    string line;

                    var writer = new JsonGraphWriter();

                    // O retorno da linha será salva na variavel line, usar ela para identificar o regex e gerar o log json
                    while ((line = sr.ReadLine()) != null)
                    {


                        //Execucao do Linker para analisar o texto e buscar leis

                        article = article + " " + line;


                    }


                    //Escrevendo as variaveis no documento
                    writer.XMLtoJsonWriter(article);
                }
            }
            catch (Exception e)
            {
                // formato do arquivo carregado invalido caso entre aqui.
                Console.WriteLine("O arquivo nao pode ser lido");
                Console.WriteLine(e.Message);
            }


        }




    }

}
