using System;
using System.IO;

namespace DelegatesBasicExample
{
    class Program
    {
        delegate void LogDel(string text);
        //delegate void LogDel(string text, DateTime dateTime);
        static void Main(string[] args)
        {
            //LogDel logDel = new LogDel(LogTextToScreen);
            //LogDel logDel = new LogDel(LogTextToFile);

            Log log = new Log();

            LogDel LogTextToScreenDel, LogTextToFileDel;
            //LogDel logDel = new LogDel(log.LogTextToScreen);
            //LogDel logDel = new LogDel(log.LogTextToFile);

            LogTextToScreenDel = new LogDel(log.LogTextToScreen);
            LogTextToFileDel = new LogDel(log.LogTextToFile);

            LogDel multiLogDel = LogTextToScreenDel + LogTextToFileDel;
            //logDel("Abrakadabra");
            //logDel.Invoke("Alamakota");


            Console.WriteLine("Please write some text");

            var text = Console.ReadLine();
            //logDel(text, DateTime.Now);

            //logDel(text);
            //multiLogDel(text);
            //LogText(multiLogDel, text);
            //LogText(LogTextToScreenDel, text);
            LogText(LogTextToFileDel, text);

            Console.ReadLine();
        }

        static void LogText(LogDel logDel, string text)
        {
            logDel(text);
        }

        //static void LogTextToScreen(string text)
        ////static void LogTextToScreen(string text, DateTime dateTime)
        //{
        //    //Console.WriteLine($"{DateTime.Now}: {text}");
        //    Console.WriteLine($"{DateTime.Now}: {text}");
        //}

        //static void LogTextToFile(string text)
        //{
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_log.txt"), true))
        //    {
        //        sw.WriteLine($"{DateTime.Now}: {text}");
        //    }
        //}
    }

    public class Log
    {
        public void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");
        }

        public void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
        }
        ////static void LogTextToScreen(string text, DateTime dateTime)
        //{
        //    //Console.WriteLine($"{DateTime.Now}: {text}");
        //    Console.WriteLine($"{DateTime.Now}: {text}");
        //}

        //static void LogTextToFile(string text)
        //{
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_log.txt"), true))
        //    {
        //        sw.WriteLine($"{DateTime.Now}: {text}");
        //    }
        //}

    }
}
