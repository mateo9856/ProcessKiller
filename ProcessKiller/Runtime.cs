using ProcessKiller.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace ProcessKiller
{
    public class Runtime : IDisposable
    {
        private static ProcessTimer timer;
        private static int val;
        static bool SaveHistory = false;
        struct ReadValues
        {
            public int SelectedId;
            public string SelectedName;
            public int[] SelectedMultiple;
        }
        private static ProcessList ProcessList = new ProcessList();
        private static ReadValues values = new ReadValues();
        public void RunNewConsole()
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
            Console.WriteLine("5. Search Process");
            Console.WriteLine("6. Read History");

            try
            {
                val = Int32.Parse(Console.ReadLine());
                if (val < 4)
                {
                    GetValueChanger(ref val);
                }
                else if(val == 4) 
                {
                    Console.WriteLine("Enter process or id");
                    var GetDetails = ProcessList.FindProcess(Console.ReadLine());

                    Console.WriteLine(GetDetails);

                }
                else if(val == 5)
                {
                    var search = new ProcessSearch();
                    Console.WriteLine("Select your drive and type name:");
                    search.GetDrives();
                    string drive = Console.ReadLine();
                    Console.WriteLine("Enter process name:");
                    string process = Console.ReadLine();
                    Console.WriteLine(search.FindProcessByNameAndDrive(process, drive));
                }
                else
                {
                    ProcessHistory ph = new ProcessHistory();
                    Console.WriteLine("Open in notepad? press T!");
                    if (Console.ReadLine() == "T")
                        ph.ReadHistory(true);
                    else
                        ph.ReadHistory(false);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error Press X to Restart");
                if (Console.ReadLine() == "X")
                {
                    Console.Clear();
                    this.Dispose();
                    RunNewConsole();
                }
            }
        }

        public static void GetValueChanger(ref int val)
        {
            timer = new ProcessTimer();

            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Console.WriteLine("Enter time interval from seconds");
            timer.SetIntervalTime(Double.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Value/Values:");
            switch (val)
            {
                case 1:
                    values.SelectedId = Convert.ToInt32(Console.ReadLine());
                    break;
                case 2:
                    values.SelectedName = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter id's after the space");
                    string Values = Console.ReadLine();
                    var SplitArr = Values.Split(' ');
                    values.SelectedMultiple = Array.ConvertAll(SplitArr, d => Int32.Parse(d));
                    break;
            }
            Console.WriteLine("Are you save history? press y if you agree");
            
            if (Console.ReadLine() == "t")
                SaveHistory = true;
            else
                SaveHistory = false;

            timer.StartInterval();
            Console.WriteLine("Press any key to stop kill!");
            Console.ReadKey();
        }

        private static void Timer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                switch(val)
                {
                    case 1:
                        ProcessList.KillProcessById(values.SelectedId, SaveHistory);
                        break;
                    case 2:
                        ProcessList.KillByProcessName(values.SelectedName, SaveHistory);
                        break;
                    case 3:
                        ProcessList.KillMultipleProcess(values.SelectedMultiple, SaveHistory);
                        break;
                    default:
                        Console.WriteLine("Bad value!");
                        Environment.Exit(-1);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fatal Error! App is Stopped");
                Environment.Exit(-1);
            }

        }

        public void Dispose()
        {
            timer = null;
            ProcessList = null;
            GC.SuppressFinalize(this);
        }
    }
}
