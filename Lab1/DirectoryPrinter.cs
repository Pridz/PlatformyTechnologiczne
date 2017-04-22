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
        private bool isDirectory(String path)
        {
            return System.IO.Directory.Exists(path);
        }

        private bool isFile(String path)
        {
            return System.IO.File.Exists(path);
        }

        public void printDirectory(String path)
        {
            if (isDirectory(path))
            {

            }
            else if (isFile(path))
            {

            }
        }

        public void print()
        {

        }
    }
}
