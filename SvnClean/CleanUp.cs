using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SvnClean
{
    public class CleanUp
    {
        public static void Run(string directory)
        {
            Console.WriteLine("SVN cleaning directory {0}", directory);

            List<string> notVersioned = GetNotVersionedItems(directory);

            foreach (string notVersionedItem in notVersioned)
            {
                if (Directory.Exists(notVersionedItem))
                {
                    Directory.Delete(notVersionedItem, true);
                }
                else if (File.Exists(notVersionedItem))
                {
                    File.Delete(notVersionedItem);
                }
            }
        }

        public static List<string> GetNotVersionedItems(string directory)
        {
            var notVersionedItems = new List<string>();
            Directory.SetCurrentDirectory(directory);

            var psi = new ProcessStartInfo("svn.exe", "status --non-interactive --depth=infinity --no-ignore");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.WorkingDirectory = directory;

            using (var process = Process.Start(psi))
            {
                string line = process.StandardOutput.ReadLine();
                while (line != null)
                {
                    if (line.Length > 7)
                    {
                        if (line[0] == '?' || line[0] == 'I')
                        {
                            string relativePath = line.Substring(7).Trim();
                            notVersionedItems.Add(Path.Combine(directory, relativePath));
                        }
                    }
                    line = process.StandardOutput.ReadLine();
                }
            }

            return notVersionedItems;
        }
    }
}
