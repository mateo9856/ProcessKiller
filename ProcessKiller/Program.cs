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
        private static dynamic IntervalMethod;
        public static void Main(string[] args)
        {
            RunNewConsole();
        }

        public static void RunNewConsole()
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
            Console.WriteLine("4. FindProcess");
            int val = Int32.Parse(Console.ReadLine());

            try
            {
                if (val < 4)
                {
                    GetValueChanger(ref val);
                }
                else
                {
                    Console.WriteLine("Enter process or id");
                    var GetDetails = ProcessList.FindProcess(Console.ReadLine());

                    Console.WriteLine(GetDetails);

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error Press X to Restart");
                if (Console.ReadLine() == "X")
                {
                    Console.Clear();
                    RunNewConsole();
                }
            }
        }

        public static void GetValueChanger(ref int val)
        {
            var ProcessList = new ProcessList();
            timer = new ProcessTimer();
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Console.WriteLine("Enter time interval from seconds");
            timer.SetIntervalTime(Double.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Value/Values:");
            switch (val)
            {
                case 1:
                    IntervalMethod = ProcessList.KillProcessById(Int32.Parse(Console.ReadLine()));
                    break;
                case 2:
                    IntervalMethod = ProcessList.KillByProcessName(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Enter id's after the space");
                    string Values = Console.ReadLine();
                    var SplitArr = Values.Split(' ');
                    IntervalMethod = ProcessList.KillMultipleProcess(Array.ConvertAll(SplitArr, d=> Int32.Parse(d)));
                    break;
            }
            timer.StartInterval();
            Console.WriteLine("Press any key to stop kill!");
            Console.ReadKey();
        }

        private static void Timer_Elapsed(object source, ElapsedEventArgs e)
        {
            IntervalMethod();//error in run method;
        }
    }
}
