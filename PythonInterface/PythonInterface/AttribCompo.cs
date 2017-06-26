using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PythonInterface
{
    class AttribCompo:GH_ComponentAttributes
    {
        public AttribCompo(IGH_Component PythonInterfaceComponent) : base(PythonInterfaceComponent) { }
        public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
           // PythonInterfaceComponent.DisplayForm();
            return Grasshopper.GUI.Canvas.GH_ObjectResponse.Handled;
        }
    }
}
