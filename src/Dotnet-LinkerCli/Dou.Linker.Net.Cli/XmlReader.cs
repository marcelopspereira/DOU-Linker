using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Dou.Linker.Net.Cli
{
    public class XmlReader
    {
        
        public void ReadXmlFile(string xmlFile)
        {
            try
            {
                // Fará a leitura do XML dos artigos e deverá retornar a lei correspondente do XML através da lógica de Parse.

                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    string line;
                    string article = "";

                    //variaveis do regex
                    string pattern = @"(Lei|LEI|Lei complementar|LEI COMPLEMENTAR|Lei Complementar|Decreto|DECRETO|Portaria|PORTARIA|Projeto de Lei|Acórdão|ACÓRDÃO|Emenda|EMENDA[0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
                    Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

                    var writer = new JsonGraphWriter();

                    // O retorno da linha será salva na variavel line, usar ela para identificar o regex e gerar o log json
                    while ((line = sr.ReadLine()) != null)
                    {
                        MatchCollection matches = rgx.Matches(line);

                        //Execucao do Linker para analisar o texto e buscar leis

                      //  foreach (Match match in matches)
                            article = article + ";" + line;


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
