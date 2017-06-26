using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PythonInterface
{
    class dC:Grasshopper.Kernel.Attributes.GH_ComponentAttributes
    {
        PythonInterfaceComponent pIC;
        dC ():
            base(new PythonInterfaceComponent())
        {

        }
        public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            pIC.displayText();
        }
    }
}
