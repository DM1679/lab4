using System;
using System.IO;
using System.Threading.Tasks;

namespace lab4
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if(args.Length == 3) 
            {
                string funk = args[0];
                if (funk == "EditCreationTimeFile")
                {
                    string dateTime = args[2];
                    string path = args[1];
                    Console.WriteLine(funk);
                    Console.WriteLine(path);
                    Console.WriteLine(dateTime);
                    DateTime ResultDateTime = GetDate(dateTime);
                    EditCreationTimeFile(path, ResultDateTime);
                }
                if(funk == "EditCreationTimeFiles")
                {
                    string dateTime = args[2];
                    string path = args[1];
                    DateTime ResultDateTime = GetDate(dateTime);
                    EditCreationTimeFiles(path, ResultDateTime);
                }

                return 0;
            }
            else if (args.Length == 4)
            {
                string funk = args[0];
                if (funk == "EditCreationTimeFilesMask")
                {
                    string dateTime = args[2];
                    string path = args[1];
                    string maskTemp = args[3];
                    DateTime ResultDateTime = GetDate(dateTime);
                    EditCreationTimeFilesMask(path, ResultDateTime, maskTemp);
                }

                return 0;
            }
            else if (args.Length == 1)
            {
                string funk = args[0];
                if (funk == "?")
                {
                    Console.WriteLine("Є два режими запуску з параметрами\n1. Запуск с трьома параметрами «lab4.exe \"назва функції\" \"шлях\" \"дата та час\"»\n2. Запуск с чотирма параметрами «lab4.exe \"назва функції\" \"шлях\" \"дата та час\" \"маска\"»");
                }

                return 0;
            }
            else
            {
                string dateTime = "16.06.2030 18:00:00";
                string path = "C:\\Users\\Pioneer\\Desktop\\text.txt";
                DateTime ResultDateTime = GetDate(dateTime);
                EditCreationTimeFile(path, ResultDateTime);
                //string path = "C:\\Users\\Pioneer\\Downloads";
                //EditCreationTimeFiles(path, ResultDateTime);

                return 0;
            }
        }

        public static DateTime GetDate(string dateTime)
        {
            string date = dateTime.Split(" ")[0];
            string time = dateTime.Split(" ")[1];

            int day;
            int.TryParse(date.Split(".")[0], out day);

            int month;
            int.TryParse(date.Split(".")[1], out month);

            int year;
            int.TryParse(date.Split(".")[2], out year);

            int hour;
            int.TryParse(time.Split(":")[0], out hour);
            int minutes;
            int.TryParse(time.Split(":")[1], out minutes);
            int seconds;
            int.TryParse(time.Split(":")[2], out seconds);

            DateTime ResultDateTime = new DateTime(year, month, day, hour, minutes, seconds);
            return ResultDateTime;
        }

        public static void EditCreationTimeFile(string filePath, DateTime dateTime) 
        {
            if (File.Exists(filePath))
            {
                File.SetCreationTime(filePath, dateTime);
                File.SetLastWriteTime(filePath, dateTime);
                File.SetLastAccessTime(filePath, dateTime);
            }
        }

        public static void EditCreationTimeFiles(string filePaths, DateTime dateTime)
        {
            string[] allfiles = Directory.GetFiles(filePaths);
            foreach (string filePath in allfiles) 
            {
                if (File.Exists(filePath))
                {
                    File.SetCreationTime(filePath, dateTime);
                    File.SetLastWriteTime(filePath, dateTime);
                    File.SetLastAccessTime(filePath, dateTime);
                }
            }
        }

        public static void EditCreationTimeFilesMask(string filePaths, DateTime dateTime, string maskTemp)
        {
            string[] allfiles = Directory.GetFiles(filePaths);
            foreach (string filePath in allfiles)
            {
                string mask = ReverseString(filePath);
                mask = mask.Split(".")[0];
                mask = ReverseString(mask);
                if (mask == maskTemp)
                {
                    if (File.Exists(filePath))
                    {
                        File.SetCreationTime(filePath, dateTime);
                        File.SetLastWriteTime(filePath, dateTime);
                        File.SetLastAccessTime(filePath, dateTime);
                    }
                }
            }
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}