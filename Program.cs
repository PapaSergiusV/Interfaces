using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
// using System.Windows.Forms;

namespace Interfaces
{
    class Program
    {
        static void PrintFilesTree(string path, string mask, int depth = 0)
        {
            string[] files = Directory.GetFiles(path).OrderBy(x => x).ToArray();

            foreach (string file in files)
                if (Regex.IsMatch(file, @"\w+"))
                {
                    FileInfo fileInf = new FileInfo(file);
                    Console.WriteLine($"{new String(' ', 4 * depth)}|-{fileInf.Name} - {fileInf.Length} bytes");
                }
            string[] directories = Directory.GetDirectories(path);
            if (directories.Length == 0)
                return;
            else
                foreach (string dir in directories)
                {
                    Console.WriteLine($"{new String(' ', 4 * depth)}[]{Regex.Match(dir, @"/[\w\s\d]+$").Value}");
                    PrintFilesTree(dir, mask, depth + 1);
                }
        }

        static void Main(string[] args)
        {
            string path;

            if (args.Length == 0)
                path = AppDomain.CurrentDomain.BaseDirectory;
            else
                path = AppDomain.CurrentDomain.BaseDirectory + args[0];
            
            Console.WriteLine($"Каталог: {path}\n{new String('=', 90)}");

            PrintFilesTree(path, args.Length > 1 ? args[1] : @"*.*");

            Console.WriteLine("\nPress any key for exit");
            Console.ReadKey();
        }
    }
}
