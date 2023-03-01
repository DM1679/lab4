using System;
using System.IO;
using System.Linq;

namespace lab4
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 3 || args.Length == 4)
            {
                string funk = args[0];
                string path = args[1];
                string dateTime = args[2];
                DateTime ResultDateTime;

                if (!DateTime.TryParse(dateTime, out ResultDateTime))
                {
                    Console.WriteLine($"Ошибка: неверный формат даты/времени: {dateTime}");
                    return 1;
                }

                switch (funk)
                {
                    case "EditCreationTimeFile":
                        EditCreationTimeFile(path, ResultDateTime);
                        break;
                    case "EditCreationTimeFiles":
                        EditCreationTimeFiles(path, ResultDateTime);
                        break;
                    case "EditCreationTimeFilesMask":
                        string mask = args[3];
                        EditCreationTimeFilesMask(path, ResultDateTime, mask);
                        break;
                    default:
                        Console.WriteLine($"Ошибка: неизвестная команда: {funk}");
                        return 1;
                }

                return 0;
            }
            else if (args.Length == 1 && args[0] == "?")
            {
                Console.WriteLine("Є два режими запуску з параметрами\n1. Запуск с трьома параметрами «lab4.exe \"назва функції\" \"шлях\" \"дата та час\"»\n2. Запуск с чотирма параметрами «lab4.exe \"назва функції\" \"шлях\" \"дата та час\" \"маска\"»");
                return 0;
            }
            else
            {
                Console.WriteLine($"Ошибка: неверное количество параметров: {args.Length}");
                return 1;
            }
        }

        public static void EditCreationTimeFile(string filePath, DateTime dateTime)
        {
            if (File.Exists(filePath))
            {
                File.SetCreationTime(filePath, dateTime);
                File.SetLastWriteTime(filePath, dateTime);
                File.SetLastAccessTime(filePath, dateTime);
            }
            else
            {
                Console.WriteLine($"Ошибка: файл не найден: {filePath}");
            }
        }

        public static void EditCreationTimeFiles(string filePaths, DateTime dateTime)
        {
            if (Directory.Exists(filePaths))
            {
                string[] allfiles = Directory.GetFiles(filePaths);
                foreach (string filePath in allfiles)
                {
                    EditCreationTimeFile(filePath, dateTime);
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: директория не найдена: {filePaths}");
            }
        }

        public static void EditCreationTimeFilesMask(string filePaths, DateTime dateTime, string maskTemp)
        {
            if (Directory.Exists(filePaths))
            {
                string[] allfiles = Directory.GetFiles(filePaths);
                foreach (string filePath in allfiles)
                {
                    string mask = Path.GetExtension(filePath);
                    if (mask == maskTemp)
                    {
                        EditCreationTimeFile(filePath, dateTime);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: директория не найдена: {filePaths}");
            }
        }
    }
}
