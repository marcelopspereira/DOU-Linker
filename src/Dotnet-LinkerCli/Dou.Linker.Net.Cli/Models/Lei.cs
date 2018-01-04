using System;
using System.Collections.Generic;
using System.Text;

namespace Dou.Linker.Net.Cli.Models
{
    public class LeiCollection
    {
        public IEnumerable<Lei> Leis;
    }

    public class Lei
    {
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Link { get; set; }
        public string LeiFilho { get; set; }
        public Linker[] Linker { get; set; }
    }


}