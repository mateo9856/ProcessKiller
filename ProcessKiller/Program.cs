using ProcessKiller.Infrastructure;
using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ProcessKiller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ProcessBuilder().StartProcessByPath("notepad", "C:\\Windows");
            //var App = new Runtime();
            //App.RunNewConsole();
        }

        
    }
}
