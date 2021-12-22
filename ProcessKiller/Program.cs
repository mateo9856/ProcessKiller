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
            new ProcessSearch().FindProcessByNameAndDrive("notepad", "C:\\");//try sort by first file not directories
            //var App = new Runtime();
            //App.RunNewConsole();
        }

        
    }
}
