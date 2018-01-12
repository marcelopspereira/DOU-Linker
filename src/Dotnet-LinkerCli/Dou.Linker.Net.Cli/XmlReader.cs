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

                    var LinkerLeiProcessor = new LeiTextExtractor();
                    var LinkerPortariaProcessor = new PortariaTextExtractor();


                    //Busca do item Pai 


                    LinkerLeiProcessor.FindTitleLei(XmlArticleLayout.ArticleTitle);
                    LinkerPortariaProcessor.FindTitlePortaria(XmlArticleLayout.ArticleTitle);


                    //Busca rastreabilidade de leis nos childs


                    LinkerLeiProcessor.FindBodyLei(XmlArticleLayout.ArticleBody);
                    LinkerLeiProcessor.FindBodyLei(XmlArticleLayout.ArticleCaput);
                    LinkerLeiProcessor.FindLeiTraceability(XmlArticleLayout.ArticleBody + XmlArticleLayout.ArticleCaput);

                    

                    //Buscando rastreabilidade da portaria nos childs

                     LinkerPortariaProcessor.FindBodyPortaria(XmlArticleLayout.ArticleBody);
                     LinkerPortariaProcessor.FindBodyPortaria(XmlArticleLayout.ArticleCaput);
                     LinkerPortariaProcessor.FindPortariaTraceability(XmlArticleLayout.ArticleBody + XmlArticleLayout.ArticleCaput);


                    //Print dos resultados ou export deles

                    LinkerPortariaProcessor.PrintResults();
                    LinkerLeiProcessor.PrintResults();
                    


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
