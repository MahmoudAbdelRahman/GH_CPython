using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GH_CPython
{
    public partial class locatePython : Form
    {
        public locatePython()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Python interpreter|Python.exe";
            openFileDialog1.Title = "Select Python interpreter";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            {
                File.WriteAllText(@"C:\GH_CPython\interpreter.dat", textBox1.Text);
                this.Close();
            }
            
        }

        private void locatePython_Load(object sender, EventArgs e)
        {
            if(File.Exists(@"C:\GH_CPython\interpreter.dat"))
            {
                string tt = File.ReadAllText(@"C:\GH_CPython\interpreter.dat");
                this.textBox2.Text = tt;
                this.textBox1.Text = tt;
            }else
            {
                textBox2.Text = "None specified";
            }
        }



    }
}
