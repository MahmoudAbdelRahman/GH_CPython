using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GH_CPython
{
    class InitFunctions
    {
        public bool recall = false;
        public string isPythonFloderExists(string dir)
        {
            string path = "";
            if (!Directory.Exists(@dir))
            {
                Directory.CreateDirectory(@dir);
                path = @dir;
            }
            else
            {
                path = @dir;
            }
            return path;
        }

        public string getPythonInterpretere(string datfile)
        {
            if (!File.Exists(@datfile))
            {
                string defaultFileName = "Python.exe";
                //MessageBox.Show("Not exists");
                if (File.Exists(@"C:\Python27\python.exe")) { defaultFileName = @"C:\Python27\python.exe"; }
                else if (File.Exists(@"C:\Anaconda\python.exe")) { defaultFileName = @"C:\Anaconda\python.exe"; }
                else if (File.Exists(@"C:\Python34\python.exe")) { defaultFileName = @"C:\Python34\python.exe"; }
                else if (File.Exists(@"C:\Python35\python.exe")) { defaultFileName = @"C:\Python35\python.exe"; }
                else if (File.Exists(@"C:\Python36\python.exe")) { defaultFileName = @"C:\Python36\python.exe"; }

                else if (File.Exists(@"C:\Program Files (x86)\Python27\python.exe")) { defaultFileName = @"C:\Program Files (x86)\Python27\python.exe"; }
                else if (File.Exists(@"C:\Program Files (x86)\Python34\python.exe")) { defaultFileName = @"C:\Program Files (x86)\Python34\python.exe"; }
                else if (File.Exists(@"C:\Program Files (x86)\Python35\python.exe")) { defaultFileName = @"C:\Program Files (x86)\Python35\python.exe"; }
                else if (File.Exists(@"C:\Program Files (x86)\Python36\python.exe")) { defaultFileName = @"C:\Program Files (x86)\Python36\python.exe"; }

                else if (File.Exists(@"C:\Program Files\Python27\python.exe")) { defaultFileName = @"C:\Program Files\Python27\python.exe"; }
                else if (File.Exists(@"C:\Program Files\Python34\python.exe")) { defaultFileName = @"C:\Program Files\Python34\python.exe"; }
                else if (File.Exists(@"C:\Program Files\Python35\python.exe")) { defaultFileName = @"C:\Program Files\Python35\python.exe"; }
                else if (File.Exists(@"C:\Program Files\Python36\python.exe")) { defaultFileName = @"C:\Program Files\Python36\python.exe"; }
                File.WriteAllText(@datfile, defaultFileName);
                
                return @defaultFileName;
                
            }
            else
            {
                if (File.ReadAllText(@datfile).Trim() != String.Empty)
                {
                    
                    string StartFileName = File.ReadAllText(@datfile);
                    //MessageBox.Show("Exists at\n"+StartFileName);
                    return @StartFileName;
                }
                else
                {
                    locatePython locPy = new locatePython();
                    locPy.ShowDialog();
                    try
                    {
                        string StartFileName = File.ReadAllText(@datfile);
                        return @StartFileName;
                    }catch
                    {
                        MessageBox.Show("Sorry, We can't find Python installed on your machine, If you have already installed it, would you contact Mahmoud Abdlerahman via this e-mail \n arch.mahmoud.ouf111@gmail.com\n Thanks.");
                        return @"Python.exe";
                    }
                }

            }
        }

        public bool addGrasshopperPyModule(string fileName, string pythonVersion)
        {
            if (!File.Exists(@fileName))
            {
                File.WriteAllText(@fileName, Resources.RhinoLibs.Grasshopper);
            }
            else
            {
                string ver = File.ReadAllLines(@fileName)[0];
                if (ver != pythonVersion)
                {
                    File.WriteAllText(@fileName, Resources.RhinoLibs.Grasshopper);
                }
            }
            return true;
        }
    }
}
