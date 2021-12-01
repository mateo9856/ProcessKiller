using ProcessKiller.Infrastructure;
using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ProcessKiller
{
    public class Program
    {
        private static ProcessTimer timer;
        public static void Main(string[] args)
        {
            var ListProcess = Process.GetProcesses().Where(d => !SystemProcesses.EnvironmentProcesses.ContainsKey(d.ProcessName)).ToList();

            foreach (var item in ListProcess)
            {
                Console.WriteLine(item.ProcessName + " " + item.Id);
            }

            Console.WriteLine("Select your kill");
            Console.WriteLine("1. KillById");
            Console.WriteLine("2. KillByName");
            Console.WriteLine("3. KillMultipleProcesses");
            int val = Int32.Parse(Console.ReadLine());

            try
            {
                GetValueChanger(ref val);
            } catch(Exception e)
            {
                Console.WriteLine("Error Press X to start");
            }
        }
    
        public static void GetValueChanger(ref int val)
        {
            var ProcessList = new ProcessList();
            timer = new ProcessTimer();
            Console.WriteLine("Enter time interval from seconds");
            timer.SetIntervalTime(Double.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Value/Values:");
            switch (val)
            {
                case 1:
                    ProcessList.KillProcessById(Int32.Parse(Console.ReadLine()));
                    break;
                case 2:
                    ProcessList.KillByProcessName(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Enter id's after the space");
                    string Values = Console.ReadLine();
                    var SplitArr = Values.Split(' ');
                    ProcessList.KillMultipleProcess(Array.ConvertAll(SplitArr, d=> Int32.Parse(d)));
                    break;
            }
        }
    
    }
}
