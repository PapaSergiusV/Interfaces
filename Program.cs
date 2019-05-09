using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Console4
{
    class Program
    {
        // Рекурсивная функция построения дерева каталогов
        static void PrintFilesTree(string path, Regex mask, int depth = 0)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(path).OrderBy(x => x).ToArray();
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Папка {path} не найдена");
                return;
            }

            foreach (string file in files)
                if (mask.IsMatch(file))
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
                    Console.WriteLine($"{new String(' ', 4 * depth)}[]{Regex.Match(dir, @"[\w\s\d]+$").Value}");
                    PrintFilesTree(dir, mask, depth + 1);
                }
        }

        // Преобразует маску в регулярное выражение
        static public Regex HandleMask(string mask)
        {
            string[] exts = mask.Split('|', ',', ';');
            string pattern = string.Empty;
            foreach (string ext in exts)
            {
                pattern += @"^";
                foreach (char symbol in ext)
                    switch (symbol)
                    {
                        case '.': pattern += @"\."; break;
                        case '?': pattern += @"."; break;
                        case '*': pattern += @".*"; break;
                        default: pattern += symbol; break;
                    }
                pattern += @"$|";
            }
            pattern = pattern.Remove(pattern.Length - 1);
            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        static void Main(string[] args)
        {
            string path;

            Console.WriteLine("\nПараметры вызова: program.exe folder mask");
            Console.WriteLine("Пример вызова: program.exe [\"folder/\" \"*.json\"]");

            if (args.Length == 0)
                path = AppDomain.CurrentDomain.BaseDirectory;
            else
                path = AppDomain.CurrentDomain.BaseDirectory + args[0];

            Console.WriteLine($"\nКаталог: {path}\n{new String('=', 90)}");

            PrintFilesTree(path, HandleMask(args.Length > 1 ? args[1] : @"*.*"));

            //Console.WriteLine("\nPress any key for exit");
            //Console.ReadKey();
        }
    }
}
