using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dou.Linker.Net.Cli
{
    public class XmlReader
    {
        public void ReadXmlFile(string xmlFile)
        {
            try
            {
                // Fará a leitura do XML dos artigos e deverá retornar a lei correspondente do XML através da lógica de Parse.

                using (StreamReader sr = new StreamReader(xmlFile))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("O arquivo nao pode ser lido");
                Console.WriteLine(e.Message);
            }


        }




    }

}
