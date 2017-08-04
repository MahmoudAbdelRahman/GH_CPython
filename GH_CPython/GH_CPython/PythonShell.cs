/***
    BSD 2-Clause License

    Copyright (c) 2017, Mahmoud M. AbdelRahman
 *  arch.mahmoud.ouf111@gmail.com
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this
      list of conditions and the following disclaimer.

    * Redistributions in binary form must reproduce the above copyright notice,
      this list of conditions and the following disclaimer in the documentation
      and/or other materials provided with the distribution.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
    FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
    OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 ***/

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


        private void comments(string word, Color color, int startIndex)
        {

            if (this.PythonCanvas.Text.Contains(word))
            {
                string[] commentSeparated = this.PythonCanvas.Text.Split(new string[] { word }, System.StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    for (int i = 1; i < commentSeparated.Length; i += 2)
                    {
                        int nofStrings = commentSeparated[i].Length;
                        if (this.PythonCanvas.Text.Contains(commentSeparated[i]))
                        {
                            int index = -1;
                            int selectStart = this.PythonCanvas.SelectionStart;

                            while ((index = this.PythonCanvas.Text.IndexOf(commentSeparated[i], (index + 1))) != -1)
                            {
                                this.PythonCanvas.Select((index + startIndex), nofStrings);
                                this.PythonCanvas.SelectionFont = new Font(PythonCanvas.Font, FontStyle.Italic);
                                this.PythonCanvas.SelectionColor = color;
                                this.PythonCanvas.Select(selectStart, 0);
                                this.PythonCanvas.SelectionColor = Color.Black;
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
