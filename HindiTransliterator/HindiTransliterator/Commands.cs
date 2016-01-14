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
        bool IsSaved = false;                   //File is saved
        bool IsOpenDocSaved = false;            //Current file exists on system (i.e. it has been saved previously on system)
        string curFileName = "Untitled";
        string curFileNameWithPath = "Untitled";

        private void SetCommandBindings()
        {
            CommandBinding cmdBindingNew = new CommandBinding(ApplicationCommands.New);
            cmdBindingNew.Executed += NewCommandHandler;
            CommandBindings.Add(cmdBindingNew);

            CommandBinding cmdBindingOpen = new CommandBinding(ApplicationCommands.Open);
            cmdBindingOpen.Executed += OpenCommandHandler;
            CommandBindings.Add(cmdBindingOpen);

            CommandBinding cmdBindingSave = new CommandBinding(ApplicationCommands.Save);
            cmdBindingSave.Executed += SaveCommandHandler;
            CommandBindings.Add(cmdBindingSave);

            CommandBinding cmdBindingSaveAs = new CommandBinding(ApplicationCommands.SaveAs);
            cmdBindingSaveAs.Executed += SaveAsCommandHandler;
            CommandBindings.Add(cmdBindingSaveAs);
        }

        private void NewCommandHandler(object sender, RoutedEventArgs e)
        {
            if (IsSaved == false)
            {
                MessageBoxResult result = MessageBox.Show(this, "Save changes to current document?", "Save Current Document", MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                    Save();
                else if (result == MessageBoxResult.No)
                    New();
                else
                    return;
            }
            else
                New();
        }

        private void New()
        {
            rtb.Document.Blocks.Clear();            //Clears all data in current doc
            rtb.Document.Blocks.Add(new Paragraph(new Run()));
            curFileName = "Untitled";
            curFileNameWithPath = "Untitled";
            IsOpenDocSaved = false;
            IsSaved = true;
        }

        private void OpenCommandHandler(object sender, RoutedEventArgs e)
        {
            if (IsSaved == false)
            {
                MessageBoxResult result = MessageBox.Show(this, "Save changes to current document?", "Save Current Document", MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                    Save();
                else if (result == MessageBoxResult.No)
                    Open();
                else
                    return;
            }
            else
                Open();

        }

        public void Open()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Untitled"; // Default file name
            dlg.DefaultExt = ".rtf"; // Default file extension
            dlg.Filter = "Rich Text Documents (.rtf)|*.rtf"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

                TextRange range;
                FileStream fStream;
                if (File.Exists(filename))
                {
                    range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    fStream = new FileStream(filename, FileMode.OpenOrCreate);
                    range.Load(fStream, DataFormats.Rtf);
                    fStream.Close();
                }
            }
        }

        private void SaveCommandHandler(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAsCommandHandler(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Save()
        {
            if (IsOpenDocSaved == true)
            {
                TextRange range;
                FileStream fStream;
                range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                fStream = new FileStream(curFileNameWithPath, FileMode.Open);
                range.Save(fStream, DataFormats.Rtf);
                fStream.Close();

                IsSaved = true;
            }
            else
                SaveAs();
        }

        private void SaveAs()
        {
            SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = curFileName;     // Default file name
            dlg.DefaultExt = ".rtf";        // Default file extension
            dlg.Filter = "Rich Text Documents (.rtf)|*.rtf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                TextRange range;
                FileStream fStream;
                range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                fStream = new FileStream(filename, FileMode.Create);
                range.Save(fStream, DataFormats.Rtf);
                fStream.Close();

                curFileName = dlg.SafeFileName;
                curFileNameWithPath = dlg.FileName;
                IsOpenDocSaved = true;
                IsSaved = true;
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (IsSaved == false)
            //{
            //    MessageBoxResult result = MessageBox.Show(this, "Save changes to current document?", "Save Current Document", MessageBoxButton.YesNoCancel);

            //    if (result == MessageBoxResult.Yes)
            //        Save();
            //    else if (result == MessageBoxResult.No)
            //        Application.Current.Shutdown();
            //    else
            //        e.Cancel = true;
            //}
            //else
            Application.Current.Shutdown();
        }

    }
}