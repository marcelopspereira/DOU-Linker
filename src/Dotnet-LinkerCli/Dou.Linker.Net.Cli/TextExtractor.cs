using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Dou.Linker.Net.Cli.Models;

namespace Dou.Linker.Net.Cli
{
    public class TextExtractor
    {

        public static string TitleLei { get; set; }

        public static string BodyLei { get; set; }

       // public static List<string> lei.Child { get; set; } = new List<string>();
        public static List<string> ActionLei { get; set; } = new List<string>();

        public static Lei lei = new Lei();

        public string leiAltera = "";
        public string linkAltera = "indefinido";


        public void FindTitleLei(string ArticleTitle)
        {
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleTitle);

            Match match = matches[0];

            //Tratamento do titulo

            lei.Name = match.Value;
           


        }


        public void FindBodyLei(string ArticleBody)
        {
            var pattern = @"(Lei nº|Lei no|Leis nº|Leis nos) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)"; //REMOVER (.*). CASO NÃO FUNCIONE NO CENÁRIO DE BATCH
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList
            
           

            foreach (Match match in matches)
            {

                var cleanMatchValue = match.Value;
                    leiAltera = cleanMatchValue;

                var regex = new Regex(@"([0-9]+(\.[0-9]+)?(\-[0-9]+)?)");
                var leiMatch = regex.Match(cleanMatchValue);

                if (leiMatch.Success)
                {
                    leiAltera = leiMatch.Value;
                }
             

                lei.Child.Add(leiAltera);

            }

            //Remover itens duplicados e adicionar referencia indefinida no link

            lei.Child.Sort();

            Int32 index = 0;
            while (index < lei.Child.Count - 1)
            {
                if (lei.Child[index] == lei.Child[index + 1])
                {
                    lei.Child.RemoveAt(index + 1);
                    lei.Child.RemoveAt(index);
                    lei.Child.Add(leiAltera);
                    lei.LinkType.Add(linkAltera);
                }

                else
                    index++;
            }

         
            
           
            
        }

        public void FindLeiTraceability(string text)
        {

            for (var i = 0; i < lei.Child.Count; i++)
            {
                lei.LinkItemChild.Add(lei.Child[i] + ";" + linkAltera);
            }

            lei.LinkItemParent = lei.Name;


            //Usar o regex para capturar o Verbo Altera e o ponto final, após a captura deverei verificar se existe alguma lei dentro dela (Olhar a Lista de Strings já capturada)


            var pattern = @"Altera(.*)(Revoga|.)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(text);

            //////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matches)
            {

                if (match.Success)
                {
                   for (var i = 0; i<lei.LinkItemChild.Count;i++)
                   {
                      if(match.Value.Contains(lei.Child[i])==true)
                        {
                            var linkAltera = "Altera";

                            lei.LinkItemChild[i] = "Lei " + lei.Child[i] + ";" + linkAltera;                          

                        }
                        

                   }
                  
                }

            }



            var patternRev = @"Revoga(.*)(Altera|Reinvidica|.)";
            Regex rgxRev = new Regex(patternRev, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matchesRev = rgxRev.Matches(text);

            //////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matchesRev)
            {

                if (match.Success)
                {
                    for (var i = 0; i < lei.LinkItemChild.Count; i++)
                    {
                        if (match.Value.Contains(lei.Child[i]) == true)
                        {
                            //if(lei.LinkItemChild[i].Contains("Altera") != true)
                            //    {

                                var linkAltera = "Revoga";

                                lei.LinkItemChild[i] = "Lei " + lei.Child[i] + ";" + linkAltera;

                                //}
                      

                        }


                    }

                }

            }

            //    lei.Child.Add(leiAltera);

            //    //Adiciona Altera no final do retorno - AQUI DEVERA TER ALTERA OU REVOGA, DEVERA SER NO MESMO METODO PARA EVITAR EXECUCAO DEMASIADA E CLASSIFICACAO, UMA VEZ QUE EU JA CAPTUREI OS ITENS DO BODY, TALVEZ DEVEREI EXECUTAR O CLEAN NO CAPUT E BODY.
            //    var regexAltera = new Regex("(Altera|Revoga)(.*).");
            //    var alteraMatch = regexAltera.Match(cleanMatchValue);

            //    if (alteraMatch.Success)

            //    {

            //        linkAltera = alteraMatch.Value;
            //        lei.LinkType.Add(linkAltera);
            //    }


            //    else
            //    {
            //        linkAltera = "indefinido";
            //        lei.LinkType.Add(linkAltera);
            //    }



            //    lei.LinkItemChild.Sort();


            //    //Int32 index = 0;
            //    //while (index < lei.LinkItemChild.Count - 1)
            //    //{
            //    //    if (lei.LinkItemChild[index] == lei.LinkItemChild[index + 1])
            //    //    {
            //    //        lei.LinkItemChild.RemoveAt(index + 1);
            //    //        lei.LinkItemChild.RemoveAt(index);
            //    //        lei.LinkType.RemoveAt(index+1);
            //    //        lei.LinkType.RemoveAt(index);
            //    //    }

            //    //    else
            //    //        index++;
            //    //}

            //    //lei.LinkItemChild.Add(leiAltera);
            //    //lei.LinkType.Add(linkAltera);

            //}


        }





    }
}
