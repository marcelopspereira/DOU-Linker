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
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleTitle);

            Match match = matches[0];

            //Tratamento do titulo

            TitleLei = match.Value;
            

        }


        public void FindBodyLei(string ArticleBody)
        {
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)"; //REMOVER (.*). CASO NÃO FUNCIONE NO CENÁRIO DE BATCH
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

                foreach (Match match in matches)
            {

                var cleanMatchValue = match.Value;
                var leiAltera = cleanMatchValue;

                var regex = new Regex(@"([0-9]+(\.[0-9]+)?(\-[0-9]+)?)");
                var leiMatch = regex.Match(cleanMatchValue);

                if (leiMatch.Success)
                {
                    leiAltera = "Lei " + leiMatch;
                }


                IDLei.Add(leiAltera);
         
            }

         
            IDLei = IDLei.Distinct().ToList();
            
        }

        public void FindLeiTraceability(string ArticleCaput)
        {

            //Usar o regex para capturar o Verbo Altera e o ponto final, após a captura deverei verificar se existe alguma lei dentro dela (Olhar a Lista de Strings já capturada)


            var pattern = @"Altera(.*) (Lei nº|Lei no|Leis nos) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleCaput);

            

            ////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            foreach (Match match in matches)
            {
                var cleanMatchValue = match.Value;
                var leiAltera = cleanMatchValue;

                var regex = new Regex(@"([0-9]+(\.[0-9]+)?(\-[0-9]+)?)");
                var leiMatch = regex.Match(cleanMatchValue);

                if (leiMatch.Success)
                {
                    leiAltera = "Lei " + leiMatch ;
                }

                IDLei.Add(leiAltera);



                IDLei.Sort();
                Int32 index = 0;
                while (index < IDLei.Count - 1)
                {
                    if (IDLei[index] == IDLei[index + 1])
                    {
                        IDLei.RemoveAt(index + 1);
                        IDLei.RemoveAt(index);
                    }
                       
                    else
                        index++;
                }

           

                //Adiciona Altera no final do retorno - AQUI DEVERA TER ALTERA OU REVOGA, DEVERA SER NO MESMO METODO PARA EVITAR EXECUCAO DEMASIADA E CLASSIFICACAO, UMA VEZ QUE EU JA CAPTUREI OS ITENS DO BODY, TALVEZ DEVEREI EXECUTAR O CLEAN NO CAPUT E BODY.
                var regexAltera = new Regex("Altera");
                var alteraMatch = regex.Match(cleanMatchValue);

                if (alteraMatch.Success)
                {
                    leiAltera = leiAltera + ";" + "Altera";
                }

                IDLei.Add(leiAltera);

                IDLei = IDLei.Distinct().ToList();
                IDLei.Sort();


            }


        }





    }
}
