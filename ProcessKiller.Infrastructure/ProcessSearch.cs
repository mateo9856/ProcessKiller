using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessKiller.Infrastructure
{
    public class ProcessSearch
    {
        private HashSet<string> DirectoriesTree = new HashSet<string>();

        public void FindProcessByNameAndDrive(string name, string drive)
        {
            string ActualPath = drive;
            var DriveDirectory = Directory.GetFileSystemEntries(drive, "*.*", SearchOption.TopDirectoryOnly);
            int DirectoryIndex = 0;
            while(!ActualPath.Contains(name))
            {
                try
                {
                    ActualPath = DriveDirectory[DirectoryIndex];
                    var IsSearched = ResearchFolder(ActualPath, name);
                } catch(Exception)
                {

                }
                DirectoryIndex++;
            }
            Console.WriteLine();
        }

        private bool ResearchFolder(string path, string name)
        {//folder searched!!! add only condition if file is search!!
            //add threading!!
            var CheckFiles = Directory.GetFileSystemEntries(path, "*.*", SearchOption.TopDirectoryOnly);
            foreach(var CheckFile in CheckFiles)
            {

                if(CheckFile.Contains(name))
                {
                    return true;
                }

                try
                {
                    ResearchFolder(CheckFile, name);
                    Console.WriteLine(CheckFile);
                }
                catch(Exception)
                {

                }
            }

            return false;

        }

    }
}
