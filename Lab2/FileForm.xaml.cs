﻿using System;
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

namespace Lab2
{
    /// <summary>
    /// Interaction logic for FileForm.xaml
    /// </summary>
    public partial class FileForm : Window
    {
        public FileForm()
        {
            InitializeComponent();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
