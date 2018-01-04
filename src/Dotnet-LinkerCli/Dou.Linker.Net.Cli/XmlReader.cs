using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Dou.Linker.Net.Cli.Models;

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
                        XmlArticle.FindBodyArticle(article);
                        XmlArticle.FindCaputArticle(article);
                    }

                    var LinkerProcessor = new TextExtractor();

                    //Busca de Leis, Portarias e etc...
                   

                    LinkerProcessor.FindTitleLei(XmlArticleLayout.ArticleTitle);
                    LinkerProcessor.FindBodyLei(XmlArticleLayout.ArticleBody);
                    LinkerProcessor.FindLeiTraceability(XmlArticleLayout.ArticleCaput);


                    //Busca de verbos de acao em leis e portarias (revoga, altera e etc..)
                    // LinkerProcessor.FindLeiTraceability(XmlArticleLayout.ArticleCaput);


                    //Escrevendo as variaveis no documento

                    var lei = new Lei();


                    var writer = new JsonGraphWriter();
                    writer.XMLtoJsonWriter(lei.Name + "\n" + lei.Child + "-" + TextExtractor.ActionLei);

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
