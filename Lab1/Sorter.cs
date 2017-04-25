using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Sorter : IComparer<string>
    {
        public int Compare(string x, string y )
        {
            if (x.Length < y.Length)
            {
                return 1;
            }
            else if (x.Length == y.Length)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
