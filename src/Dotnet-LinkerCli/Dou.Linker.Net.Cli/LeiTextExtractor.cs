using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Dou.Linker.Net.Cli.Models;

namespace Dou.Linker.Net.Cli
{
    public class LeiTextExtractor
    {

        public static string TitleLei { get; set; }

        public static string BodyLei { get; set; }

        // public static List<string> lei.Child { get; set; } = new List<string>();
        public static List<string> ActionLei { get; set; } = new List<string>();

        public string leiValue = "";
        public string linkValue = "Citado";

        public Lei lei = new Lei();


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
            var pattern = @"(Lei nº|Lei(s)? no(s)?) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList
                  

            foreach (Match match in matches)
            {
                var cleanMatchValue = match.Value;                        

                var regex = new Regex(@"([0-9]+(\.[0-9]+)?(\-[0-9]+)?)");
                var leiMatch = regex.Match(cleanMatchValue);

                if (leiMatch.Success)
                {
                    leiValue = leiMatch.Value;
                }
             
                lei.Child.Add(leiValue);

            }
            
        }

        public void FindLeiTraceability(string text)
        {

            for (var i = 0; i < lei.Child.Count; i++)
            {
                lei.LinkItemChild.Add("Lei " + lei.Child[i] + ";" + linkValue);
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

                                var linkAltera = "Revoga";

                                lei.LinkItemChild[i] = "Lei " + lei.Child[i] + ";" + linkAltera;
                               
                        }

                    }

                }

            }


            var patternOthers = @"(Decreto no|Decreto-Lei no|Ato|Portaria) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgxOthers = new Regex(patternOthers, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matchesOthers = rgxOthers.Matches(text);

            //////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matchesOthers)
            {

                if (match.Success)
                {
                    for (var i = 0; i < lei.LinkItemChild.Count; i++)
                    {
                        if (match.Value.Contains(lei.Child[i]) == true)
                        {
                            var fullString = match.Value;

                            string itemLinkName = "";

                            if (fullString.Contains("Decreto-Lei no")==true)
                            {
                               itemLinkName = fullString.Substring(0, 12);
                            }
                            else
                            if (fullString.Contains("Decreto") == true)
                            {
                                itemLinkName = fullString.Substring(0, 7);
                            }


                            var linkAltera = "Citado";

                            lei.LinkItemChild[i] = itemLinkName + lei.Child[i] + ";" + linkAltera;

                        }

                    }

                }

            }






            lei.LinkItemChild.Sort();

            int index = 0;
            while (index < lei.LinkItemChild.Count - 1)
            {
                if (lei.LinkItemChild[index] == lei.LinkItemChild[index + 1])
                {
                    var LeiAltera = lei.LinkItemChild[index];
                    lei.LinkItemChild.RemoveAt(index);                  
                }

                else
                    index++;
           
            }

            lei.LinkItemChild.Sort();
        }


        public void PrintResults()
        {
            Console.WriteLine(lei.Name);

            //Console.WriteLine("\n");


            ////Impressao das Leis filhas

            //for (var i = 0; i < lei.Child.Count; i++)
            //    Console.WriteLine(lei.Child[i]);


            Console.WriteLine("\n");

            //Mostrando a rastreabilidade entre itens

            for (var i = 0; i < lei.LinkItemChild.Count; i++)
            {
                Console.WriteLine(lei.LinkItemChild[i]);
            }

        }

    }
}
