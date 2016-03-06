using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AlpineGPGSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            StringBuilder sb = new StringBuilder();
            bool inEncryptedBlock = false;
            int i = 0;
            while ((s = Console.ReadLine()) != null)
            {
                if (!inEncryptedBlock && !s.StartsWith("-----BEGIN PGP"))
                {

                    Console.WriteLine(i.ToString() + ">" + s);
                }
                else if (s.StartsWith("-----BEGIN PGP"))
                {
                    inEncryptedBlock = true;
                    sb = new StringBuilder();
                    sb.AppendLine(s);
                }
                else if (inEncryptedBlock)
                {
                    sb.AppendLine(s);
                    if (s.StartsWith("-----END PGP"))
                    {
                        inEncryptedBlock = false;
                        try
                        {
                            if (Properties.Settings.Default.ShowWarningBlock) {
                                Console.WriteLine("===== Start AlpineGPGSharp =====");
                            }
                            
                            string tempFile = Path.GetTempFileName();
                            File.WriteAllText(tempFile, sb.ToString());
                            ProcessStartInfo si = new ProcessStartInfo();
                            si.FileName = Properties.Settings.Default.GPGPath;
                            si.Arguments = @"-d " + tempFile;
                            si.CreateNoWindow = true;
                            si.UseShellExecute = false;
                            si.RedirectStandardInput = true;
                            si.RedirectStandardOutput = true;
                            si.RedirectStandardError = true;
                            si.WorkingDirectory = Path.GetTempPath();

                            Process process = Process.Start(si);
                            process.WaitForExit();
                            System.Console.Write(process.StandardOutput.ReadToEnd());
                            process.Close();

                            if (Properties.Settings.Default.ShowWarningBlock)
                            {
                                Console.WriteLine("===== Original Message =====");
                            }
                                if (Properties.Settings.Default.ShowOriginal)
                            {
                                Console.WriteLine(sb.ToString());
                            }

                            if (Properties.Settings.Default.ShowWarningBlock)
                            {
                                Console.WriteLine("===== End AlpineGPGSharp =====");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);
                            if (Properties.Settings.Default.ShowWarningBlock)
                            {
                                Console.WriteLine("===== End AlpineGPGSharp =====");
                            }
                            Console.Write(sb.ToString());

                        }
                    }
                }
                i++;
            }

        }
    }
}
