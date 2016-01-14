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
        //Note: First PreviewKeyDown, then KeyDown, Then PreviewTextInput ,then TextInput(character is written according to textinput from kb on rtb) ,then PreviewKeyUp and then KeyUp.

        private void rtb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) || (e.Key == Key.Tab) || (e.Key == Key.Return))
            {
                if (this[-1] == '\u007E')                                          //If previous char is a continuation char and any space char are inseerted, removes the continuation char.
                {
                    RemoveChar(1);
                }
            }
        }

        private void rtb_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void rtb_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void rtb_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void rtb_KeyUp(object sender, KeyEventArgs e)
        {
            rtb.IsReadOnly = false;
        }

        private void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSaved = false;
        }
    }
}