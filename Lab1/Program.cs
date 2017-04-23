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
            Console.WriteLine("Write path to directory to print it's interior.");
            String path = Console.ReadLine();
            Console.WriteLine("");
            DirectoryPrinter printer = new DirectoryPrinter(path);
            printer.printDirectory(path);
        }
    }


}
