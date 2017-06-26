using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Parameters;

namespace PythonInterface
{
    public class PythonInterfaceComponent : GH_Component, IGH_VariableParameterComponent
    {
        string savedDAta = "";
        string initShell;
        PythonShell Ps = new PythonShell();
        private string at = DateTime.Now.ToString("dddd MMMM yyyy hh:mm:ss ");



        /// </summary>
        public PythonInterfaceComponent()
            : base("PythonInterface", "PyIn",
                "a python IDE interface",
                "Maths", "Script")
        {
            Ps.Hide();
            Ps.Load += Ps_Load;
        }

        void close_Click(object sender, EventArgs e)
        {
            this.Message = "Closed Click";
            Ps.Hide();
        }
        void PythonCanvas_TextChanged(object sender, EventArgs e)
        {
            initShell = Ps.PythonCanvas.Text;
            savedDAta = Ps.PythonCanvas.Text;
        }
        void Ps_Load(object sender, EventArgs e)
        {
            string Name = System.Environment.UserName;
            initShell = Resources.SavedPythonFile.Shellinit.Replace("##CreatedBy##", Name);
            initShell = initShell.Replace("##at##", at);
            Ps.PythonCanvas.Text = initShell;
            this.Message = initShell;

        }



        void Ps_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;
            initShell = Ps.PythonCanvas.Text;
            this.Message = initShell;
            Ps.PythonCanvas.Text = initShell;
            Ps.Hide();
        }

        
        public void DisplayForm()
        {
            Ps.Show();
            Ps.close.Click += close_Click;
            Ps.FormClosing += Ps_FormClosing;
            Ps.PythonCanvas.TextChanged += PythonCanvas_TextChanged;

        }

        public override void CreateAttributes()
        {
            m_attributes = new AttribCompo(this);
        }

        protected override void AppendAdditionalComponentMenuItems(System.Windows.Forms.ToolStripDropDown menu)
        {
            Menu_AppendItem(menu, "open", Open_pyte);
        }

        private void Open_pyte(object sender, EventArgs e)
        {
            DisplayForm();
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
            pManager.AddGenericParameter("out", "out", "", GH_ParamAccess.item);
            pManager.AddGenericParameter("a", "a", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            DA.SetData(0, initShell);
                
                
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
        public AttribCompo(IGH_Component PythonInterfaceComponent) : base(PythonInterfaceComponent) { }
        public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            PythonInterfaceComponent gg = new PythonInterfaceComponent();
            gg.DisplayForm();
            return Grasshopper.GUI.Canvas.GH_ObjectResponse.Handled;

            
        }
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
        Param_Number param = new Param_Number();
        param.Name = GH_ComponentParamServer.InventUniqueNickname("abcdefghijklmnopqrstuvwxyz", Params.Input);
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
    }
}
