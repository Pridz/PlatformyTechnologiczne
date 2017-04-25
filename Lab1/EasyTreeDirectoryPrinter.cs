using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    class EasyTreeDirectoryPrinter
    {
        private string path;
        private int space;
        private string rootPath;
        public EasyTreeDirectoryPrinter(string path)       
        {
            this.path = path;
            space = 0;
            rootPath = Directory.GetParent(path).ToString();
        }

        public EasyTreeDirectoryPrinter(string path, int space)
        {
            this.path = path;
            this.space = space;
            rootPath = System.IO.Directory.GetParent(path).ToString();
        }

        public void printDirectory(String path)
        {
            if (isDirectory())
            {
                string name = removePathFrom(path);
                print(name);
                printInteriorOf(path, space);
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

        public void printInteriorDirectory(String path, string rootPath, int nameLengthOfBackFolder)
        {
            if (isDirectory())
            {
                string name = insertSpaces(removeFrom(path, rootPath), nameLengthOfBackFolder);
                print(name);
                printInteriorOf(path, nameLengthOfBackFolder + space);
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

        private void printInteriorOf(String path, int nameLengthOfBackFolder)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            String[] paths = Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);
            int filesCount = paths.Length + files.Length;
            printLine(" " + filesCount.ToString() + " " + directory.getAttributes());
            foreach (String dir in paths)
            {
                printInteriorDirectory(dir, path, nameLengthOfBackFolder);
            }
            foreach (String file in files)
            {
                printFileNameWithSpaces(file, path, nameLengthOfBackFolder);
            }
        }

        private String removePathFrom(String thisOne)
        {
            return thisOne.Remove(0, rootPath.Length);
        }

        private string insertSpaces(string inThisOne, int amountOfSpaces)
        {
            char[] newString = new char[inThisOne.Length + amountOfSpaces];
            for (int i = 0; i < amountOfSpaces; i++)
			{
                newString[i] = Convert.ToChar(" ");
			}
            char[] copy = inThisOne.ToCharArray();
            for (int i = amountOfSpaces, j = 0; i < newString.Length; i++, j++)
			{
                newString[i] = copy[j];
			}

            return new string(newString);
        }

        private void printFileNameWithSpaces(string path, string rootPath, int nameLengthOfBackFolder)
        {
            FileInfo file = new FileInfo(path); 
            printLine(insertSpaces(removeFrom(path, rootPath), nameLengthOfBackFolder + space) + " " + file.getAttributes() + " " + file.Length + " bajtow");
        }

        private void printFirstFolderName(string path)
        {
            printLine(removePathFrom(path));
        }

        private void printFolderName(string pathToFolderName, string rootPath)
        {
            printLine(insertSpaces(removeFrom(pathToFolderName, rootPath),space));
        }

        private String removeFrom(string path, string rootPath)
        {
            return path.Remove(0, rootPath.Length);
        }

        private bool isDirectory()
        {
            return Directory.Exists(path);
        }

        private bool isFile()
        {
            return File.Exists(path);
        }

        private void printInformationAboutFilePath()
        {
            Console.WriteLine("Wrong input! File is under this path! End of program...");
        }

        private void printInformationAboutNotExistingPath()
        {
            Console.WriteLine("Such path doesn't exist! End of program...");
        }

        public void print(String path)
        {
            Console.Write(path + space);
        }

        public void printLine(string path)
        {
            Console.Write(path + "\n");
        }
            
    }
    
}
