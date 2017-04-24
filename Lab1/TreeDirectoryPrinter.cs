using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class TreeDirectoryPrinter : DirectoryPrinter
    {
        private int space;
        private string rootPath;
        public TreeDirectoryPrinter(string path)
        {
            this.path = path;
            space = 0;
            rootPath = System.IO.Directory.GetParent(path).ToString();
        }

        public TreeDirectoryPrinter(string path, int space)
        {
            this.path = path;
            this.space = space;
            rootPath = System.IO.Directory.GetParent(path).ToString();
        }

        public new void printDirectory(String path)
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
            String[] paths = System.IO.Directory.GetDirectories(path);
            foreach (String dir in paths)
            {
                printInteriorDirectory(dir, path, nameLengthOfBackFolder);
            }
            String[] files = System.IO.Directory.GetFiles(path);
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
            print(insertSpaces(removeFrom(path, rootPath), nameLengthOfBackFolder + space));
        }

        private void printFirstFolderName(string path)
        {
            print(removePathFrom(path));
        }

        private void printFolderName(string pathToFolderName, string rootPath)
        {
            print(insertSpaces(removeFrom(pathToFolderName, rootPath),space));
        }

        private String removeFrom(string path, string rootPath)
        {
            return path.Remove(0, rootPath.Length);
        }
    }
}
