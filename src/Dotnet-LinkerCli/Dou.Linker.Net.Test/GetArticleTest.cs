using System;
using Xunit;
using Dou.Linker.Net.Cli;

namespace Dou.Linker.Net.Test
{
    public class ArticleTest
    {
        [Fact]     
        public void GetArticleTest()
        {
            var file = @"C:\Projects\DOU-Linker\src\XmlSamples\Sample14.xml";
            var processor = new XmlReader();
            

            processor.ReadXmlFile(file);

            Assert.Equal(@"PORTARIA No- 13, DE 10 DE NOVEMBRO DE 2017", XmlArticleLayout.ArticleTitle);

        }



    }
}
