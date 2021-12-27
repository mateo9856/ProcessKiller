using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcessKiller;
using System.Timers;

namespace ProcessKiller.Infrastructure
{
    public class ProcessList
    {
        
        public void KillProcessById(int id, bool history)
        {
            try
            {
                var GetProcess = Process.GetProcessById(id);
                SystemProcesses.KillProcesses.Add(GetProcess.Id, GetProcess.MainModule.FileName);
                
                if(history)
                {
                    using(var pHistory = new ProcessHistory())
                    {
                        pHistory.WriteHistory(GetProcess.MainModule.FileName, GetProcess.Id);
                    }
                }

                GetProcess.Kill();
            } catch(Exception)
            {
                Console.WriteLine("Process not found press any key to stop interval!");
            }

        }

        public void StartProcessFromHistory(int id)
        {
            var GetProcess = SystemProcesses.KillProcesses.Where(d => d.Key == id).Select(d => d.Value).First();
            Process.Start(GetProcess);
        }

        public void StartMultipleProcesses(int[] id)
        {
            var GetProcesses = SystemProcesses.KillProcesses.Where(d => id.Contains(d.Key)).Select(d => d.Value);

            foreach(var item in GetProcesses)
            {
                Process.Start(item);
            }
        }

        public void KillByProcessName(string name, bool history)
        {
            try
            {
                var Get = Process.GetProcessesByName(name);
                foreach (var item in Get)
                {
                    SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                    if (history)
                    {
                        using (var pHistory = new ProcessHistory())
                        {
                            pHistory.WriteHistory(item.MainModule.FileName, item.Id);
                        }
                    }
                    item.Kill();
                }
            } catch (Exception)
            {
                Console.WriteLine("Process not found press any key to stop interval");
            }

        }

        public static object FindProcess(object process)
        {
            dynamic GetProcess = null;
            try
            {
                int id = int.Parse(process.ToString()); 
                GetProcess = Process.GetProcessById(id);
            } catch(Exception) {
                GetProcess = Process.GetProcessesByName((string)process).First();
            }
            return new
            {
                ProcessName = (Process)GetProcess.ProcessName,
                ProcessID = (Process)GetProcess.ProcessId, 
                Priority = (Process)GetProcess.BasePriority,
                Path = (Process)GetProcess.MainModule.FileName,
                Version = (Process)GetProcess.MainModule.FileVersionInfo,
                ModuleInfo = (Process)GetProcess.ModuleInfo
            };
        }

        public void KillMultipleProcess(int[] processes, bool history)
        {
            try
            {
                var GetProcesses = Process.GetProcesses().Where(d => processes.Contains(d.Id));

                foreach (var item in GetProcesses)
                {
                    SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                    if (history)
                    {
                        using (var pHistory = new ProcessHistory())
                        {
                            pHistory.WriteHistory(item.MainModule.FileName, item.Id);
                        }
                    }
                    item.Kill();
                }
            } catch(Exception)
            {
                Console.WriteLine("Process not found press any key to stop interval");
            }

        }

    }
}
