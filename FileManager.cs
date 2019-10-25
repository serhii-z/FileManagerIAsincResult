using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace FileMenagerIAsincResult
{
    public class FileManager
    {
        public FileManager() { }

        public void ShowTreeView(string d, TreeViewItem driveGo)
        {
            TreeViewItem folderGo;
            TreeViewItem fileGo;
            try
            {
                string[] directories = Directory.GetDirectories(d);
                foreach (string directory in directories)
                {
                    TextBlock textBlock1 = new TextBlock() { Text = directory };
                    folderGo = new TreeViewItem() { Header = textBlock1 };
                    driveGo.Items.Add(folderGo);
                    ShowTreeView2(directory, folderGo);
                }
                foreach (string i in Directory.GetFiles(d))
                {
                    TextBlock textBlock2 = new TextBlock() { Text = i };
                    fileGo = new TreeViewItem() { Header = textBlock2 };
                    driveGo.Items.Add(fileGo);
                }
            }
            catch { }
        }

        public void ShowTreeView2(string d, TreeViewItem dirGo)
        {
            TreeViewItem folderGo;
            TreeViewItem fileGo;
            try
            {
                string[] directories = Directory.GetDirectories(d);
                foreach (string directory in directories)
                {
                    TextBlock textBlock1 = new TextBlock() { Text = directory };
                    folderGo = new TreeViewItem() { Header = textBlock1 };
                    dirGo.Items.Add(folderGo);
                    ShowTreeView(directory, folderGo);
                }
                foreach (string i in Directory.GetFiles(d))
                {
                    TextBlock textBlock2 = new TextBlock() { Text = i };
                    fileGo = new TreeViewItem() { Header = textBlock2 };
                    dirGo.Items.Add(fileGo);
                }
            }
            catch { }
        }


        public string CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                if (Directory.Exists(path))
                {
                    return path;
                }
            }
            catch { }
            return null;
        }

        public string DeleteDirectory(string path)
        {
            try
            {
                Directory.Delete(path);
                if (Directory.Exists(path))
                {
                    return path;
                }
            }
            catch { }
            return null;
        }

        public string CreateFile(string path)
        {
            try
            {
                File.Create(path);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            catch { }
            return null;
        }

        public string DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            catch { }
            return null;
        }

        public string ReadFileTxt(string path)
        {
            string s;
            using (StreamReader sr = new StreamReader(path))
            {
                s = sr.ReadToEnd();
            }
            return s;
        }

        public void WriteFileTxt(string path, string text)
        {
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(text);
                //sw.Write(4.5);
            }
        }
    }
}