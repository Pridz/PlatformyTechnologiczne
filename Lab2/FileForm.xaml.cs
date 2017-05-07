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
using System.Windows.Shapes;
using System.IO;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for FileForm.xaml
    /// </summary>
    public partial class FileForm : Window
    {
        private string path;
        private string newFilePath;
        public FileForm()
        {
            InitializeComponent();
        }

        public string getNewFilePath()
        {
            return newFilePath;
        }

        public FileForm(string path)
        {
            this.path = path;
            InitializeComponent();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            string fullPath;
            if (isFileRadioButtonChecked())
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, returnFilePattern()))
                {
                    fullPath = path + '\\' + txtName.Text;
                    File.Create(fullPath);
                    File.SetAttributes(fullPath, getCheckedAttributes(File.GetAttributes(fullPath)));
                    newFilePath = fullPath;
                }
                else
                {
                    setStatusLabelContent("Wrong name given");
                }
            }
            else if (isDirectoryRadioButtonChecked())
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, returnDirectoryPattern()))
                {
                    fullPath = path + '\\' + txtName.Text;
                    Directory.CreateDirectory(fullPath);
                    File.SetAttributes(fullPath, getCheckedAttributes(File.GetAttributes(fullPath)));                    
                    newFilePath = fullPath;
                }
                else
                {
                    setStatusLabelContent("Wrong name given");
                }
            }
            else
            {
                setStatusLabelContent("No File nor Directory radiobutton checked.");
                return;
            }
            this.Close();
        }
        private bool isFileRadioButtonChecked()
        {
            if(fileRadioButton.IsChecked ?? true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string returnFilePattern()
        {
            return "^[a-zA-Z0-9_~-]{1,8}(.(txt|php|html))$";
        }


        private bool isDirectoryRadioButtonChecked()
        {
            if (directoryRadioButton.IsChecked ?? true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string returnDirectoryPattern()
        {
            return "^[a-zA-Z0-9_~-]{1,8}$";
        }

        private FileAttributes getCheckedAttributes(FileAttributes attributes)
        {
            if (isReadOnlyCheckBoxChecked())
            {
                attributes = setReadOnlyAttribute(attributes);
            }
            if (isArchiveCheckBoxChecked())
            {
                attributes = setArchiveAttribute(attributes);
            }
            if (isHiddenCheckBoxChecked())
            {
                attributes = setHiddenAttribute(attributes);
            }
            if (isSystemCheckBoxChecked())
            {
                attributes = setSystemAttribute(attributes);
            }
            return attributes;
        }

        private bool isReadOnlyCheckBoxChecked()
        {
            if (readOnlyCheckBox.IsChecked ?? true)
	        {
                return true;
	        }
            else
	        {
                return false;
	        }            
        }

        private FileAttributes setReadOnlyAttribute(FileAttributes attributes)
        {
            return attributes | FileAttributes.ReadOnly;
        }

        private bool isArchiveCheckBoxChecked()
        {
            if (archiveCheckBox.IsChecked ?? true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private FileAttributes setArchiveAttribute(FileAttributes attributes)
        {
            return attributes | FileAttributes.Archive;
        }

        private bool isHiddenCheckBoxChecked()
        {
            if (hiddenCheckBox.IsChecked ?? true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private FileAttributes setHiddenAttribute(FileAttributes attributes)
        {
            return attributes | FileAttributes.Hidden;
        }

        private bool isSystemCheckBoxChecked()
        {
            if (systemCheckBox.IsChecked ?? true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private FileAttributes setSystemAttribute(FileAttributes attributes)
        {
            return attributes | FileAttributes.System;
        }

        private void setStatusLabelContent(string message)
        {
            statusLabel.Content = message; 
        }
    }
}
