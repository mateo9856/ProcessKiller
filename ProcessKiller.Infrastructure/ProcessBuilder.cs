using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ProcessKiller.Infrastructure
{
    public class ProcessBuilder
    {
        public void GetProcessTime()
        {

        }

        public string StartProcessByPath(string name, string path)
        {
            var GetProcess = IsProcessExists(name, path);

            if (GetProcess == null)
            {
                return "Process not Exist of this path!";
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = GetProcess;
                process.Start();
            }
            return "Worked!";
        }

        private string IsProcessExists(string name, string path)
        {
            var GetPath = Directory.GetFiles(path);

            foreach(var disk in GetPath)
            {
                if (disk.Contains($"{name}"))
                    return disk;
            }
            return null;
        }

        public void ReloadProcess()
        {

        }

    }
}
