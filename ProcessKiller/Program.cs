using ProcessKiller.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ProcessKiller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var App = new Runtime();
            App.RunNewConsole();
        }

        
    }
}
