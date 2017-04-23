using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class DirectoryPrinter
    {
        private String path;

        public DirectoryPrinter(String path)
        {
            this.path = path;
        }
        private bool isDirectory()
        {
            return System.IO.Directory.Exists(path);
        }

        private bool isFile()
        {
            return System.IO.File.Exists(path);
        }

        public void printDirectory(String path)
        {
            if (isDirectory())
            {
                print(path);
                String[] paths = System.IO.Directory.GetDirectories(path);
                foreach (String dir in paths)
                {
                    printDirectory(dir);
                }
                String[] files = System.IO.Directory.GetFiles(path);
                foreach (String file in files)
                {
                    print(file);
                }
            }
            else if (isFile())
            {
                printInformationAboutFilePath();
            }
            else
            {
                printInformationAboutNotExistingPath();
            }
        }

        public void print(String path)
        {
            Console.WriteLine(path);
        }

        public void printInformationAboutFilePath()
        {
            Console.WriteLine("Wrong input! File is under this path! End of program...");
        }

        public void printInformationAboutNotExistingPath()
        {
            Console.WriteLine("Such path doesn't exist! End of program...");
        }
    }
}
