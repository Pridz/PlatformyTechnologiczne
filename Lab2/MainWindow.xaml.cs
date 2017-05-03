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
            folderBrowser.ShowDialog();
            DirectoryInfo dirInfo = new DirectoryInfo(folderBrowser.SelectedPath);
            makeTree(dirInfo);
            //var root = new TreeViewItem
            //{
            //    Header = dirInfo.Name,
            //    Tag = dirInfo.FullName
            //};

            //treeView.Items.Add(root);
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
                Header = dirInfo.Name,
                Tag = dirInfo.FullName
            };
            treeView.Items.Add(root);
            DirectoryInfo[] dirsInfo = dirInfo.GetDirectories();
            foreach (var dir in dirsInfo)
            {
                var item = new TreeViewItem
                {
                    Header = dir.Name,
                    Tag = dir.FullName
                };
                makeSubTree(dir, root);
            }
            FileInfo[] filesInfo = dirInfo.GetFiles();
            foreach (var file in filesInfo)
            {
                var item = new TreeViewItem
                {
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
                Header = dirInfo.Name,
                Tag = dirInfo.FullName
            };
            parent.Items.Add(root);
            DirectoryInfo[] dirsInfo = dirInfo.GetDirectories();
            foreach (var dir in dirsInfo)
            {
                var item = new TreeViewItem
                {
                    Header = dir.Name,
                    Tag = dir.FullName
                };
                root.Items.Add(item);
                makeSubTree(dir, root);
            }
            FileInfo[] filesInfo = dirInfo.GetFiles();
            foreach (var file in filesInfo)
            {
                var item = new TreeViewItem
                {
                    Header = file.Name,
                    Tag = file.FullName
                };
                root.Items.Add(item);
            }
        }
    }
}
