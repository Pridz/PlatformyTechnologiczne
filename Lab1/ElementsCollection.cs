using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    class ElementsCollection
    {
        private string path;
        SortedDictionary<string, int> dictionary;

        public ElementsCollection(string path)
        {
            this.path = path;
            dictionary = new SortedDictionary<string, int>( new Sorter());
        }

        public void addToDictionary(string nameOfElement, int sizeOfElement)
        {
            dictionary.Add(nameOfElement, sizeOfElement);
        }

        public void addElementsFromPath()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] paths = dirInfo.GetDirectories();
            FileInfo[] files = dirInfo.GetFiles();            
            foreach (DirectoryInfo dir in paths)
            {
                addToDictionary(dir.Name, countElements(dir));
            }
            foreach (FileInfo file in files)
            {
                addToDictionary(file.Name, (int)file.Length);
            }
        }

        private int countElements(DirectoryInfo path)
        {
            DirectoryInfo[] paths = path.GetDirectories();
            FileInfo[] files = path.GetFiles();
            int filesCount = paths.Length + files.Length;
            return filesCount;
        }
    }
}
