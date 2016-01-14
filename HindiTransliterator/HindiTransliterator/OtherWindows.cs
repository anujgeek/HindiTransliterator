using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace HindiTransliterator
{
    public partial class MainWindow : Window
    {
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            About myAbout = new About();
            myAbout.ShowDialog();
        }
    }
}