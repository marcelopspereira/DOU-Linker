using System;
using System.Collections.Generic;
using System.Text;

namespace Dou.Linker.Net.Cli.Models
{
    public class LeiCollection
    {
        public List<Lei> Leis;
    }

    public class Lei
    {
        public string Name { get; set; }
        public List<string> Child { get; set; } = new List<string>();
        public Linker[] Linker { get; set; }
    }


}