using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public static class DirectoryInfoExtension
    {
        public static void findTheOldestFile(this System.IO.DirectoryInfo dirInfo)
        {
            System.IO.DirectoryInfo[] dirs = dirInfo.GetDirectories();
            System.IO.DirectoryInfo theOldestDirectory = null;
            if (dirs.Length != 0)
            {
                theOldestDirectory = dirs[0];
                foreach (System.IO.DirectoryInfo directory in dirs)
                {
                    if (theOldestDirectory.CreationTime > directory.CreationTime)
                    {
                        theOldestDirectory = directory;
                    }
                    findTheOldestFile(directory);
                }

            }
            System.IO.FileInfo[] files = dirInfo.GetFiles();
            System.IO.FileInfo theOldestFile = null;
            if (files.Length != 0)
            {
                theOldestFile = files[0];
                foreach (System.IO.FileInfo file in files)
                {
                    if (theOldestFile.CreationTime > file.CreationTime)
                    {
                        theOldestFile = file;
                    }
                }
            }
            if (theOldestDirectory != null && theOldestFile != null)
            {
                if (theOldestDirectory.CreationTime > theOldestFile.CreationTime)
                {
                    Console.WriteLine(theOldestFile.Name);
                }
                else
                {
                    Console.WriteLine(theOldestDirectory.Name);
                }
            }
            else if (theOldestDirectory == null && theOldestFile != null)
            {
                Console.WriteLine(theOldestFile.Name);
            }
            else if (theOldestDirectory != null && theOldestFile == null)
            {
                Console.WriteLine(theOldestDirectory.Name);
            }
        }        
    }
}
