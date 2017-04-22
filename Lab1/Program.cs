using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory dir;
            Console.WriteLine("Write path to directory to print it's interior.");
            String path = Console.ReadLine();
            dir = new Directory(path);
            if (System.IO.Directory.Exists(path))
            {

            }
            if (System.IO.File.Exists(path))
            {

            }
        }
    }


}
