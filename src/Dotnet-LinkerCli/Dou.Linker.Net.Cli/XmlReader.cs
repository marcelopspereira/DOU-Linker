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
                // Fará a leitura do XML dos artigos e deverá retornar a lei correspondente do XML através da lógica de Regex.

                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    string article;

                    var XmlArticle = new XmlArticleLayout();


                    article = sr.ReadToEnd();
                    {                      
                        XmlArticle.FindTitleArticle(article);
                        XmlArticle.FindCaputArticle(article);
                        XmlArticle.FindBodyArticle(article);
                    }

                    var LinkerProcessor = new Linker();

                    //Busca de Leis, Portarias e etc...

                    LinkerProcessor.FindTitleLei(XmlArticleLayout.ArticleTitle);
                    LinkerProcessor.FindBodyLei(XmlArticleLayout.ArticleBody);


                    //Busca de verbos de acao em leis e portarias (revoga, altera e etc..)
                    LinkerProcessor.FindLeiTraceability(XmlArticleLayout.ArticleCaput);
              

                    //Escrevendo as variaveis no documento

                    var writer = new JsonGraphWriter();
                    writer.XMLtoJsonWriter(Linker.TitleLei + "\n" + Linker.IDLei + "-" + Linker.ActionLei);

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
