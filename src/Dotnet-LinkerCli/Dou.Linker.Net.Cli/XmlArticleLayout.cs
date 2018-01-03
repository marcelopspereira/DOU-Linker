using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dou.Linker.Net.Cli
{
   public class XmlArticleLayout
    {
        public static string ArticleTitle { get; set; }
        public static string ArticleBody { get; set; }
        public static string ArticleCaput { get; set; }


        public void FindTitleArticle(string article)
        {
            var pattern = @"<title>(.*)</title>";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rgx.Matches(article);


            if (matches.Count > 0)
            {
                Match match = matches[0];


                //Tratamento do titulo

                ArticleTitle = match.Value;
                ArticleTitle = ArticleTitle.Replace("<title>", "");
                ArticleTitle = ArticleTitle.Replace("</title>", "");

            }

        }


        public void FindBodyArticle(string article)
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


        public void FindCaputArticle(string article)
        {
            var pattern = @"<caput>(.*)</caput>";
            Regex rgx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

            MatchCollection matches = rgx.Matches(article);


            if (matches.Count > 0)
            {
                Match match = matches[0];


                //Tratamento do titulo

                ArticleCaput = match.Value;
                ArticleCaput = ArticleCaput.Replace("<caput>", "");
                ArticleCaput = ArticleCaput.Replace("</caput>", "");

            }




        }













    }
}
