using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessKiller.Infrastructure
{
    public class ExceptionLoader
    {
        public static Dictionary<int, string> Exceptions = new Dictionary<int, string>();

        public void AddExceptionProcessFromRuntime(string processName)
        {
            var GetProcess = Process.GetProcessesByName(processName);
            foreach (var item in GetProcess)
            {
                Exceptions.Add(item.Id, item.ProcessName);
            }
        }

        public void RemoveExceptionProcessFromRuntime(string processName)
        {
            var GetProcessName = Exceptions.Where(d => d.Value.Equals(processName)).Select(d=> d.Key);
        
            foreach(var item in GetProcessName)
            {
                Exceptions.Remove(item);
            }
        }

        public void AddExceptionProcessToFile(string processName)
        {
            var GetProcess = Process.GetProcessesByName(processName);
            bool IsFileExist = false;
            var TextPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ProcessExceptions.txt");

            if (File.Exists(TextPath))
            {
                IsFileExist = true;
            }

            using(StreamWriter sw = new StreamWriter(TextPath, IsFileExist))
            {
                foreach(var item in GetProcess)
                {
                    sw.WriteLine($"{item.ProcessName} {item.Id}");
                }
                sw.Flush();
            }

        }

        public void LoadExceptionFromFile()
        {
            var TextPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ProcessExceptions.txt");
            if(File.Exists(TextPath))
            {
                using (StreamReader sr = new StreamReader(TextPath))
                {
                    string s;
                    while((s = sr.ReadLine()) != null)
                    {
                        var ValuesToArr = s.Split(' ');
                        Exceptions.Add(Int32.Parse(ValuesToArr[1]), ValuesToArr[0]);
                    }
                }
            }
        }

        public void RemoveExceptionProcessToFile(string processName)
        {
            var tempFile = Path.GetTempFileName();

            var TextPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ProcessExceptions.txt");
            if(File.Exists(TextPath))
            {
                var lines = File.ReadAllLines(TextPath).Where(d => !d.Contains(processName));
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    foreach(var item in lines)
                    {
                        sw.WriteLine(item);
                    }
                }

                File.Delete(TextPath);
                File.Move(tempFile, TextPath);
            }
        }
    }
}
