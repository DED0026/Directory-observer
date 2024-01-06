using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Directory_observer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text.EndsWith("\\")|| textBox1.Text.EndsWith("/")))
            {
                textBox1.Text += "\\";
            }
            reload();
        }


        FileSystemWatcher Folderwatcher = new FileSystemWatcher();
        public void reload()
        {
            if (Directory.Exists(textBox1.Text))
            {
                textBox1.Text = Path.GetFullPath(textBox1.Text);

                listBox1.Items.Clear();
                listBox1.Items.Add("..");
                foreach (string file in Directory.GetFiles(textBox1.Text))
                    listBox1.Items.Add(file.Remove(0, textBox1.TextLength).Trim('\\'));
                foreach (string directory in Directory.GetDirectories(textBox1.Text))
                    listBox1.Items.Add(directory.Remove(0, textBox1.TextLength).Trim('\\'));
            }
            else
            {
                MessageBox.Show("directory doesnt exist, backtracking.");
                textBox1.Text = Path.GetDirectoryName(textBox1.Text);
                reload();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Directory.Exists(textBox1.Text) ? Color.Green : Color.Red;
        }

        int LastIndex;

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == LastIndex)
            {
                string selection = textBox1.Text + "\\" + listBox1.SelectedItem.ToString();
                if (Directory.Exists(selection))
                {
                    textBox1.Text = selection;
                    reload();
                }
                else
                {
                    ProcessStartInfo Launchedprocess = new ProcessStartInfo();
                    Launchedprocess.WorkingDirectory = Path.GetDirectoryName(selection);
                    Launchedprocess.FileName = selection;
                    Process.Start(Launchedprocess);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LastIndex = listBox1.SelectedIndex;
        }
    }
}
