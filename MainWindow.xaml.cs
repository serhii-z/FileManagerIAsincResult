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
using System.IO;

namespace FileMenagerIAsincResult
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileManager fm = new FileManager();
        string s;
        TextBlock temp2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DelDirectory_Click(object sender, RoutedEventArgs e)
        {
            treeView.Items.Clear();
            string path = pathTxtBox.Text + @"\" + nameTxtBox.Text;
            fm.DeleteDirectory(path);
            ShowTree();
        }

        private void CreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            treeView.Items.Clear();
            string path = pathTxtBox.Text + @"\" + nameTxtBox.Text;
            fm.CreateDirectory(path);
            ShowTree();
        }

        private void DelFile_Click(object sender, RoutedEventArgs e)
        {
            treeView.Items.Clear();
            string path = pathTxtBox.Text + @"\" + nameTxtBox.Text;
            fm.DeleteFile(path);
            ShowTree();
        }


        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TreeViewItem)treeView.SelectedItem).IsSelected = false;
            }
            catch { }
            treeView.Items.Clear();
            string path = pathTxtBox.Text + @"\" + nameTxtBox.Text;
            fm.CreateFile(path);
            pathTxtBox.Text = path;
            ShowTree();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            treeView.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            ShowTree();
        }

        private void CliarBtn_Click(object sender, RoutedEventArgs e)
        {
            treeView.Items.Clear();
            pathTxtBox.Text = "";
            nameTxtBox.Text = "";
            readTextBox.Text = "";
            writeTextBox.Text = "";
        }

        private void ShowTree()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    if (drive.Name == @"D:\")
                    {
                        TextBlock textBlock = new TextBlock() { Text = drive.Name };
                        TreeViewItem driveGo = new TreeViewItem() { Header = textBlock };
                        treeView.Items.Add(driveGo);
                        fm.ShowTreeView(drive.Name, driveGo);
                    }
                }
                catch { }
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                temp2 = ((TreeViewItem)e.NewValue).Header as TextBlock;
            }
            catch{ }
            pathTxtBox.Text = temp2.Text;
            s = System.IO.Path.GetExtension(pathTxtBox.Text);
            if(s == ".txt")
            {
                var myDelegate = new Func<string, string>(fm.ReadFileTxt);
                IAsyncResult asyncRes = myDelegate.BeginInvoke(pathTxtBox.Text, null, null);
                readTextBox.Text = myDelegate.EndInvoke(asyncRes);
            }
            else
            {
                readTextBox.Text = "";
            }
        }

        private void WriteFile_Click(object sender, RoutedEventArgs e)
        {
            var myDelegate = new Action<string, string>(fm.WriteFileTxt);
            IAsyncResult asyncRes = myDelegate.BeginInvoke(pathTxtBox.Text, writeTextBox.Text, null, null);
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            var myDelegate = new Func<string, string>(fm.ReadFileTxt);
            IAsyncResult asyncRes = myDelegate.BeginInvoke(pathTxtBox.Text, null, null);
            readTextBox.Text = myDelegate.EndInvoke(asyncRes);
        }

        private void ReadTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TreeViewItem)treeView.SelectedItem).IsSelected = false;
        }

        private void WriteTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TreeViewItem)treeView.SelectedItem).IsSelected = false;
        }
    }
}
