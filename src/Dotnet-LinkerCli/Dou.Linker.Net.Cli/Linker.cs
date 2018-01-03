using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dou.Linker.Net.Cli
{
    public class Linker
    {

        public static string TitleLei { get; set; }

        public static string BodyLei { get; set; }

        public static List<string> IDLei { get; set; } = new List<string>();
        public static List<string> ActionLei { get; set; } = new List<string>();



        public void FindTitleLei(string ArticleTitle)
        {
            var pattern = @"(Lei nº|Lei no) (.*).";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleTitle);

            Match match = matches[0];

            //Tratamento do titulo

            TitleLei = match.Value;
            

        }


        public void FindBodyLei(string ArticleBody)
        {
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)(.*)."; //REMOVER (.*). CASO NÃO FUNCIONE NO CENÁRIO DE BATCH
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

                foreach (Match match in matches)
            {
            
                IDLei.Add(match.Value);
         
            }

         
            IDLei = IDLei.Distinct().ToList();
            
        }

        public void FindLeiTraceability(string ArticleCaput)
        {

            //Usar o regex para capturar o Verbo Altera e o ponto final, após a captura deverei verificar se existe alguma lei dentro dela (Olhar a Lista de Strings já capturada)


              var pattern = @"(Altera)(.*).";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleCaput);

            

            ////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matches)
            {

               ActionLei.Add(match.Value);
        
            }

            //Deixa elementos unicos dentro do array e limpa nulos

            ActionLei = ActionLei.Distinct().ToList();

        }





    }
}
