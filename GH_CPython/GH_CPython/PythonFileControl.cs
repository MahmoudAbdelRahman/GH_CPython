using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace GH_CPython
{
    class PythonFileControl
    {
        Regex line = new Regex(@"Line\(.*\)", RegexOptions.Multiline);
        /// <summary>
        /// This function is resposible for writing python files after
        /// gathering all inputs and outputs in a python-syntax form.
        /// </summary>
        /// <param name="WinForm"></param>
        public void writeReadPythonFile(GH_Component ThisComponent, 
                                        PythonShell WinForm,
                                        IGH_DataAccess DA,
                                        int index,
                                        string path)
        {

            string variablesAre = "";
            string foot = string.Empty;

            /// Section 1
            /// Initiate temporary Python file name that will be executed as well as the temporary folder.
            string name = "PythonFileWritten_" + index.ToString();

            try
            {
                variablesAre = ""; // Collecting the input variables here.

                /// Section 2
                /// Add the output variables' names and initiate them as None. 
                for (int i = 0; i < ThisComponent.Params.Output.Count; i++)
                {
                    variablesAre += ThisComponent.Params.Output[i].NickName + " = None\n";
                }

                /// Section 3. 
                /// Collect input data names, and values then initiate them in a python syntax form as : "variableName = varibaleValue \n"
                for (int i = 0; i < ThisComponent.Params.Input.Count; i++)
                {

                    string datahere = "";
                    string paramType = "";
                    try
                    {
                        paramType = ThisComponent.Params.Input[i].Sources[0].Type.ToString();
                    }catch
                    {
                        paramType = ThisComponent.Params.Input[i].Type.ToString();
                    }
                    
                    // GET input type at first
                    try
                    {
                        if (paramType == "Grasshopper.Kernel.Types.GH_String" ||
                            paramType == "Grasshopper.Kernel.Types.GH_Integer" ||
                            paramType == "Grasshopper.Kernel.Types.GH_Number" ||
                            paramType == "Grasshopper.Kernel.Types.GH_Boolean")
                        {
                            if (ThisComponent.Params.Input[i].Access == GH_ParamAccess.item)
                            {
                                ThisComponent.Params.Input[i].Access = GH_ParamAccess.list;
                                ThisComponent.ExpireSolution(true);
                            }
                            else
                            {
                                string thisInputString = ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                                datahere = recomposeInputString(thisInputString);
                            }
                        }
                        else if (paramType == "Grasshopper.Kernel.Types.GH_Line" || paramType == "Grasshopper.Kernel.Types.GH_Point")
                            datahere += doCheckforInputStaff(ThisComponent, DA, i);
                        else if (paramType == "Grasshopper.Kernel.Types.IGH_Goo")
                            datahere += doAllOtherTries(ThisComponent, DA, i);
                        else
                        {
                            datahere += "None";
                        }

                    }catch
                        {
                            //MessageBox.Show(typeExeption.ToString());
                            datahere += "None";
                        }
                    if (datahere == "") datahere = "None";
                        variablesAre += ThisComponent.Params.Input[i].NickName + " = " + datahere + " \n";

                    }
                    foot = Resources.SavedPythonFile.savingFile;
                    string thisOutputData = "";


                    for (int i = 0; i < ThisComponent.Params.Output.Count; i++)
                    {
                        if (i < ThisComponent.Params.Output.Count - 1)
                            thisOutputData += "\"" + ThisComponent.Params.Output[i].NickName + "\":" + ThisComponent.Params.Output[i].NickName + ", ";
                        else
                            thisOutputData += "\"" + ThisComponent.Params.Output[i].NickName + "\":" + ThisComponent.Params.Output[i].NickName;

                    }
                    foot = foot.Replace("##data##", thisOutputData);
                    foot = foot.Replace("##fileName##", path.Replace(@"\", "/") + @"_PythonExecutionOrder_" + index.ToString() + @".xml");

                
            }
            catch (Exception exep)
            {
                MessageBox.Show(exep.ToString());
            }

            /// Section 5.
            /// Save all data as a python file that will be run when the component is expired. 
            System.IO.File.WriteAllText(path + name + ".py", variablesAre + WinForm.PythonCanvas.Text + "\n" + foot);
        }

        private string doAllOtherTries(GH_Component ThisComponent, IGH_DataAccess DA, int i)
        {
            
            string datahere = "";

            try
            {
               datahere = doCheckforInputStaff(ThisComponent, DA, i);
            }
            catch
            {

            }
            


            return datahere;
        }

        private string doCheckforInputStaff(GH_Component ThisComponent, IGH_DataAccess DA, int i)
        {
            string datahere = "";
            string paramType;
            if (ThisComponent.Params.Input[i].Access == GH_ParamAccess.list)
            {
                ThisComponent.Params.Input[i].Access = GH_ParamAccess.item;
                ThisComponent.ExpireSolution(true);

            }
            try
            {
                paramType = ThisComponent.Params.Input[i].Sources[0].Type.ToString();
            }
            catch
            {
                paramType = ThisComponent.Params.Input[i].Type.ToString();
            }

            if (paramType == "Grasshopper.Kernel.Types.GH_Line")
            {
                Rhino.Geometry.Line inputLine = Rhino.Geometry.Line.Unset;
                DA.GetData(i, ref inputLine);
                //MessageBox.Show(.ToString());
                datahere = "\"gCPy.Line(" + inputLine.FromX.ToString() + ", "
                                           + inputLine.FromY.ToString() + ", "
                                           + inputLine.FromZ.ToString() + ", "
                                           + inputLine.ToX.ToString() + ", "
                                           + inputLine.ToY.ToString() + ", "
                                           + inputLine.ToZ.ToString() + ")\"";
            }else if
                (paramType == "Grasshopper.Kernel.Types.GH_Point")
            {

                Rhino.Geometry.Point3d inputPoint = Rhino.Geometry.Point3d.Unset;

                DA.GetData(i, ref inputPoint);
                //MessageBox.Show(.ToString());
                datahere = "\"gCPy.Point("+ inputPoint.X.ToString() + ", "
                                           + inputPoint.Y.ToString() + ", "
                                           + inputPoint.Z.ToString() + ")\"";
            }
            

            return datahere;
        }

       
        private string recomposeInputString(string thisInputString)
        {
            string datahere = "";

            // These temporary variables  are used for parsing input data into float, double or int. 
            float f;
            double d;
            int into;


            string[] newstr = thisInputString.Split(',');

            if (float.TryParse(newstr[0], out f) || double.TryParse(newstr[0], out d) || int.TryParse(newstr[0], out into) || newstr[0] == "True" || newstr[0] == "False")
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

            return datahere;
        }



    }
}
