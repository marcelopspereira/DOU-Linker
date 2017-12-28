using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dou.Linker.Net.Cli
{
    public class Linker
    {
        public static string ArticleTitle { get; set; }
        public static string ArticleBody { get; set; }

        public static string TitleLei { get; set; }

        public static string BodyLei { get; set; }

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
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
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
            var pattern = @"(Lei nº|Lei no) ([0-9]+(\.[0-9]+)?(\-[0-9]+)?)";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(ArticleBody);

            BodyLei = "";

                foreach (Match match in matches)
            {
                BodyLei += "->" + match.Value + "\n";


            }
                


        }





    }
}
