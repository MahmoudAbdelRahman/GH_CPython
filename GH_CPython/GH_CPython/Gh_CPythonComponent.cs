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

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Parameters;
using Grasshopper.GUI.Canvas;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Grasshopper.Kernel.Data;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace GH_CPython
{

    public class Gh_CPythonComponent : GH_Component, IGH_VariableParameterComponent
    {
        PythonShell PythonIDE;

        GH_Document docUment;

        Process RunningPythonProcess = new Process();

        string path = @"C:\GH_CPython\";

        private string at;

        XmlDocument doc = new XmlDocument();

        public string retrievedData = "";

        public string writtenText = "";

        bool firstTime = true;

        string StartFileName = "python.exe";

        /// <summary>
        /// Constructor 
        /// </summary>
        public Gh_CPythonComponent()
            : base("GH_CPython", "GH_CPython",
                "a python IDE interface",
                "Maths", "Script")
        {

            docUment = this.OnPingDocument();


            if (!Directory.Exists(@"C:\GH_CPython\"))
            {
                Directory.CreateDirectory(@"C:\GH_CPython\");
                path = @"C:\GH_CPython\";
            }
            else
            {
                path = @"C:\GH_CPython\";
            }
            if (!File.Exists(@"C:\GH_CPython\interpreter.dat"))
            {
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
                File.WriteAllText(@"C:\GH_CPython\interpreter.dat", defaultFileName);
                locatePython locPy = new locatePython();
                locPy.ShowDialog();
            }
            else
            {
                if (File.ReadAllText(@"C:\GH_CPython\interpreter.dat").Trim() != String.Empty)
                {
                    StartFileName = File.ReadAllText(@"C:\GH_CPython\interpreter.dat");
                }
                else
                {
                    MessageBox.Show("Sorry, We can't find Python installed on your machine, If you have already installed it, would you contact Mahmoud Abdlerahman via this e-mail \n arch.mahmoud.ouf111@gmail.com\n Thanks.");
                }

            }

            PythonIDE = new PythonShell();
            PythonIDE.TopMost = true;
            //PythonIDE.KeyDown += PythonIDE_KeyDown;
            PreviewExpired += Gh_CPythonComponent_PreviewExpired;

            thisIndex = Globals.index;

            name = "PythonFileWritten_" + thisIndex.ToString();



            Globals.fileName.Add(thisIndex, "_PythonExecutionOrder_" + thisIndex.ToString());
            Globals.index += 1;
            Globals.OpenThisShell.Add(thisIndex, false);


            ///Initiate Python process options.
            ///Don't show Shell - Redirect Standard output - Redirect Standard error - Hide Shell
            RunningPythonProcess.StartInfo.UseShellExecute = false;
            RunningPythonProcess.StartInfo.RedirectStandardOutput = true;
            RunningPythonProcess.StartInfo.RedirectStandardError = true;
            RunningPythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            RunningPythonProcess.StartInfo.UseShellExecute = false;
            RunningPythonProcess.StartInfo.CreateNoWindow = true;


            /// Initiate Python IDE Editor text, it should be like so: - Change if you wish-
            //# -*- coding: utf-8 -*-
            //""" 
            //Python Script
            //Created on  Tuesday August 2017 12:22:25  
            //@author:  UserName 
            //"""

            at = DateTime.Now.ToString("dddd MMMM yyyy hh:mm:ss");
            string Name = System.Environment.UserName;
            InitialPythonText = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
            InitialPythonText = InitialPythonText.Replace("##at##", at);

            try
            {
                /// Initiate Console data as follows
                /// "Hi UserName, How are you ? Are you ready to Change the world ?"
                PythonIDE.console.Text = "Hi " + Name + ", How are you ? Are you ready to Change the world ?";


                /// retrievedData are the data that are saved just after closing the Form (either by clicking x or close)
                /// They are saved here and then retrieved just after reopening the form again.
                if (writtenText != "")
                {
                    PythonIDE.PythonCanvas.Text = writtenText;
                    retrievedData = writtenText;
                }
                else if (retrievedData != "")
                {
                    PythonIDE.PythonCanvas.Text = retrievedData;
                    writtenText = retrievedData;
                }
                else
                {
                    PythonIDE.PythonCanvas.Text = InitialPythonText;
                    retrievedData = InitialPythonText;
                    writtenText = InitialPythonText;

                }

                /// This function reads all the input data, then initiates it in python syntax
                /// this refrers to the present winForm i.e. the python IDE.
                /// writeReaadPythonFile function needs a lot of work to handle different inputs in a proper way
                writeReadPythonFile(this);

                /// EventHandler of the Form Closing
                PythonIDE.FormClosing += Ps_FormClosing;

                /// Handleing Test button click. 
                PythonIDE.Test.Click += (se, ev) =>
                {
                    AddNamesAndDescriptions();
                    //writeReadPythonFile(this);
                    ExpireSolution(true);
                };

                PythonIDE.PythonCanvas.KeyDown += PythonCanvas_KeyDown;
                PythonIDE.close.Click += (se, ev) =>
                {
                    InitialPythonText = PythonIDE.PythonCanvas.Text;
                    shellOpened = false;
                    PythonIDE.Hide();
                    AddNamesAndDescriptions();
                    Globals.OpenThisShell[thisIndex] = false;
                    Grasshopper.Instances.RedrawCanvas();
                };

                Grasshopper.Instances.RedrawCanvas();

            }
            catch (Exception erx)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, erx.ToString());
                //MessageBox.Show(erx.ToString());
            }
            AddNamesAndDescriptions();
        }

        void PythonCanvas_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
                //MessageBox.Show("pressed");
                AddNamesAndDescriptions();
                ExpireSolution(true);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Gh_CPythonComponent_PreviewExpired(IGH_DocumentObject sender, GH_PreviewExpiredEventArgs e)
        {
            try
            {
                if (Globals.OpenThisShell[thisIndex] == true)
                {
                    PythonIDE.Show();
                }
                else
                {
                    PythonIDE.Hide();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            AddNamesAndDescriptions();
            Grasshopper.Instances.RedrawCanvas();

        }



        /// <summary>
        /// 
        /// </summary>
        public override Grasshopper.Kernel.GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }



        Regex line = new Regex(@"Line\(.*\)", RegexOptions.Multiline);
        /// <summary>
        /// This function is resposible for writing python files after
        /// gathering all inputs and outputs in a python-syntax form.
        /// </summary>
        /// <param name="WinForm"></param>
        private void writeReadPythonFile(Gh_CPythonComponent WinForm)
        {


            /// Section 1
            /// Initiate temporary Python file name that will be executed as well as the temporary folder.
            string name = "PythonFileWritten_" + thisIndex.ToString();


            try
            {
                variablesAre = ""; // Collecting the input variables here.

                /// Section 2
                /// Add the output variables' names and initiate them as None. 
                for (int i = 0; i < Params.Output.Count; i++)
                {
                    variablesAre += Params.Output[i].NickName + " = None\n";
                }


                // These temporary variables  are used for parsing input data into float, double or int. 
                float f;
                double d;
                int into;

                /// Section 3. 
                /// Collect input data names, and values then initiate them in a python syntax form as : "variableName = varibaleValue \n"
                for (int i = 0; i < Params.Input.Count; i++)
                {
                    string datahere = "";

                    if (Params.Input[i].Access == GH_ParamAccess.list)
                    {
                        string thisInputString = Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");

                        string[] newstr = thisInputString.Split(',');

                        if (line.IsMatch(newstr[0]))
                        {



                            //docUment = this.OnPingDocument();
                            //var dodo = docUment.FindComponent(Params.Input[i].Sources[0].InstanceGuid);// (Params.Input[i].Sources[0].InstanceGuid);//FindObject(Params.Input[i].Sources[0].InstanceGuid, false).;

                            //MessageBox.Show(dodo.ToString());
                            //datahere += "(\"" + Params.Input[i].Sources[0].InstanceGuid.ToString() + "\")";


                        }
                        else if (float.TryParse(newstr[0], out f) || double.TryParse(newstr[0], out d) || int.TryParse(newstr[0], out into) || newstr[0] == "True" || newstr[0] == "False")
                        {
                            if (newstr.Length == 1)
                                datahere += thisInputString;
                            else
                                datahere += "[" + thisInputString + "]";
                        }
                        else if (thisInputString.Contains("{") && thisInputString.Contains("}"))
                        {
                            datahere += thisInputString.Replace("{", "[").Replace("}", "]");
                        }
                        else if (thisInputString.Contains("{") && thisInputString.Contains("}") && thisInputString.Contains("\""))
                        {
                            datahere += thisInputString.Replace("{", "[").Replace("}", "]").Replace("\"", "'");
                        }
                        else if (thisInputString.Contains("[") && thisInputString.Contains("]"))
                        {
                            datahere += thisInputString.Replace("[", "[").Replace("]", "]");
                        }
                        else if (thisInputString.Contains("(") && thisInputString.Contains(")"))
                        {
                            datahere += thisInputString.Replace("(", "(").Replace(")", ")");
                        }
                        else
                        {
                            if (newstr.Length == 1)
                            {
                                if (thisInputString == "True" || thisInputString == "False" || thisInputString == "None")
                                {
                                    datahere += thisInputString;
                                }
                                else if (thisInputString == "")
                                {
                                    datahere += "None";
                                }
                                else
                                {
                                    datahere += "\"" + thisInputString + "\"";
                                }
                            }
                            else
                            {
                                datahere += "[\"" + thisInputString.Replace(",", "\",\"") + "\"]";
                            }
                        }
                    }
                    else if (Params.Input[i].Access == GH_ParamAccess.item)
                    {

                        string thisInputString = Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                        string[] newstr = thisInputString.Split(',');
                        if (float.TryParse(newstr[0], out f) || double.TryParse(newstr[0], out d) || int.TryParse(newstr[0], out into))
                        {
                            if (newstr.Length == 1)
                                datahere += thisInputString;
                            else
                                datahere += "[" + thisInputString + "]";
                        }
                        else
                        {
                            if (newstr.Length == 1)
                                if (thisInputString == "True" || thisInputString == "False")
                                {
                                    datahere += thisInputString;
                                }
                                else
                                {
                                    datahere += "\"" + thisInputString + "\"";
                                }
                            else
                                datahere += "[\"" + thisInputString.Replace(",", "\",\"") + "\"]";
                        }
                    }


                    variablesAre += Params.Input[i].NickName + " = " + datahere + " \n";
                }
                foot = Resources.SavedPythonFile.savingFile;
                string thisOutputData = "";


                for (int i = 0; i < Params.Output.Count; i++)
                {
                    if (i < Params.Output.Count - 1)
                        thisOutputData += "\"" + Params.Output[i].NickName + "\":" + Params.Output[i].NickName + ", ";
                    else
                        thisOutputData += "\"" + Params.Output[i].NickName + "\":" + Params.Output[i].NickName;

                }
                foot = foot.Replace("##data##", thisOutputData);
                foot = foot.Replace("##fileName##", path.Replace(@"\", "/") + @"_PythonExecutionOrder_" + thisIndex.ToString() + @".xml");

            }
            catch (Exception exep)
            {
                MessageBox.Show(exep.ToString());
            }
            if (Globals.PythonString.ContainsKey(thisIndex))
            {
                Globals.PythonString.Remove(thisIndex);
                Globals.PythonString.Add(thisIndex, variablesAre + WinForm.PythonIDE.PythonCanvas.Text + "\n" + foot);

            }
            else
            {
                Globals.PythonString.Add(thisIndex, variablesAre + WinForm.PythonIDE.PythonCanvas.Text + "\n" + foot);
            }


            /// Section 4.
            /// put all variables together alongwith the body of the python file text.
            thisPythonString = variablesAre + WinForm.PythonIDE.PythonCanvas.Text + "\n" + foot;


            /// Section 5.
            /// Save all data as a python file that will be run when the component is expired. 
            System.IO.File.WriteAllText(path + name + ".py", variablesAre + WinForm.PythonIDE.PythonCanvas.Text + "\n" + foot);




        }



        int inputCount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {

            AddNamesAndDescriptions();

            try
            {
                writer.SetString("allSavedText", PythonIDE.PythonCanvas.Text);
                /*if (retrievedData != "")
                    writer.SetString("allSavedText", retrievedData);
                else
                    writer.SetString("allSavedText", InitialPythonText);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return base.Write(writer);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {

            if (reader.ItemExists("allSavedText"))
            {
                writtenText = reader.GetString("allSavedText");
                PythonIDE.PythonCanvas.Text = writtenText;
            }

            return base.Read(reader);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Ps_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;
            PythonIDE.Hide();
            AddNamesAndDescriptions();
            Globals.OpenThisShell[thisIndex] = false;
            Grasshopper.Instances.RedrawCanvas();
        }



        public override void CreateAttributes()
        {
            m_attributes = new AttribCompo(this);
        }



        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("_input", "_input", "", GH_ParamAccess.list, "None");
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("output_", "output_", "", GH_ParamAccess.item);

        }

        static string commBlock = "\"\"\"";
        //Regex pattern = new Regex("(?<=" + commBlock + @"(.*)?!" + commBlock + @"(?=" + commBlock + ")", RegexOptions.Singleline);
        Regex getInputPatterns = new Regex(@"(?<=<inp>)(.*?)(?=<\/inp>)", RegexOptions.Singleline);
        Regex getOutputPatterns = new Regex(@"(?<=<out>)(.*?)(?=<\/out>)", RegexOptions.Singleline);

        Regex getInputOptions = new Regex(@"(?<=\[)(\w*)(?=\])");

        Regex pattern2 = new Regex(@"\w*\s*:.*");
        Regex pattern3 = new Regex(@"(\w*\s*)(?=:)");
        Regex pattern4 = new Regex(@"(?<=:)(.*)", RegexOptions.Singleline);

        /// <summary>
        /// This method mainly reads the documentation of each component, such as input and output descriptions. 
        /// </summary>
        void AddNamesAndDescriptions()
        {

            try
            {
                string firstComment = PythonIDE.PythonCanvas.Text.Split(new string[] { commBlock }, StringSplitOptions.RemoveEmptyEntries)[1];
                try
                {
                    string ComponentDesc = firstComment.Split(new string[] { @"[desc]" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "[/desc]" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.Description = ComponentDesc;
                }
                catch { }

                try
                {
                    string ComponentName = firstComment.Split(new string[] { @"[name]" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "[/name]" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.Name = ComponentName;
                    this.NickName = ComponentName;
                }
                catch { }

                try
                {
                    string ComponentMessage = firstComment.Split(new string[] { @"[message]" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "[/message]" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.Message = ComponentMessage;

                }
                catch { }

                try
                {
                    string ComponentCategory = firstComment.Split(new string[] { @"[category]" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "[/category]" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.Category = ComponentCategory;

                }
                catch { }

                try
                {
                    string ComponentSubCategory = firstComment.Split(new string[] { @"[subcategory]" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "[/subcategory]" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.SubCategory = ComponentSubCategory;

                }
                catch { }

                MatchCollection inIn = getInputPatterns.Matches(firstComment);
                MatchCollection ouOu = getOutputPatterns.Matches(firstComment);

                //MessageBox.Show(mm[0].Value);
                Dictionary<string, string> inVarsRegx = new Dictionary<string, string>();
                Dictionary<string, string> OutVarsRegx = new Dictionary<string, string>();
                Dictionary<string, string> optional = new Dictionary<string, string>();
                for (int i = 0; i < inIn.Count; i++)
                {
                    string el1 = pattern3.Match(inIn[i].Value).Value.Trim();
                    string el2 = pattern4.Match(inIn[i].Value).Value;

                    if (el2.Contains("[required]"))
                    {
                        if (optional.ContainsKey(el1)) optional[el1] = "required";
                        else optional.Add(el1, "required");
                    }
                    else
                    {
                        if (optional.ContainsKey(el1)) optional[el1] = "optional";
                        else optional.Add(el1, "optional");
                    }

                    inVarsRegx.Add(el1.Trim(), el2.Trim());
                    //MessageBox.Show("Name: "+el1 + "\n Desc: " + el2);
                }

                for (int i = 0; i < ouOu.Count; i++)
                {
                    string el1 = pattern3.Match(ouOu[i].Value).Value.Trim();
                    string el2 = pattern4.Match(ouOu[i].Value).Value;

                    OutVarsRegx.Add(el1.Trim(), el2.Trim());
                    //MessageBox.Show("Name: "+el1 + "\n Desc: " + el2);
                }
                this.Name = this.NickName;

                for (int i = 0; i < this.Params.Input.Count; i++)
                {
                    this.Params.Input[i].Name = this.Params.Input[i].NickName;

                    try
                    {
                        this.Params.Input[i].Description = inVarsRegx[this.Params.Input[i].NickName].Replace(@"\n", Environment.NewLine);
                        //MessageBox.Show(optional[Params.Input[i].NickName]);

                        if (optional[Params.Input[i].NickName] == "required") Params.Input[i].Optional = false;
                        else Params.Input[i].Optional = true;
                    }
                    catch { }
                }

                for (int i = 0; i < this.Params.Output.Count; i++)
                {
                    this.Params.Output[i].Name = this.Params.Output[i].NickName;
                    try { this.Params.Output[i].Description = OutVarsRegx[this.Params.Output[i].NickName].Replace(@"\n", Environment.NewLine); }
                    catch { }
                }

            }
            catch
            {
                //MessageBox.Show("error");
            }

        }


        Regex matchLine = new Regex(@"(?<=Line\().*(?=\))");

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        ///   
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            try
            {
                for (int i = 0; i < Params.Input.Count; i++)
                {
                    if (Params.Input[i].Optional == false && Params.Input[i].VolatileData.DataDescription(false, false) == null)
                        return;
                }

            }
            catch
            {

            }

            bool ThereisProblem = false;

            long t0 = DateTime.Now.Ticks;
            PythonIDE.Text = this.NickName;
            retrievedData = PythonIDE.PythonCanvas.Text;

            AddNamesAndDescriptions();
            string output = "";


            writeReadPythonFile(this);
            System.IO.File.WriteAllText(@path + @name + @".py", thisPythonString);
            try
            {

                RunningPythonProcess.StartInfo.FileName = @StartFileName;
                RunningPythonProcess.StartInfo.Arguments = path + name + ".py";
                RunningPythonProcess.Start();

                // To avoid deadlocks, always read the output stream first and then wait.
                output += RunningPythonProcess.StandardOutput.ReadToEnd();
                output += RunningPythonProcess.StandardError.ReadToEnd();
                RunningPythonProcess.WaitForExit();
                System.IO.File.Delete(path + name + ".py");

                PythonIDE.console.Text = output;

                doc.Load(path + @"_PythonExecutionOrder_" + thisIndex.ToString() + @".xml");

                for (int i3 = 0; i3 < Params.Output.Count; i3++)
                {

                    DA.SetData(i3, getOutPutData(doc.DocumentElement.SelectSingleNode("/result/" + Params.Output[i3].NickName).InnerText));
                 
                }
                System.IO.File.Delete(path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");
                RunningPythonProcess.Close();

            }
            catch (Exception exf)
            {
                PythonIDE.console.Text = output;
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, output);
            }
            PythonIDE.console.Text = output;
            long t1 = (DateTime.Now.Ticks - t0) / 10000;
            if (output.Trim() == "")
            {
                PythonIDE.console.Text = "Successful in " + t1.ToString() + " MilliSeconds";
            }


           
        }

        private object getOutPutData(string p)
        {
            if (matchLine.Match(p).Value != "")
            {

                var numbers = matchLine.Match(p).Value.Split(',');
                Line lx = new Line(double.Parse(numbers[0]), double.Parse(numbers[1]), double.Parse(numbers[2]),
                                   double.Parse(numbers[3]), double.Parse(numbers[4]), double.Parse(numbers[5]));
                return lx; 


            }
            else
            {
                return p;
            }
        }


        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return Resources.Icons.Python_logo_241;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{f7418245-81a4-4dbd-9b8f-c6ef68607efc}"); }
        }


        ////////////////////////////////////////////////////////////////////////////
        //                  CUSTOME ATTRIBUTE COMPONENT 
        ////////////////////////////////////////////////////////////////////////////






        /// <summary>
        /// CUSTOME ATTRIBUTE COMPONENT 
        /// </summary>
        public class AttribCompo : GH_ComponentAttributes
        {
            bool shellOpened = false;
            private string at = DateTime.Now.ToString("dddd MMMM yyyy hh:mm:ss ");

            public AttribCompo(IGH_Component PythonInterfaceComponent)
                : base(PythonInterfaceComponent)
            {
                string Name = System.Environment.UserName;
                ChangedText = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
                ChangedText = ChangedText.Replace("##at##", at);

                thisIndex2 = Globals.index;
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <returns></returns>
            public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
            {

                if (!Globals.OpenThisShell.ContainsKey(thisIndex2))
                {
                    Globals.OpenThisShell.Add(thisIndex2, true);
                    shellOpened = true;
                    Owner.ExpirePreview(false);
                    //Owner.ExpireSolution(true);

                }
                else
                {
                    Globals.OpenThisShell[thisIndex2] = true;
                    shellOpened = true;
                    Owner.ExpirePreview(false);
                    //Owner.ExpireSolution(true);

                }

                return Grasshopper.GUI.Canvas.GH_ObjectResponse.Handled;
            }


            System.Drawing.Rectangle rec0;

            /// <summary>
            /// LAYOUT DRAWING
            /// </summary>
            protected override void Layout()
            {
                base.Layout();
                rec0 = GH_Convert.ToRectangle(Bounds);
                rec0.Height += 5;
                System.Drawing.Rectangle rec1 = rec0;
                rec1.Width = rec0.Width / 2;
                rec1.Y = rec0.Bottom - 5;
                rec1.Height = 5;
                rec1.Inflate(0, 0);

                System.Drawing.Rectangle rec2 = rec0;

                if (rec0.Width % 2 == 0)
                {
                    rec2.X = rec0.Right - rec0.Width / 2;
                    rec2.Width = rec0.Width / 2;
                }
                else
                {
                    rec2.X = -1 + rec0.Right - rec0.Width / 2;
                    rec2.Width = 1 + rec0.Width / 2;
                }
                rec2.Y = rec0.Bottom - 5;
                rec2.Height = 5;
                rec2.Inflate(0, 0);

                Bounds = rec0;
                ButtonBounds = rec1;
                ButtonBounds2 = rec2;

            }

            GH_Capsule button;

            GH_Capsule button2;

            /// <summary>
            /// VAR - BUTTON BOUNDS
            /// </summary>
            private System.Drawing.Rectangle ButtonBounds { get; set; }

            /// <summary>
            /// RENDERING LAYOUT 
            /// </summary>
            /// <param name="canvas"></param>
            /// <param name="graphics"></param>
            /// <param name="channel"></param>
            protected override void Render(Grasshopper.GUI.Canvas.GH_Canvas canvas, System.Drawing.Graphics graphics, Grasshopper.GUI.Canvas.GH_CanvasChannel channel)
            {
                base.Render(canvas, graphics, channel);
                if (channel == Grasshopper.GUI.Canvas.GH_CanvasChannel.Objects)
                {
                    button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, GH_Palette.Transparent, "", new int[] { 0, 0, 0, 5 }, 0);
                    button.Render(graphics, Selected, Owner.Locked, false);
                    button.Render(graphics, Color.FromArgb(255, 50, 100, 150));

                    button2 = GH_Capsule.CreateTextCapsule(ButtonBounds2, ButtonBounds2, GH_Palette.Transparent, "", new int[] { 0, 0, 5, 0 }, 0);
                    button2.Render(graphics, Selected, Owner.Locked, false);
                    button2.Render(graphics, Color.FromArgb(255, 230, 200, 10));


                    if (Globals.OpenThisShell[thisIndex2] == true)
                    {
                        System.Drawing.Drawing2D.LinearGradientBrush lgb = new System.Drawing.Drawing2D.LinearGradientBrush(
                           rec0.Location,
                           pythonRect.Location,
                           System.Drawing.Color.FromArgb(255, 0, 200, 0),   // Opaque red
                           System.Drawing.Color.FromArgb(255, 0, 200, 0));  // Opaque blue);
                        System.Drawing.Pen p = new System.Drawing.Pen(Color.Black, 1);
                        p.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        Rectangle r5 = rec0;
                        r5.Inflate(5, 5);
                        // graphics.DrawLine(p, rec0.Location, pythonRect.Location);
                        graphics.DrawPath(p, RoundedRect(r5, 3));
                        //graphics.DrawRectangle(p, rec0);
                    }

                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.Trimming = StringTrimming.EllipsisCharacter;


                    Brush gg = new SolidBrush(Color.FromArgb(255, 50, 100, 150));
                    graphics.DrawString(Owner.NickName, GH_FontServer.Standard, gg, new PointF(rec0.Left, rec0.Top - 15));

                    button.Dispose();
                }
            }

            /// <summary>
            /// CREATES ROUND RECTANGLE AROUND THE COMPONENT
            /// </summary>
            /// <param name="bounds"></param>
            /// <param name="radius"></param>
            /// <returns></returns>
            public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
            {
                int diameter = radius * 2;
                System.Drawing.Size size = new System.Drawing.Size(diameter, diameter);
                Rectangle arc = new Rectangle(bounds.Location, size);
                GraphicsPath path = new GraphicsPath();


                if (radius == 0)
                {
                    path.AddRectangle(bounds);
                    return path;
                }

                // top left arc  
                path.AddArc(arc, 180, 90);

                // top right arc  
                arc.X = bounds.Right - diameter;
                path.AddArc(arc, 270, 90);

                // bottom right arc  
                arc.Y = bounds.Bottom - diameter;
                path.AddArc(arc, 0, 90);

                // bottom left arc 
                arc.X = bounds.Left;
                path.AddArc(arc, 90, 90);

                path.CloseFigure();
                path.CloseFigure();
                return path;
            }

            public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    System.Drawing.Rectangle rec = ButtonBounds;
                    if (rec.Contains((int)e.CanvasLocation.X, (int)e.CanvasLocation.Y))
                    {
                        //MessageBox.Show("Still under Development", "Refresh", MessageBoxButton.OK);
                        locatePython gg = new locatePython();
                        gg.ShowDialog();
                        System.Drawing.Graphics g = null;
                        button.Render(g, Color.Blue);
                        return GH_ObjectResponse.Handled;
                    }
                }
                return base.RespondToMouseDown(sender, e);
            }

            public System.Drawing.Rectangle pythonRect { get; set; }

            public string ChangedText { get; set; }

            public Rectangle ButtonBounds2 { get; set; }

            public string output { get; set; }

            public string variablesAre { get; set; }

            public string foot { get; set; }

            public int thisIndex2 { get; set; }
        }


        public string thisshell { get; set; }

        public string pythonData { get; set; }

        public bool CanInsertParameter(GH_ParameterSide side, int index)
        {
            return true;
        }

        public bool CanRemoveParameter(GH_ParameterSide side, int index)
        {
            return true;
        }

        public IGH_Param CreateParameter(GH_ParameterSide side, int index)
        {
            Param_GenericObject param = new Param_GenericObject();

            if (side == GH_ParameterSide.Input)
            {
                param.Name = GH_ComponentParamServer.InventUniqueNickname("xyzuvwst", Params);
                param.Access = GH_ParamAccess.list;
                param.Optional = true;
                param.NickName = param.Name;
            }
            else if (side == GH_ParameterSide.Output)
            {
                param.Name = GH_ComponentParamServer.InventUniqueNickname("abcdefghijklmn", Params);

                param.Access = GH_ParamAccess.list;
                param.NickName = param.Name;
            }

            param.Description = "Param" + (Params.Input.Count + 1);

            return param;
        }

        public bool DestroyParameter(GH_ParameterSide side, int index)
        {
            return true;
        }

        public void VariableParameterMaintenance()
        {


        }

        public string variablesAre { get; set; }

        //public string output { get; set; }

        public int thisIndex { get; set; }

        public string name { get; set; }

        public string InitialPythonText { get; set; }

        public bool shellOpened { get; set; }

        public string foot { get; set; }

        public string thisPythonString { get; set; }


        public string defaultFileName { get; set; }
    }
}
