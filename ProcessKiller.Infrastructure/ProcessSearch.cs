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
        public void FindProcessByNameAndDrive(string name, string drive)
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
        }

        private bool ResearchFolder(string path, string name)
        {
            var reg = new Regex(@"[^\\]+$");
            while (lockThread == true)
            {
                try
                {
                    string[] CheckFiles = Directory.GetFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly).ToArray();
                    
                    if(CheckFiles.Any(d => d.Contains(".")))
                    {
                        CheckFiles = CheckFiles.OrderBy(d => reg.IsMatch(d)).ToArray();
                    }//think how resolve problem with sort by files

                    foreach (var CheckFile in CheckFiles)
                    {
                        Console.WriteLine(CheckFile);
                        if (CheckFile.Contains(name))
                        {
                            Console.WriteLine("FIND!!!!" + " " + name);
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
                    Console.WriteLine("ERROR");
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
