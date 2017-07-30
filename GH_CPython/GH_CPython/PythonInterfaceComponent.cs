/***
    BSD 2-Clause License

    Copyright (c) 2017, Mahmoud AbdelRahman
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

 * */

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

namespace GH_CPython
{


    public class PythonInterfaceComponent : GH_Component, IGH_VariableParameterComponent
    {
        string initShell;
        PythonShell Ps;

        Process p = new Process();
        string path = System.IO.Path.GetTempPath();
        private string at;
        bool focused = false;

        XmlDocument doc = new XmlDocument();
        /// <summary>
        /// Constructor 
        /// </summary>
        public PythonInterfaceComponent()
            : base("GH_CPython", "GH_CPython",
                "a python IDE interface",
                "Maths", "Script")
        {
            this.Params.ParameterNickNameChanged += Params_ParameterNickNameChanged;

            Ps = new PythonShell();
            Ps.TopMost = true;
            thisIndex = Globals.index;
            name = "PythonFileWritten_" + thisIndex.ToString();
            at = DateTime.Now.ToString("dddd MMMM yyyy hh:mm:ss ");
            Globals.fileName.Add(thisIndex, "_PythonExecutionOrder_" + thisIndex.ToString());
            Globals.index += 1;
            Globals.OpenThisShell.Add(thisIndex, false);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "python.exe";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;


          
            string Name = System.Environment.UserName;
            ChangedText = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
            ChangedText = ChangedText.Replace("##at##", at);
            
            try
            {

                Ps.console.Text = "Hi, How are you ? Are you ready to Change the world ?";
                if (retrievedData != "")
                {
                    Ps.PythonCanvas.Text = retrievedData;
                }
                else
                {
                    Ps.PythonCanvas.Text = ChangedText;
                }
                writeReadPythonFile(this);
                Ps.FormClosing += Ps_FormClosing;

                Ps.Test.Click += (se, ev) =>
                   {
                       writeReadPythonFile(this);

                       ExpireSolution(true);
                   };
                Ps.close.Click += (se, ev) =>
                    {
                        ChangedText = Ps.PythonCanvas.Text;
                        savedShellData = ChangedText;
                        shellOpened = false;
                        Ps.Hide();

                        Globals.OpenThisShell[thisIndex] = false;
                        Grasshopper.Instances.RedrawCanvas();
                    };
                Grasshopper.Instances.RedrawCanvas();

            }
            catch (Exception erx)
            {
                MessageBox.Show(erx.ToString());
            }
        }


        private void writeReadPythonFile(PythonInterfaceComponent gg)
        {
            output = "";
            //string name = DateTime.Now.ToString("yyyyMMddhhmmssff");
            string name = "PythonFileWritten_" + thisIndex.ToString();
            string path = System.IO.Path.GetTempPath();

            try
            {
                variablesAre = "";

                float f;
                double d;
                int into;


                for (int i = 0; i < Params.Output.Count; i++)
                {
                    variablesAre += Params.Output[i].NickName + " = None\n";
                }

                for (int i = 0; i < Params.Input.Count; i++)
                {
                    string datahere = "";
                    if (Params.Input[i].Access == GH_ParamAccess.list)
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
                        else if (thisInputString.Contains("{") && thisInputString.Contains("}"))
                        {
                            datahere += thisInputString.Replace("{", "[").Replace("}", "]");
                        }
                        else
                        {
                            if (newstr.Length == 1)
                                if(thisInputString =="True" || thisInputString == "False")
                                {
                                    datahere+= thisInputString;
                                }else
                                {
                                    datahere += "\"" + thisInputString + "\"";
                                }
                            else
                                datahere += "[\"" + thisInputString.Replace(",", "\",\"") + "\"]";
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
                                if(thisInputString == "True" || thisInputString == "False")
                                {
                                    datahere +=  thisInputString ;
                                }else
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
                foot = foot.Replace("##fileName##", path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");

            }
            catch (Exception exep)
            {
                MessageBox.Show(exep.ToString());
            }
            if (Globals.PythonString.ContainsKey(thisIndex))
            {
                Globals.PythonString.Remove(thisIndex);
                Globals.PythonString.Add(thisIndex, variablesAre + gg.Ps.PythonCanvas.Text + "\n" + foot);
                
            }
            else
            {
                Globals.PythonString.Add(thisIndex, variablesAre + gg.Ps.PythonCanvas.Text + "\n" + foot);
            }

            thisPythonString = variablesAre + gg.Ps.PythonCanvas.Text + "\n" + foot;

            System.IO.File.WriteAllText(path + name + ".py", variablesAre + gg.Ps.PythonCanvas.Text + "\n" + foot);

            try
            {
                p.StartInfo.Arguments = path + name + ".py";
                p.Start();

                //To avoid deadlocks, always read the output stream first and then wait.
                output += p.StandardOutput.ReadToEnd();
                output += p.StandardError.ReadToEnd();
                p.WaitForExit();


            }
            catch (Exception eex)
            {
                output += eex.ToString();
            }
            gg.Ps.console.Text = output;
            System.IO.File.Delete(path + name + ".py");
            try
            {
                //ExpireSolution(true);
            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.ToString());
            }

            //gg.Ps.BringToFront();

        }



        void Params_ParameterNickNameChanged(object sender, GH_ParamServerEventArgs e)
        {
            data = this.Params.Input[0].NickName;
            this.Message = this.Params.Input[0].NickName;
        }

        void close_Click(object sender, EventArgs e)
        {
            Ps.Hide();
        }

        void Ps_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;
            Ps.Hide();

            Globals.OpenThisShell[thisIndex] = false;
            Grasshopper.Instances.RedrawCanvas();
        }


        public override void CreateAttributes()
        {
            m_attributes = new AttribCompo(this);
        }


        public string retrievedData = "";

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("_input", "_input", "description", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("output_", "output_", "", GH_ParamAccess.item);

        }
        string data;
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
                if (Globals.OpenThisShell[thisIndex] == true)
                {
                    Ps.Show();
                }
                else
                {
                    Ps.Hide();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            writeReadPythonFile(this);
            System.IO.File.WriteAllText(path + name + ".py", thisPythonString);
            try
            {
                p.StartInfo.Arguments = path + name + ".py";
                p.Start();

                // To avoid deadlocks, always read the output stream first and then wait.
                output += p.StandardOutput.ReadToEnd();
                output += p.StandardError.ReadToEnd();
                p.WaitForExit();
                System.IO.File.Delete(path + name + ".py");


            }
            catch (Exception eex)
            {
                output += eex.ToString();
                MessageBox.Show("Error at the first Position");
            }
            Ps.console.Text = output;

            doc.Load(path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");
            try
            {
                for (int i3 = 0; i3 < Params.Output.Count; i3++)
                {
                    DA.SetData(i3, doc.DocumentElement.SelectSingleNode("/result/" + Params.Output[i3].Name).InnerText);
                }
                System.IO.File.Delete(path + "_PythonExecutionOrder_" + thisIndex.ToString() + ".xml");
            }catch (Exception exf)
            {
                MessageBox.Show(exf.ToString());
                MessageBox.Show("Error at the second Position");
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
                }
                else
                {
                    Globals.OpenThisShell[thisIndex2] = true;
                    shellOpened = true;
                }
                Owner.ExpireSolution(true);
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
                    graphics.DrawString(Owner.NickName, GH_FontServer.Standard, gg, new PointF(rec0.Left, rec0.Bottom + 5));

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
                        MessageBox.Show("Still under Development", "Refresh", MessageBoxButton.OK);
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
                param.NickName = param.Name;
            }
            else if (side == GH_ParameterSide.Output)
            {
                param.Name = GH_ComponentParamServer.InventUniqueNickname("abcdefghijklmn", Params);
                param.Access = GH_ParamAccess.item;
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

        public string savedShellData { get; set; }

        public string variablesAre { get; set; }

        public string output { get; set; }

        public int thisIndex { get; set; }

        public string name { get; set; }

        public string ChangedText { get; set; }

        public bool shellOpened { get; set; }

        public string foot { get; set; }

        public string thisPythonString { get; set; }
    }
}
