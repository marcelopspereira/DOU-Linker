﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dou.Linker.Net.Cli
{
    public class Linker
    {
        public static string ArticleTitle { get; set; }
        public static string ArticleBody { get; set; }

        public static string TitleLei { get; set; }

        public static string BodyLei { get; set; }

        public static List<string> IDLeiList { get; set; } = new List<string>(new string[50]);


        public void FindArticleTitle(string article)
        {
            var pattern = @"<title>(.*)</title>";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(article);


            if (matches.Count > 0)
            {         
            Match match = matches[0];


           //Tratamento do titulo

           ArticleTitle = match.Value;
           ArticleTitle = ArticleTitle.Replace("<title>","");
           ArticleTitle = ArticleTitle.Replace("</title>", "");

            }       

        }

        public void FindTitleLei(string ArticleTitle)
        {
            var pattern = @"(Lei nº|Lei no) (.*).";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleTitle);

            Match match = matches[0];

            //Tratamento do titulo

            TitleLei = match.Value;
            

        }



        public void GetBodyArticle(string article)
        {
            var pattern = @"<body>(.*)</body>";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

            MatchCollection matches = rgx.Matches(article);


            if (matches.Count > 0)
            {
                Match match = matches[0];


                //Tratamento do titulo

                ArticleBody = match.Value;
                ArticleBody = ArticleBody.Replace("<body>", "");
                ArticleBody = ArticleBody.Replace("</body>", "");
               
            }




        }

        public void FindBodyLei(string ArticleBody)
        {
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)(.*)."; //REMOVER (.*). CASO NÃO FUNCIONE NO CENÁRIO DE BATCH
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);

            int i = 0;


            //Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

                foreach (Match match in matches)
            {
            
                IDLeiList[i] = match.Value;


                i++;
            }

            //Deixa elementos unicos dentro do array e limpa nulos

            IDLeiList = IDLeiList.Distinct().ToList();
            IDLeiList.RemoveAll(item => item == null);
        }

        public void FindBodyLeiTraceability(string ArticleBody)
        {
            //var pattern = @"(Lei nº|Lei no)(.*)\n";
            //Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //MatchCollection matches = rgx.Matches(ArticleBody);

            //int i = 0;


            ////Preenche a lista de Leis capturadas no body dentro da variavel IDLeiList

            //foreach (Match match in matches)
            //{

            //    IDLeiList[i] = match.Value;


            //    i++;
            //}

            ////Deixa elementos unicos dentro do array e limpa nulos

            //IDLeiList = IDLeiList.Distinct().ToList();
            //IDLeiList.RemoveAll(item => item == null);
        }





    }
}
