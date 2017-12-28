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
            string lei = "";
            try
            {
                // Fará a leitura do XML dos artigos e deverá retornar a lei correspondente do XML através da lógica de Regex.

                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    string article;
                   

                    article = sr.ReadToEnd();
                    {
                        var getTitle = new Linker();

                        getTitle.FindArticleTitle(article);
                        getTitle.GetBodyArticle(article);

                        //Busca de Leis, Portarias e etc...

                        getTitle.FindTitleLei(Linker.ArticleTitle);
                        


                    }

                    //Escrevendo as variaveis no documento

                    var writer = new JsonGraphWriter();
                    writer.XMLtoJsonWriter(Linker.TitleLei + "\n" + Linker.ArticleBody);

                }
            }
            catch (Exception e)
            {
                // formato do arquivo carregado invalido caso entre aqui.
                Console.WriteLine("O arquivo nao pode ser lido");
                Console.WriteLine(e.Message);
            }


        }

        private static string FindLei(string article, string line, Regex rgx)
        {
            MatchCollection matches = rgx.Matches(line);

            //Execucao do Linker para analisar o texto e buscar leis

            foreach (Match match in matches)

                article += "->" + match + "\n";
            return article;
        }
    }

}
