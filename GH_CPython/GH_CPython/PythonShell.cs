using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GH_CPython
{
    public partial class PythonShell : Form
    {
        public List<string> PsinputData = new List<string>();
        public PythonShell()
        {
            InitializeComponent();
        }

        
        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.CheckKeyword("import ", Color.Salmon, 0);
            this.CheckKeyword("from ", Color.Salmon, 0);
            this.CheckKeyword(" as ", Color.Salmon, 0);
            this.CheckKeyword(" in ", Color.Salmon, 0);

            this.CheckKeyword("print ", Color.Cyan, 0);

            this.CheckKeyword("def ", Color.YellowGreen, 0);
            this.CheckKeyword("if ", Color.YellowGreen, 0);
            this.CheckKeyword("for ", Color.YellowGreen, 0);
            this.CheckKeyword("while ", Color.YellowGreen, 0);
            this.CheckKeyword("if ", Color.YellowGreen, 0);
            this.CheckKeyword("do ", Color.YellowGreen, 0);
            this.CheckKeyword(" range", Color.YellowGreen, 0);
            this.indent(":\n", 0);

        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.PythonCanvas.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.PythonCanvas.SelectionStart;

                while ((index = this.PythonCanvas.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.PythonCanvas.Select((index + startIndex), word.Length);
                    this.PythonCanvas.SelectionColor = color;
                    this.PythonCanvas.Select(selectStart, 0);
                    this.PythonCanvas.SelectionColor = Color.White;
                }
            }
        }

        private void indent(string word, int startIndex)
        {
            if (this.PythonCanvas.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.PythonCanvas.SelectionStart;
                while ((index = this.PythonCanvas.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.PythonCanvas.Select((index + startIndex), word.Length);
                    if (this.PythonCanvas.SelectedText == word)
                    {
                        this.PythonCanvas.SelectedText = ":  \n    ";
                    }
                }

            }
        }

        private void Test_Click(object sender, EventArgs e)
        {
            /*output = "";
            string name = DateTime.Now.ToString("yyyyMMddhhmmssff");
            string path = System.IO.Path.GetTempPath();
            System.IO.File.WriteAllText(path + name + ".py", PythonCanvas.Text);
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.FileName = "python.exe";
                p.StartInfo.Arguments = path + name + ".py";
                p.Start();

                // To avoid deadlocks, always read the output stream first and then wait.
                output += p.StandardOutput.ReadToEnd();
                output += p.StandardError.ReadToEnd();
                p.WaitForExit();
            }
            catch (Exception eex)
            {
                output += eex.ToString();
            }
            console.Text = output;
            System.IO.File.Delete(path + name + ".py");

            this.BringToFront();*/
        }


        public string output { get; set; }

        public string theshell { get; set; }

    }
}
