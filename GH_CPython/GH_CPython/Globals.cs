using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GH_CPython
{
    public static class Globals
    {

        // INPUTS NAMES
        public static Dictionary<int, string> AllInputsNames = new Dictionary<int, string>();


        // INPUTS DATA

        public static Dictionary<int, string> AllInputs = new Dictionary<int, string>();
        public static Dictionary<int,int> AllIntInputs = new Dictionary<int,int>();

        // OUTPUTS
        public static Dictionary<int, string> AllOutputs = new Dictionary<int, string>();
    }
}
