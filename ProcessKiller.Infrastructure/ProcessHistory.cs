using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProcessKiller.Infrastructure
{
    public class ProcessHistory : IDisposable
    {
        public void WriteHistory(string processName, int id)
        {
            var GetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var FolderPath = Path.Combine(GetDirectory, "ProcessKillerPath", "history.txt");

            using(StreamWriter sw = new StreamWriter(FolderPath))
            {
                 sw.WriteLine($"{processName} {id} {DateTime.Now}");
            }
        }
        public string ReadHistory(bool OtherWindow)
        {
            var GetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var FolderPath = Path.Combine(GetDirectory, "ProcessKillerPath", "history.txt");
            if(File.Exists(FolderPath))
            {
                if(OtherWindow)
                {
                    Process.Start(FolderPath);
                }
                else
                {
                    string line = "";
                    using (StreamReader streamReader = new StreamReader(FolderPath))
                    {
                        while((line = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }

                return "opened!";

            }

            return "history.txt not found!";
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
