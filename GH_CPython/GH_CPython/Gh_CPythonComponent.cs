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
        string pythonVersion = "# Grasshopper Module Version 0.0.1.7";

        PythonShell PythonIDE;

        DataManagement DM;

        InitFunctions initfunc;

        PythonFileControl pythonFileControl = new PythonFileControl();

        Process RunningPythonProcess = new Process();

        XmlDocument doc = new XmlDocument();

        string path = @"C:\GH_CPython\";

        private string at;

        public string retrievedData = "";

        public string writtenText = "";

        string StartFileName = "python.exe";


       

        /// <summary>
        /// Constructor 
        /// </summary>
        public Gh_CPythonComponent()
            : base("GH_CPython", "GH_CPython",
                "a python IDE interface",
                "Maths", "Script")
        {

            initfunc = new InitFunctions();

            //1- Look for default GH_CPython path i.e.'C:\GH_CPython\'
            path = initfunc.isPythonFloderExists(@"C:\GH_CPython\");

            //2- Look for and set Python.exe interpreter if not set
            StartFileName = initfunc.getPythonInterpretere(@"C:\GH_CPython\interpreter.dat");

            //3- Add latest version of required python modules if not existed
            initfunc.addGrasshopperPyModule(@"C:\GH_CPython\Grasshopper.py", pythonVersion);

            // initiate python IDE instance
            PythonIDE = new PythonShell();

            PythonIDE.TopMost = true;

            
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
                /// "Hi UserName, How are you ?"
                PythonIDE.console.Text = "Hi " + Name + ", How are you ?";


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

                /// EventHandler of the Form Closing
                PythonIDE.FormClosing += Ps_FormClosing;

                PythonIDE.manageDataItem.Click += manageDataItem_Click;
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
            catch { }

            AddNamesAndDescriptions();
        }



        void manageDataItem_Click(object sender, EventArgs e)
        {

            DM = new DataManagement();

            PythonIDE.TopMost = false;

            DM.TopMost = true;

            DM.Show();

            DM.cancelButton.Click += (s, ev) => {

                DM.Close();
            
            };

            DM.applyButton.Click += (se, ev) =>
            {
                ExpirePreview(true);
                ExpireSolution(true);
            };
            DM.FormClosing += DM_FormClosing;

            
            List<string> inputStrings = new List<string>();
            Dictionary<string, int> dAccess = new Dictionary<string, int>();
            Dictionary<string, int> dINdex = new Dictionary<string, int>();
            Dictionary<string, string> dDesc = new Dictionary<string, string>();

            for (int i = 0; i < Params.Input.Count; i++)
            {
                int access = Params.Input[i].Access == GH_ParamAccess.item ? 0 : Params.Input[i].Access == GH_ParamAccess.list ? 1 : 2;
                inputStrings.Add(Params.Input[i].NickName);
                dAccess.Add(Params.Input[i].NickName, access);
                dINdex.Add(Params.Input[i].NickName, i);
                dDesc.Add(Params.Input[i].NickName, Params.Input[i].Description);
            }
            DM.inputList.DataSource = inputStrings;

            DM.inputList.SetSelected(0, true);

            DM.inputList.SelectedValueChanged += (se, ev) =>
            {
                try
                {
                    string selItem = DM.inputList.SelectedValue.ToString();
                    DM.dAccesslist.SetSelected(dAccess[selItem],true);
                    DM.description.Text = dDesc[selItem];
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            };

            DM.dAccesslist.SelectedIndexChanged += (sen, eve) =>
            {
                string selItem = DM.inputList.SelectedValue.ToString();
                int selectedindex = DM.dAccesslist.SelectedIndex;
                Params.Input[dINdex[selItem]].Access = selectedindex == 0 ? GH_ParamAccess.item : selectedindex == 1 ? GH_ParamAccess.list : GH_ParamAccess.tree;
                dAccess[selItem] = selectedindex;
                
            };

        }

        void DM_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            PythonIDE.TopMost = true;

            ExpireSolution(true);
        }

        void PythonCanvas_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
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
            StartFileName = initfunc.getPythonInterpretere(@"C:\GH_CPython\interpreter.dat");
            Grasshopper.Instances.RedrawCanvas();
        }



        /// <summary>
        /// 
        /// </summary>
        public override Grasshopper.Kernel.GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
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
            pManager.AddGenericParameter("_input", "_input", "", GH_ParamAccess.list);

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
                }
                this.Name = this.NickName;

                for (int i = 0; i < this.Params.Input.Count; i++)
                {
                    this.Params.Input[i].Name = this.Params.Input[i].NickName;

                    try
                    {
                        this.Params.Input[i].Description = inVarsRegx[this.Params.Input[i].NickName].Replace(@"\n", Environment.NewLine);

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


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        ///   
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            long t0 = DateTime.Now.Ticks;
            PythonIDE.Text = this.NickName;
            retrievedData = PythonIDE.PythonCanvas.Text;
            try { AddNamesAndDescriptions(); }
            catch { }

            string output = "";

            try
            {

                pythonFileControl.writeReadPythonFile(this,
                                                        PythonIDE,
                                                        DA,
                                                        thisIndex,
                                                        path);

                RunningPythonProcess.StartInfo.FileName = @StartFileName;
                //MessageBox.Show(@StartFileName);
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
                    string outputType = doc.DocumentElement.SelectSingleNode("/result/" + Params.Output[i3].NickName).Attributes["type"].Value;
                    setoutPutData(i3, doc.DocumentElement.SelectSingleNode("/result/" + Params.Output[i3].NickName).InnerText, DA, outputType);
                }
                RunningPythonProcess.Close();
                System.IO.File.Delete(path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");

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

            try
            {
                System.IO.File.Delete(path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");
            }
            catch { }


        }

        Regex matchLine = new Regex(@"(?<=gCPy\.Line\().*(?=\))");
        Regex matchPoint = new Regex(@"(?<=gCPy\.Point\().*(?=\))");
        Regex matchCircle = new Regex(@"(?<=gCPy\.Circle\().*(?=\))");
        Regex matchSurface = new Regex(@"(?<=gCPy\.Surface\().*(?=\))");


        Regex matchDoc = new Regex(@"(?<=gCPy\.Doc\().*(?=\))");
        private void setoutPutData(int i3, string p, IGH_DataAccess DA, string outputType)
        {
            //MessageBox.Show(outputType);
            //Rhino.Geometry.NurbsSurface.CreateFromCorners()
            //Line input

            if (matchLine.Match(p).Value != "")
            {
                if (p.Contains(@")', 'gCPy.Line("))
                {
                    p = p.Replace("'gCPy.Line(", "'(").Replace("'", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace("),", "#").Replace(")", "");
                    string[] lines = p.Split('#');
                    List<Rhino.Geometry.Line> lns = new List<Rhino.Geometry.Line>();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var thisL = lines[i].Split(',');
                        Rhino.Geometry.Line lx = new Rhino.Geometry.Line(double.Parse(thisL[0].Trim()),
                                                                            double.Parse(thisL[1].Trim()),
                                                                            double.Parse(thisL[2].Trim()),
                                                                            double.Parse(thisL[3].Trim()),
                                                                            double.Parse(thisL[4].Trim()),
                                                                            double.Parse(thisL[5].Trim()));
                        lns.Add(lx);
                    }
                    DA.SetDataList(i3, lns);
                }
                // Single Point input
                else
                {
                    var numbers = matchLine.Match(p).Value.Split(',');
                    Line lx = new Line(double.Parse(numbers[0]), double.Parse(numbers[1]), double.Parse(numbers[2]),
                                       double.Parse(numbers[3]), double.Parse(numbers[4]), double.Parse(numbers[5]));
                    DA.SetData(i3, lx);
                }

            }
            //Point input
            else if (matchPoint.Match(p).Value != "")
            {
                // Point List input
                if (p.Contains(@")', 'gCPy.Point("))
                {
                    p = p.Replace("'gCPy.Point(", "'(").Replace("'", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace("),", "#").Replace(")", "");
                    string[] points = p.Split('#');
                    List<Rhino.Geometry.Point3d> pts = new List<Rhino.Geometry.Point3d>();
                    for (int i = 0; i < points.Length; i++)
                    {
                        var thisP = points[i].Split(',');
                        Rhino.Geometry.Point3d px = new Rhino.Geometry.Point3d(new Point3d(double.Parse(thisP[0].Trim()), double.Parse(thisP[1].Trim()), double.Parse(thisP[2].Trim())));
                        pts.Add(px);
                    }
                    DA.SetDataList(i3, pts);
                }
                // Single Point input
                else
                {
                    var numbers = matchPoint.Match(p).Value.Split(',');
                    Rhino.Geometry.Point px = new Rhino.Geometry.Point(new Point3d(double.Parse(numbers[0]), double.Parse(numbers[1]), double.Parse(numbers[2])));
                    DA.SetData(i3, px);
                }

            }
            else if (matchCircle.Match(p).Value != "")
            {

                var numbers = matchCircle.Match(p).Value.Split(',');
                Rhino.Geometry.Plane pl = new Plane(new Point3d(double.Parse(numbers[0]), double.Parse(numbers[1]), double.Parse(numbers[2])),
                                                    new Vector3d(double.Parse(numbers[4]), double.Parse(numbers[5]), double.Parse(numbers[6])),
                                                    new Vector3d(double.Parse(numbers[7]), double.Parse(numbers[8]), double.Parse(numbers[9])));
                Rhino.Geometry.Circle cx = new Rhino.Geometry.Circle(pl, double.Parse(numbers[3]));
                DA.SetData(i3, cx);
            }
            else if (matchSurface.Match(p).Value != "")
            {
                var numbers = matchSurface.Match(p).Value.Split(',');
                NurbsSurface srf = NurbsSurface.CreateFromCorners(new Point3d(double.Parse(numbers[0]), double.Parse(numbers[1]), double.Parse(numbers[2])),
                                                                    new Point3d(double.Parse(numbers[3]), double.Parse(numbers[4]), double.Parse(numbers[5])),
                                                                    new Point3d(double.Parse(numbers[6]), double.Parse(numbers[7]), double.Parse(numbers[8])),
                                                                    new Point3d(double.Parse(numbers[9]), double.Parse(numbers[10]), double.Parse(numbers[11])));
                DA.SetData(i3, srf);
            }
            else
            {
                if(outputType == "Item")
                {
                    DA.SetData(i3, p);
                }else
                {
                    string[] all = p.Replace("[", "").Replace("]", "").Split(new string[] { @"," }, StringSplitOptions.RemoveEmptyEntries);
                    DA.SetDataList(i3, all);

                }
                
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
