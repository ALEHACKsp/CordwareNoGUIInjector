using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordwareNoGUIInjector
{
    public static class ConsoleUtils
    {
        public static void SetTitle(string title) => Console.Title = title;

        public static void Log(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Cordware] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"({DateTime.Now.ToShortTimeString()}) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");
        }
        public static void LogError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[Cordware] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"({DateTime.Now.ToShortTimeString()}) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");
        }
    }
}
