﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dou.Linker.Net.Cli.Models
{
   public class Linker
    {
        public string LinkItemParent { get; set; } = "";
        public List<string> LinkItemChild { get; set; } = new List<string>();
        public List<string> LinkType { get; set; } = new List<string>();

    }
}
