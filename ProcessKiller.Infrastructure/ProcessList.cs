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
        
        public void AddExceptionProcess(string processName)
        {
            var GetProcess = Process.GetProcessesByName(processName);
            foreach(var item in GetProcess)
            {//think how add exception processes
                SystemProcesses.Exceptions.Add(item.Id, item.ProcessName);
            }
        }

        public void KillProcessById(int id)
        {
            var GetProcess = Process.GetProcessById(id);
            SystemProcesses.KillProcesses.Add(GetProcess.Id, GetProcess.MainModule.FileName);
            GetProcess.Kill();
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

        public void KillByProcessName(string name)
        {
            var Get = Process.GetProcessesByName(name);
            foreach(var item in Get)
            {
                SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                item.Kill();
            }
        }

        public void KillMultipleProcess(int[] processes)
        {
            var GetProcesses = Process.GetProcesses().Where(d => processes.Contains(d.Id));

            foreach(var item in GetProcesses)
            {
                SystemProcesses.KillProcesses.Add(item.Id, item.MainModule.FileName);
                item.Kill();
            }

        }

    }
}
