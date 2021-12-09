using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;

namespace ProcessKiller.Infrastructure
{
    public class ProcessList
    {
        
        public bool KillProcessById(int id)
        {
            try
            {
                var GetProcess = Process.GetProcessById(id);
                SystemProcesses.KillProcesses.Add(GetProcess.Id, GetProcess.MainModule.FileName);
                GetProcess.Kill();
                return true;
            } catch(Exception)
            {
                return false;
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

        public bool KillByProcessName(string name)
        {
            try
            {
                var Get = Process.GetProcessesByName(name);
                foreach (var item in Get)
                {
                    SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                    item.Kill();
                }
                return true;
            } catch (Exception)
            {
                return false;
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

        public bool KillMultipleProcess(int[] processes)
        {
            try
            {
                var GetProcesses = Process.GetProcesses().Where(d => processes.Contains(d.Id));

                foreach (var item in GetProcesses)
                {
                    SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                    item.Kill();
                }
                return true;
            } catch(Exception)
            {
                return false;
            }

        }

    }
}
