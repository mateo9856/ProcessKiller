using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessKiller.Infrastructure
{
    public class ProcessSearch
    {
        private Thread[] FolderThreads { get; set; }
        private string PathDir { get; set; } = "";
        private bool lockThread { get; set; } = false;

        public void GetDrives()
        {
            var Drives = Directory.GetLogicalDrives();
            foreach(var Drive in Drives)
            {
                Console.WriteLine(Drive);
            }
        }

        public string FindProcessByNameAndDrive(string name, string drive)
        {
            string ActualPath = drive;

            var GetThreads = Environment.ProcessorCount;

            var DriveDirectory = Directory.GetFileSystemEntries(drive, "*.*", SearchOption.TopDirectoryOnly);
            FolderThreads = new Thread[DriveDirectory.Length];
            lockThread = true;
            for(int i = 0; i < DriveDirectory.Length; i++)
            {
                ActualPath = DriveDirectory[i];
                FolderThreads[i] = new Thread(() => ResearchFolder(ActualPath, name));
                FolderThreads[i].Start();
            }
            while(!PathDir.Contains(name))
            {

            }
            lockThread = false;
            return PathDir;
        }

        private bool ResearchFolder(string path, string name)
        {
            var reg = new Regex(@"[^\\]+$");
            while (lockThread == true)
            {
                try
                {
                    string[] CheckFiles = Directory.GetFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly)
                        .OrderBy(d => d.Length)
                        .OrderByDescending(d => d.Contains(".exe"))
                        .ToArray();
                    
                    foreach (var CheckFile in CheckFiles)
                    {
                        if (CheckFile.Contains(name))
                        {
                            PathDir = CheckFile;
                            ClearThreads(FolderThreads);
                            return true;
                        }

                        try
                        {
                            ResearchFolder(CheckFile, name);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(Thread.CurrentThread.Name);
                        }
                    }
                }
                catch (Exception)
                {
                }


                return false;
            }
            return true;
        }
        private void ClearThreads(Thread[] threads)
        {
            lockThread = false;
            foreach (var thread in threads)
            {
                thread.Abort();
            }
            FolderThreads = null;
        }

    }
}
