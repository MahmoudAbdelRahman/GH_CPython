using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace GH_CPython
{
    public enum COLORS
    {
        WHITE,
        DARK,
        PYTHON,
    }

    public partial class PythonShell : Form
    {

        COLORS canColor = COLORS.DARK;
        public List<string> PsinputData = new List<string>();
        public PythonShell()
        {
            InitializeComponent();
        }

        
        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            canColor = COLORS.WHITE;
                this.CheckKeyword("import ", Color.Blue, 0);
                this.CheckKeyword("from ", Color.Blue, 0);
                this.CheckKeyword(" as ", Color.Blue, 0);
                this.CheckKeyword(" in ", Color.Blue, 0);

                this.CheckKeyword("print ", Color.Red, 0);

                this.CheckKeyword("def ", Color.Green, 0);
                this.CheckKeyword("if ", Color.Green, 0);
                this.CheckKeyword("for ", Color.Green, 0);
                this.CheckKeyword("while ", Color.Green, 0);
                this.CheckKeyword("if ", Color.Green, 0);
                this.CheckKeyword("do ", Color.Green, 0);
                this.CheckKeyword(" range", Color.Green, 0);
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
                    this.PythonCanvas.SelectionFont = new Font(PythonCanvas.Font, FontStyle.Bold);
                    this.PythonCanvas.Select(selectStart, 0);
                    this.PythonCanvas.SelectionColor = Color.Black;
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


        public string output { get; set; }

        public string theshell { get; set; }

      
        /*string dric = System.IO.Path.GetTempPath();
        private void textEditorControl1_Load(object sender, EventArgs e)
        {
            File.WriteAllText(dric + @"\CPP-Mode.xshd", Resources.SavedPythonFile.hl);
            FileSyntaxModeProvider fsmp = new FileSyntaxModeProvider(dric);
            if (Directory.Exists(dric))
            {
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                textEditorControl1.SetHighlighting("Python");
            }
        }*/

    }


}
