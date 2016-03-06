using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlpineGPGSharp_Encrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = String.Empty;
            StringBuilder sb = new StringBuilder();
            while ((s = Console.ReadLine()) != null)
            {
                sb.AppendLine(s);
            }
            File.AppendAllText(Path.GetTempPath() + "\alpine_debug.txt", sb.ToString());
        }
    }
}
