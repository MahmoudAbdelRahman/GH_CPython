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

namespace GH_CPython
{
    public class PythonInterfaceComponent : GH_Component, IGH_VariableParameterComponent
    {
        string savedDAta = "";
        string initShell;
        PythonShell Ps = new PythonShell();



        /// </summary>
        public PythonInterfaceComponent()
            : base("GH_CPython", "GH_CPython",
                "a python IDE interface",
                "Maths", "Script")
        {

            this.Params.ParameterNickNameChanged += Params_ParameterNickNameChanged;
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
            e.Cancel = true;
            initShell = Ps.PythonCanvas.Text;
            this.Message = initShell;
            Ps.PythonCanvas.Text = initShell;
            Ps.Hide();
        }


        public override void CreateAttributes()
        {
            m_attributes = new AttribCompo(this);
        }

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {

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
            pManager.AddTextParameter("x", "x", "", GH_ParamAccess.item, "00");
            pManager.AddTextParameter("y", "y", "", GH_ParamAccess.item, "00");

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("a", "a", "", GH_ParamAccess.item);
            pManager.AddGenericParameter("b", "b", "", GH_ParamAccess.item);
        }
        string data;
        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            data = this.Params.Input[0].NickName;
            DA.SetData(0, data);



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
            public AttribCompo(IGH_Component PythonInterfaceComponent)
                : base(PythonInterfaceComponent)
            {
                string Name = System.Environment.UserName;
                ChangedText = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
                ChangedText = ChangedText.Replace("##at##", at);

            }
            public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
            {
                if (!shellOpened)
                {
                    shellOpened = true;
                    PythonInterfaceComponent gg = new PythonInterfaceComponent();
                    gg.Ps.TopMost = true;
                    pythonRect = gg.Ps.RectangleToClient(Grasshopper.Instances.ActiveCanvas.DisplayRectangle);
                    gg.Ps.Show();
                    if(gg.retrievedData!="")
                    {
                        gg.Ps.PythonCanvas.Text = gg.retrievedData;

                    }else
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
                    gg.Ps.close.Click += (se, ev) => gg.Ps.Close();
                    Grasshopper.Instances.RedrawCanvas();

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
            param.Name = GH_ComponentParamServer.InventUniqueNickname("abcdefghijklmnopqrstuvwxyz", Params);
            param.NickName = param.Name;
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
    }
}
