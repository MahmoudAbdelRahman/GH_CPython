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

namespace GH_CPython
{


    public class PythonInterfaceComponent : GH_Component, IGH_VariableParameterComponent
    {
        string savedDAta = "";
        string initShell;
        PythonShell Ps = new PythonShell();
        public List<string> inputsString = new List<string>();
        public List<string> inputNames = new List<string>();
        private static int dataOfAllTime = 0;
        Process p = new Process();
        

        /// </summary>
        public PythonInterfaceComponent()
            : base("GH_CPython", "GH_CPython",
                "a python IDE interface",
                "Maths", "Script")
        {
            dataOfAllTime += 100;
            this.Params.ParameterNickNameChanged += Params_ParameterNickNameChanged;
            inputsString.Add("None");
            inputsString.Add("None");
            inputNames.Add("x");
            inputNames.Add("y");

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "python.exe";


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
        void PythonCanvas_TextChanged(object sender, EventArgs e)
        {
            initShell = Ps.PythonCanvas.Text;
            savedDAta = Ps.PythonCanvas.Text;
        }
        void Ps_Load(object sender, EventArgs e)
        {


        }

        void Ps_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {

        }


        public override void CreateAttributes()
        {
            m_attributes = new AttribCompo(this);
        }




        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetString("pythonCode", savedShellData);
            return base.Write(writer);
        }
        public string retrievedData = "";
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            savedShellData = null;
            reader.TryGetString("pythonCode", ref retrievedData);
            return base.Read(reader);
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("_input", "_input", "description",GH_ParamAccess.list);
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
            /*
            Globals.AllInputs.Clear();
            Globals.AllInputsNames.Clear();
            
            try
            {
                dataOfAllTime += 1;
                for (int i = 0; i < this.Params.Input.Count; i++)
                {
                    List<string> temp = new List<string>(new string[] { "None" });
                    string gg = "";
                    //if (!DA.GetData(i, ref temp)) { }
                    if (!DA.GetDataList(i, temp)) { return; }
                    if (temp.Count == 2)
                    { 
                        gg = temp[1];
                    }
                    else if (temp.Count == 1)
                    {
                        Globals.AllInputsNames.Remove(i);
                        Globals.AllInputs.Remove(i);
                        inputsString.RemoveAt(i);
                    }
                    else
                    {
                        gg += "[";
                        for (int g = 1; g < temp.Count; g++)
                        {
                            gg += temp[g];
                            if (g < temp.Count - 1) gg += ",";
                        }
                        gg += "]";
                    }

                    string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
                    bool startSearch = true;
                    if (gg != "True" && gg != "False")
                    {
                        foreach (char c in gg)
                        {
                            foreach (char k in alpha)
                            {
                                if (startSearch)
                                {
                                    if (c == k)
                                    {
                                        gg = "\"" + gg + "\"";
                                        startSearch = false;
                                    }
                                }
                            }
                        }
                    }
                    if (Globals.AllInputsNames.ContainsKey(i))
                    {
                        Globals.AllInputsNames.Remove(i);
                        Globals.AllInputs.Remove(i);
                        inputsString.RemoveAt(i);
                    }

                    Globals.AllInputsNames.Add(i, Params.Input[i].NickName);
                    Globals.AllInputs.Add(i, gg);
                    inputsString.Add(gg);
                    
                }

                output = "";
                string name = DateTime.Now.ToString("yyyyMMddhhmmssff");
                string path = System.IO.Path.GetTempPath();
                try
                {
                    variablesAre = "";
                    for (int i = 0; i < Globals.AllInputs.Count; i++)
                    {
                        variablesAre += Globals.AllInputsNames[i] + " = " + Globals.AllInputs[i] + " \n";
                    }

                    //MessageBox.Show(inputNames[0]);
                }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.ToString());
                }

                System.IO.File.WriteAllText(path + name + ".py", variablesAre + Ps.PythonCanvas.Text);
                try
                {
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
                Ps.console.Text = output;

                //System.IO.File.Delete(path + name + ".py");

                Ps.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
             * 
             * */
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


        public class AttribCompo : GH_ComponentAttributes
        {
            bool shellOpened = false;
            private string at = DateTime.Now.ToString("dddd MMMM yyyy hh:mm:ss ");
            bool focused = false;

             Process p = new Process();
                            
            public AttribCompo(IGH_Component PythonInterfaceComponent)
                : base(PythonInterfaceComponent)
            {
                string Name = System.Environment.UserName;
                ChangedText = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
                ChangedText = ChangedText.Replace("##at##", at);
                
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.FileName = "python.exe";
                Owner.ObjectChanged += Owner_ObjectChanged;
                Owner.AttributesChanged += Owner_AttributesChanged;
            }

            void Owner_AttributesChanged(IGH_DocumentObject sender, GH_AttributesChangedEventArgs e)
            {
                //MessageBox.Show("Changed");
            }

            void Owner_ObjectChanged(IGH_DocumentObject sender, GH_ObjectChangedEventArgs e)
            {
               //MessageBox.Show("Changed");
            }
            public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
            {
                
                try
                {
                PythonInterfaceComponent gg = new PythonInterfaceComponent();

                if (!shellOpened)
                {
                    gg.ExpireSolution(true);
                    shellOpened = true;
                    gg.Ps.TopMost = true;
                    pythonRect = gg.Ps.RectangleToClient(Grasshopper.Instances.ActiveCanvas.DisplayRectangle);
                    gg.Ps.Show();
                    gg.Ps.console.Text = "Hi, How are you ? Are you ready to Change the world ?";


                    if (gg.retrievedData != "")
                    {
                        gg.Ps.PythonCanvas.Text = gg.retrievedData;

                    }
                    else
                    {
                        gg.Ps.PythonCanvas.Text = ChangedText;
                    }
                    gg.Ps.FormClosed += (se, ev) =>
                    {
                        ChangedText = gg.Ps.PythonCanvas.Text;
                        gg.savedShellData = ChangedText;
                        shellOpened = false;
                        Grasshopper.Instances.RedrawCanvas();

                    };
                    //gg.Ps.PythonCanvas.TextChanged += (s, ev) => ChangedText = gg.Ps.PythonCanvas.Text;
                    gg.Ps.Move += (s, ev) =>
                    {
                        pythonRect = gg.Ps.RectangleToClient(Grasshopper.Instances.ActiveCanvas.DisplayRectangle);
                        Grasshopper.Instances.RedrawCanvas();
                    };
                    gg.Ps.Shown += (se, ev) => gg.Ps.BringToFront();
                    gg.Ps.BringToFront();
                    gg.Ps.GotFocus += (se, ev) =>
                    {
                        focused = true;
                        gg.Ps.light.BackColor = Color.LimeGreen;
                        Grasshopper.Instances.RedrawCanvas();
                    };

                    gg.Ps.LostFocus += (se, ev) =>
                    {
                        focused = false;
                        gg.Ps.light.BackColor = Color.Gray;
                        Grasshopper.Instances.RedrawCanvas();
                    };

                    gg.Ps.Test.Click += (se, ev) =>
                    {
                        output = "";
                        string name = DateTime.Now.ToString("yyyyMMddhhmmssff");
                        string path = System.IO.Path.GetTempPath();
                        try
                        {
                            variablesAre = "";
                            int iForType = 1;
                            float fForType = 1;
                            double dForType = 1.00f;
                            string sForType = "s";

                            float f;
                            double d;
                            int into;

                            for (int i = 0; i < Owner.Params.Input.Count; i++)
                            {
                                string datahere = "";
                                if(Owner.Params.Input[i].Access == GH_ParamAccess.list)
                                {
                                    string ghijk = Owner.Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                                    
                                    string[] newstr = ghijk.Split(',');
                                    if(float.TryParse(newstr[0], out f) || double.TryParse(newstr[0],out d) || int.TryParse(newstr[0], out into))
                                    {
                                        if (newstr.Length == 1)
                                            datahere += ghijk;
                                        else
                                        datahere += "["+ghijk+"]";
                                    }
                                    else
                                    {
                                        if (newstr.Length == 1)
                                            datahere += "\""+ ghijk + "\"";
                                        else
                                            datahere += "[\""+ghijk.Replace(",", "\",\"")+"\"]";
                                    }
                                }
                                else if (Owner.Params.Input[i].Access == GH_ParamAccess.item)
                                {
                                    
                                    string ghijk = Owner.Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                                    string[] newstr = ghijk.Split(',');
                                    if (float.TryParse(newstr[0], out f) || double.TryParse(newstr[0], out d) || int.TryParse(newstr[0], out into))
                                    {
                                        if (newstr.Length == 1)
                                            datahere += ghijk;
                                        else
                                            datahere += "[" + ghijk + "]";
                                    }
                                    else
                                    {
                                        if (newstr.Length == 1)
                                            datahere += "\"" + ghijk + "\"";
                                        else
                                            datahere += "[\"" + ghijk.Replace(",", "\",\"") + "\"]";
                                    }
                                }


                                variablesAre += Owner.Params.Input[i].NickName + " = " + datahere + " \n";
                            }

                        }
                        catch (Exception exep)
                        {
                            MessageBox.Show(exep.ToString());
                        }

                        System.IO.File.WriteAllText(path + name + ".py", variablesAre + gg.Ps.PythonCanvas.Text);
                        try
                        {
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
                        gg.Ps.console.Text = output;

                        System.IO.File.Delete(path + name + ".py");

                        gg.Ps.BringToFront();
                    };
                    gg.Ps.close.Click += (se, ev) => gg.Ps.Close();
                    Grasshopper.Instances.RedrawCanvas();

                }

               }catch(Exception erx)
                {
                    MessageBox.Show(erx.ToString());
                }

                return Grasshopper.GUI.Canvas.GH_ObjectResponse.Handled;
            }





            System.Drawing.Rectangle rec0;
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
            private System.Drawing.Rectangle ButtonBounds { get; set; }

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



                    if (shellOpened)
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
            
            if(side == GH_ParameterSide.Input)
            {
                param.Name = GH_ComponentParamServer.InventUniqueNickname("xyzijklmnopqrstuvw", Params);
            }else if(side == GH_ParameterSide.Output)
            {
                param.Name = GH_ComponentParamServer.InventUniqueNickname("abcdefghijklmnopqrstuvwxyz", Params);
            }
            param.NickName = param.Name;
            param.Description = "Param" + (Params.Input.Count + 1);
            inputNames.Insert(index, param.Name);
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
    }


}
