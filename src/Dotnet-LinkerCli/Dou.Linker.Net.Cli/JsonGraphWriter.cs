using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dou.Linker.Net.Cli
{
   public class JsonGraphWriter
    {
        public void XMLtoJsonWriter(string line)
        {
            // Get the directories currently on the C drive.
            //  DirectoryInfo[] cDirs = new DirectoryInfo(@"c:\Projects\DOU-Linker\src\JsonOutput").GetDirectories();

          
        
            // Write each directory name to a file.
            using (StreamWriter sw = new StreamWriter(@"C:\Projects\DOU-Linker\src\JsonOutput\output.txt"))
            {           
                sw.WriteLine(line);
                
                
            }

        }

    }
}
