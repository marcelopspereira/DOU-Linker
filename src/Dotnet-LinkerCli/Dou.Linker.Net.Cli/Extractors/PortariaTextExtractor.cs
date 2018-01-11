using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Dou.Linker.Net.Cli.Models;

namespace Dou.Linker.Net.Cli
{
    public class PortariaTextExtractor
    {

        // public static List<string> Portaria.Child { get; set; } = new List<string>();
        public static List<string> ActionPortaria { get; set; } = new List<string>();

        public string portariaValue = "";
        public string linkValue = "Citado";

        public Portaria portaria = new Portaria();


        public void FindTitlePortaria(string ArticleTitle)
        {
            var pattern = @"(Portaria nº(s)?(-)?|Portaria no(s)?(-)?) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var matchResult = rgx.Match(ArticleTitle);

            if (matchResult.Success==true)
            {
                 var result = matchResult.Value;

                //Tratamento do titulo

                portaria.Name = result;

            }





        }


            public void FindBodyPortaria(string ArticleBody)
        {
            var pattern = @"(Portaria nº(s)?(-)?|Portaria no(s)?(-)?) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList
                  

            foreach (Match match in matches)
            {
                var cleanMatchValue = match.Value;                        

                var regex = new Regex(@"([0-9]+(\.[0-9]+)?(\-[0-9]+)?)");
                var portariaMatch = regex.Match(cleanMatchValue);

                if (portariaMatch.Success==true)
                {
                    portariaValue = portariaMatch.Value;
                }
             
                portaria.Child.Add(portariaValue);

            }
            
        }

        public void FindPortariaTraceability(string text)
        {

            for (var i = 0; i < portaria.Child.Count; i++)
            {
                portaria.LinkItemChild.Add("Portaria " + portaria.Child[i] + ";" + linkValue);
            }

            portaria.LinkItemParent = portaria.Name;


            //Usar o regex para capturar o Verbo Altera e o ponto final, após a captura deverei verificar se existe alguma lei dentro dela (Olhar a Lista de Strings já capturada)


            var pattern = @"Altera(.*)(Revoga|.)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(text);

            //////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matches)
            {

                if (match.Success)
                {
                   for (var i = 0; i<portaria.LinkItemChild.Count;i++)
                   {
                      if(match.Value.Contains(portaria.Child[i])==true)
                        {
                            var linkAltera = "Altera";

                            portaria.LinkItemChild[i] = "Portaria " + portaria.Child[i] + ";" + linkAltera;                          
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
                    for (var i = 0; i < portaria.LinkItemChild.Count; i++)
                    {
                        if (match.Value.Contains(portaria.Child[i]) == true)
                        {                        

                                var linkAltera = "Revoga";

                                portaria.LinkItemChild[i] = "Portaria " + portaria.Child[i] + ";" + linkAltera;
                               
                        }

                    }

                }

            }


            var patternOthers = @"(Lei no|Lei nº|Decreto no|Decreto-Lei no|Ato|Portaria) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgxOthers = new Regex(patternOthers, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matchesOthers = rgxOthers.Matches(text);

            //////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matchesOthers)
            {

                if (match.Success)
                {
                    for (var i = 0; i < portaria.LinkItemChild.Count; i++)
                    {
                        if (match.Value.Contains(portaria.Child[i]) == true)
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

                            portaria.LinkItemChild[i] = itemLinkName + portaria.Child[i] + ";" + linkAltera;

                        }

                    }

                }

            }






            portaria.LinkItemChild.Sort();

            int index = 0;
            while (index < portaria.LinkItemChild.Count - 1)
            {
                if (portaria.LinkItemChild[index] == portaria.LinkItemChild[index + 1])
                {
                    var LeiAltera = portaria.LinkItemChild[index];
                    portaria.LinkItemChild.RemoveAt(index);                  
                }

                else
                    index++;
           
            }

            portaria.LinkItemChild.Sort();
        }


        public void PrintResults()
        {
            Console.WriteLine(portaria.Name);

            //Console.WriteLine("\n");


            ////Impressao das Leis filhas

            //for (var i = 0; i < lei.Child.Count; i++)
            //    Console.WriteLine(lei.Child[i]);


            Console.WriteLine("\n");

            //Mostrando a rastreabilidade entre itens

            for (var i = 0; i < portaria.LinkItemChild.Count; i++)
            {
                Console.WriteLine(portaria.LinkItemChild[i]);
            }

        }

    }
}
