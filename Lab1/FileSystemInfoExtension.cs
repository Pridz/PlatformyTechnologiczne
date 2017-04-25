using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    public static class FileSystemInfoExtension
    {
        public static void printAttributes(this System.IO.FileSystemInfo fileInfo)
        {
            if (isDirectory(fileInfo.FullName))
            {
                FileAttributes fileAttributes = File.GetAttributes(fileInfo.FullName);
                char[] newString = new char[4];
                for (int i = 0; i < newString.Length; i++)
                {
                    newString[i] = '-';
                }
                if (isReadOnly(fileInfo.FullName))
                {
                    newString[0] = 'r';
                }
                if (isArchive(fileInfo.FullName))
                {
                    newString[1] = 'a';
                }
                if (isHidden(fileInfo.FullName))
                {
                    newString[2] = 'h';
                }
                if (isSystem(fileInfo.FullName))
                {
                    newString[3] = 's';
                }
                string attributes = new string(newString);
                Console.WriteLine(fileInfo.Name + " " + attributes);
            }
            else if (isFile(fileInfo.FullName))
            {
                FileAttributes fileAttributes = File.GetAttributes(fileInfo.FullName);
                char[] newString = new char[4];
                for (int i = 0; i < newString.Length; i++)
                {
                    newString[i] = '-';
                }
                if (isReadOnly(fileInfo.FullName))
                {
                    newString[0] = 'r';
                }
                if (isArchive(fileInfo.FullName))
                {
                    newString[1] = 'a';
                }
                if (isHidden(fileInfo.FullName))
                {
                    newString[2] = 'h';
                }
                if (isSystem(fileInfo.FullName))
                {
                    newString[3] = 's';
                }
                string attributes = new string(newString);
                Console.WriteLine(fileInfo.Name + " " + attributes);
            }
        }

        public static string getAttributes(this FileSystemInfo fileInfo)
        {
            if (isDirectory(fileInfo.FullName))
            {
                FileAttributes fileAttributes = File.GetAttributes(fileInfo.FullName);
                char[] newString = new char[4];
                for (int i = 0; i < newString.Length; i++)
                {
                    newString[i] = '-';
                }
                if (isReadOnly(fileInfo.FullName))
                {
                    newString[0] = 'r';
                }
                if (isArchive(fileInfo.FullName))
                {
                    newString[1] = 'a';
                }
                if (isHidden(fileInfo.FullName))
                {
                    newString[2] = 'h';
                }
                if (isSystem(fileInfo.FullName))
                {
                    newString[3] = 's';
                }
                return new string(newString);
            }
            else if (isFile(fileInfo.FullName))
            {
                FileAttributes fileAttributes = File.GetAttributes(fileInfo.FullName);
                char[] newString = new char[4];
                for (int i = 0; i < newString.Length; i++)
                {
                    newString[i] = '-';
                }
                if (isReadOnly(fileInfo.FullName))
                {
                    newString[0] = 'r';
                }
                if (isArchive(fileInfo.FullName))
                {
                    newString[1] = 'a';
                }
                if (isHidden(fileInfo.FullName))
                {
                    newString[2] = 'h';
                }
                if (isSystem(fileInfo.FullName))
                {
                    newString[3] = 's';
                }
                return new string(newString);                
            }
            return null;
        }

        public static bool isDirectory(string path)
        {
            return Directory.Exists(path);
        }
        public static bool isFile(string path)
        {
            return File.Exists(path);
        }

        public static bool isReadOnly(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
        }

        public static bool isArchive(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
        }

        public static bool isHidden(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        public static bool isSystem(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
        }
    }
}
