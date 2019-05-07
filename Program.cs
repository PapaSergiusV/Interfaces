using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Program
    {
        static void PrintInfoFiles(string path, DateTime ftime, DateTime etime)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                FileInfo fileInf = new FileInfo(file);
                if (fileInf.CreationTime >= ftime && fileInf.CreationTime <= etime)
                {
                    Console.WriteLine("{0,30} |{1,20} |{2,11}", fileInf.Name , fileInf.CreationTimeUtc, fileInf.Length);
                }
            }
        }

    static void Main(string[] args)
        {
            Console.WriteLine("Программа CONSOLE6\n");

            string path;
            DateTime firstTime;
            DateTime endTime;

            if (args.Length == 0)
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Каталог: " + path + "\n");
                Console.WriteLine("{0,30} |{1,20} |{2,11}", "File Name", "Creation Time", "File size");
                Console.WriteLine(new String('=', 65));
                firstTime = new DateTime(1, 1, 1, 0, 0, 0);
                endTime = new DateTime(9999, 12, 31, 23, 59, 59);
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + args[0];
                var ftime = args[1].Split(' ', '.', ':');
                var etime = args[2].Split(' ', '.', ':');
                firstTime = new DateTime(int.Parse(ftime[2]), int.Parse(ftime[1]), int.Parse(ftime[0]), int.Parse(ftime[3]), int.Parse(ftime[4]), 0);
                endTime = new DateTime(int.Parse(etime[2]), int.Parse(etime[1]), int.Parse(etime[0]), int.Parse(etime[3]), int.Parse(etime[4]), 59);
                Console.WriteLine("Промежуток времени: " + firstTime + " - " + endTime);
                Console.WriteLine("Каталог: " + path + "\n");
                Console.WriteLine("{0,30} |{1,20} |{2,11}", "File Name", "Creation Time", "File size");
                Console.WriteLine(new String('=', 65));
            }

            PrintInfoFiles(path, firstTime, endTime);

            Console.WriteLine("\nДля завершения программы нажмите<Enter>...");
            Console.ReadLine();
        }
    }
}
