using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog() { Description = "Select directory to open."};
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folderBrowser.SelectedPath);
                makeTree(dirInfo);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        private void makeTree(DirectoryInfo dirInfo)
        {
            treeView.Items.Clear();
            var root = new TreeViewItem
            {
                ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmDirTreeView"),
                Header = dirInfo.Name,
                Tag = dirInfo.FullName
            };
            treeView.Items.Add(root);
            DirectoryInfo[] dirsInfo = dirInfo.GetDirectories();
            foreach (var dir in dirsInfo)
            {                
                makeSubTree(dir, root);
            }
            FileInfo[] filesInfo = dirInfo.GetFiles();
            foreach (var file in filesInfo)
            {
                var item = new TreeViewItem
                {
                    ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmFileTreeView"),
                    Header = file.Name,
                    Tag = file.FullName
                };
                root.Items.Add(item);
            }
        }

        private void makeSubTree(DirectoryInfo dirInfo, TreeViewItem parent)
        {
            var root = new TreeViewItem
            {
                ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmDirTreeView"),
                Header = dirInfo.Name,
                Tag = dirInfo.FullName
            };
            parent.Items.Add(root);
            DirectoryInfo[] dirsInfo = dirInfo.GetDirectories();
            foreach (var dir in dirsInfo)
            {                
                makeSubTree(dir, root);
            }
            FileInfo[] filesInfo = dirInfo.GetFiles();
            foreach (var file in filesInfo)
            {
                var item = new TreeViewItem
                {
                    ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmFileTreeView"),
                    Header = file.Name,
                    Tag = file.FullName
                };
                root.Items.Add(item);
            }
        }

        private void cmDirCreate_Click(object sender, RoutedEventArgs e)
        {
            FileForm subWindow = new FileForm((string)((TreeViewItem)treeView.SelectedItem).Tag);
            subWindow.ShowDialog();
            string pathToNewFile = subWindow.getNewFilePath();
            if (File.Exists(pathToNewFile))
            {
                FileInfo file = new FileInfo(pathToNewFile);
                var item = new TreeViewItem
                {
                    ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmFileTreeView"),
                    Header = file.Name,
                    Tag = file.FullName
                };
                ((TreeViewItem)treeView.SelectedItem).Items.Add(item);
            }
            else if (Directory.Exists(pathToNewFile))
            {
                DirectoryInfo dir = new DirectoryInfo(pathToNewFile);
                var item = new TreeViewItem
                {
                    ContextMenu = (System.Windows.Controls.ContextMenu)this.FindResource("cmDirTreeView"),
                    Header = dir.Name,
                    Tag = dir.FullName
                };
                ((TreeViewItem)treeView.SelectedItem).Items.Add(item);
            }
        }

        private void cmDirDelete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = findItem((TreeViewItem)treeView.Items.GetItemAt(0));
            if (treeView.SelectedItem == treeView.Items.GetItemAt(0))
            {                
                if (item.Items.Count != 0)
                {
                    deleteInteriorOfDirectory(item);
                }
                treeView.Items.Remove(item);
                removeReadOnlyAttributeIfFileIsReadOnly(item.Tag.ToString());
                Directory.Delete(item.Tag.ToString());
                treeView.Items.Refresh();
            }
            else
            {
                TreeViewItem parent = (TreeViewItem)item.Parent;            
                if (item.Items.Count != 0)
                {
                    deleteInteriorOfDirectory(item);
                }            
                parent.Items.Remove(item);
                removeReadOnlyAttributeIfFileIsReadOnly(item.Tag.ToString());
                Directory.Delete(item.Tag.ToString());
                parent.Items.Refresh();
                parent.UpdateLayout();

            }
            treeView.UpdateLayout();
        }

        private void cmFileDelete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)treeView.SelectedItem;
            TreeViewItem item1 = findItem((TreeViewItem)treeView.Items.GetItemAt(0));
            //TreeViewItem parent = findItemsParent((TreeViewItem)treeView.Items.GetItemAt(0));
            TreeViewItem parent =  (TreeViewItem)item1.Parent;
            parent.Items.Remove(item1);
            removeReadOnlyAttributeIfFileIsReadOnly(item.Tag.ToString());
            File.Delete(item.Tag.ToString());
            parent.Items.Refresh();
            parent.UpdateLayout();
            treeView.UpdateLayout();
            //treeView.Items.Refresh();
            //treeView.UpdateLayout();
            //treeView.Items.Clear();
        }

        private void cmFileOpen_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = ((TreeViewItem)treeView.SelectedItem);           
            try
            {
                using (StreamReader sr = new StreamReader((string)item.Tag))
                {
                    String text = sr.ReadToEnd();
                    openedFileTextBlock.Text = text;
                }
            }
            catch(Exception ex)
            {
                openedFileTextBlock.Text = "Could not read the file";
            }
        }

        private void deleteInteriorOfDirectory(TreeViewItem item)
        {
            while (item.Items.Count != 0)
            {
                TreeViewItem node = (TreeViewItem)item.Items.GetItemAt(0);
                if (Directory.Exists(node.Tag.ToString()))
                {
                    deleteInteriorOfDirectory(node);
                    item.Items.Remove(node);
                    removeReadOnlyAttributeIfFileIsReadOnly(node.Tag.ToString());
                    Directory.Delete(node.Tag.ToString());
                }
                else if (File.Exists(node.Tag.ToString()))
                {
                    item.Items.Remove(node);
                    removeReadOnlyAttributeIfFileIsReadOnly(node.Tag.ToString());
                    File.Delete(node.Tag.ToString());
                }
            }
        }        

        private TreeViewItem findItem(TreeViewItem item)
        {
            if (File.Exists(item.Tag.ToString()))
            {
                if (isSelectedItem(item.Tag.ToString(), (TreeViewItem)treeView.SelectedItem))
                {
                return item;
                }
            }
            else if (Directory.Exists(item.Tag.ToString()))
            {
                if (isSelectedItem(item.Tag.ToString(), (TreeViewItem)treeView.SelectedItem))
                {
                    return item;
                }
                for (int i = 0; i < item.Items.Count; i++)
                {
                    TreeViewItem result = findItem((TreeViewItem)item.Items.GetItemAt(i));
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private bool isSelectedItem(string path, TreeViewItem item) { return ((string)item.Tag) == path; }

        private void removeReadOnlyAttributeIfFileIsReadOnly(string path)
        {
            if (isReadOnly(path))
            {
                removeReadOnlyAttribute(path, File.GetAttributes(path));
            }
        }

        public static bool isReadOnly(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
        }

        private void removeReadOnlyAttribute(string path, FileAttributes attributes)
        {
            attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
            File.SetAttributes(path, attributes);
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private void treeView_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                attributesOfSelectedFile.Text = attributesOfFile((string)((TreeViewItem)treeView.SelectedItem).Tag);
            }
        }

        private string attributesOfFile(string path)
        {
            char[] status = new char[4];
            hyphenCharArray(status);
            if (isReadOnly(path))
            {
                setReadOnlyAttribute(status);
            }
            if (isArchive(path))
            {
                setArchiveAttribute(status);
            }
            if (isSystem(path))
            {
                setSystemAttribute(status);
            }
            if (isHidden(path))
            {
                setHiddenAttribute(status);
            }
            return new string(status);
        }

        private void hyphenCharArray(char[] charArray)
        {
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = '-';
            }
        }

        private void setReadOnlyAttribute(char[] status)
        {
            status[0] = 'r';
        }

        private bool isArchive(string path)
        {
           return (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive;
        }

        private void setArchiveAttribute(char[] status)
        {
            status[1] = 'a';
        }

        private bool isSystem(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.System) == FileAttributes.System;
        }

        private void setSystemAttribute(char[] status)
        {
            status[2] = 's';
        }

        private bool isHidden(string path)
        {
            return (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        private void setHiddenAttribute(char[] status)
        {
            status[3] = 'h';
        }
    }
}
