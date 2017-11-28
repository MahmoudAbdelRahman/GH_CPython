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
                    variablesAre += ThisComponent.Params.Output[i].NickName + @" = None\n";
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
                    }
                    catch
                    {
                        paramType = ThisComponent.Params.Input[i].Type.ToString();
                    }

                    // GET input type at first
                    try
                    {
                        if (ThisComponent.Params.Input[i].Access == GH_ParamAccess.list/* &&
                            (paramType == "Grasshopper.Kernel.Types.GH_Integer" ||
                            paramType == "Grasshopper.Kernel.Types.GH_Number" ||
                            paramType == "Grasshopper.Kernel.Types.GH_Boolean")*/)
                        {
                            //MessageBox.Show(ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false));
                            string thisInputString = ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false).
                                Trim().Replace(System.Environment.NewLine + System.Environment.NewLine, "],[").Replace(System.Environment.NewLine, ",");

                            datahere = recomposeInputString(thisInputString);
                                /*string thisInputString = ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                                datahere = recomposeInputString(thisInputString);*/
                        }
                        else if (ThisComponent.Params.Input[i].Access == GH_ParamAccess.tree)
                        {
                            string thisInputString = ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false).
                                Trim().Replace(System.Environment.NewLine + System.Environment.NewLine, "],[").Replace(System.Environment.NewLine, ",");
                            thisInputString = "[" + thisInputString + "]";

                            datahere = recomposeInputStringTree(thisInputString);
                            //MessageBox.Show(ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false));
                        }
                        else
                        {
                            datahere += getInputs(ThisComponent, DA, i);
                        }

                    }
                    catch
                    {
                        datahere += "None";
                    }
                    if (datahere == "") datahere = "None";
                    variablesAre += ThisComponent.Params.Input[i].NickName + " = " + datahere + @" \n";

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
                //MessageBox.Show(exep.ToString());
            }

            /// Section 5.
            /// Save all data as a python file that will be run when the component is expired.
            string thisfilePath = "None";
            string thisfileName = "None";
            if(ThisComponent.OnPingDocument().IsFilePathDefined)
            {
                thisfilePath = ThisComponent.OnPingDocument().FilePath;
                thisfileName = ThisComponent.OnPingDocument().DisplayName;
            }else
            {
                 thisfilePath = @"C:\GH_CPython";
                 thisfileName = "unname";
            }
            


            string envVars = Resources.SavedPythonFile.initghEnv.Replace(Environment.NewLine, @"\n").Replace("##filePath##", thisfilePath.Replace(@"\", "/"));
            envVars = envVars.Replace("##fileName##", thisfileName.Replace("*", ""));
            variablesAre = Resources.SavedPythonFile.initVars.Replace("##InitVars##", variablesAre.Replace("'", @"\'")).Replace("##initGHENV##", envVars);
            System.IO.File.WriteAllText(path + name + ".py", variablesAre + "\n" + WinForm.PythonCanvas.Text + "\n" + foot);
        }




        static bool toggleOn = true;

        private string getInputs(GH_Component ThisComponent, IGH_DataAccess DA, int i)
        {
            string datahere = "";
            string paramType = "";

            try
            {
                if(ThisComponent.Params.Input[i].Access == GH_ParamAccess.item)
                {
                convetToItem(ThisComponent, i, DA);
                IGH_Goo inputObject = null;
                DA.GetData(i, ref inputObject);
                paramType = inputObject.GetType().ToString();

                if (paramType == "Grasshopper.Kernel.Types.GH_Line")
                {

                    convetToItem(ThisComponent, i, DA);
                    Rhino.Geometry.Line inputLine = Rhino.Geometry.Line.Unset;
                    DA.GetData(i, ref inputLine);
                    datahere = "\"gCPy.Line(" + inputLine.FromX.ToString() + ", "
                                               + inputLine.FromY.ToString() + ", "
                                               + inputLine.FromZ.ToString() + ", "
                                               + inputLine.ToX.ToString() + ", "
                                               + inputLine.ToY.ToString() + ", "
                                               + inputLine.ToZ.ToString() + ")\"";
                }
                else if
                   (paramType == "Grasshopper.Kernel.Types.GH_Point")
                {
                    convetToItem(ThisComponent, i, DA);
                    Rhino.Geometry.Point3d inputPoint = Rhino.Geometry.Point3d.Unset;
                    DA.GetData(i, ref inputPoint);
                    datahere = "\"gCPy.Point(" + inputPoint.X.ToString() + ", "
                                               + inputPoint.Y.ToString() + ", "
                                               + inputPoint.Z.ToString() + ")\"";
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_Circle")
                {
                    convetToItem(ThisComponent, i, DA);
                    Rhino.Geometry.Circle inputCircle = Rhino.Geometry.Circle.Unset;
                    DA.GetData(i, ref inputCircle);
                    datahere = "\"gCPy.Circle(" + inputCircle.Center.X.ToString() + ","         // xpos
                                                + inputCircle.Center.Y.ToString() + ","         // ypos
                                                + inputCircle.Center.Z.ToString() + ","         // zpos
                                                + inputCircle.Radius.ToString() + ","           // radius
                                                + inputCircle.Plane.XAxis.ToString() + ","      // x axis vector0 , x axis vector1 , x axis vector 2
                                                + inputCircle.Plane.YAxis.ToString() + ","      // y axis vector
                                                + inputCircle.Plane.ZAxis.ToString() + ")\"";   // z axis vector
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_Curve")
                {
                    datahere += "\"Ok This is a curve ... Take Care .. add all your things here\"";
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_String")
                {
                    convetToItem(ThisComponent, i, DA);
                    string inputString = string.Empty;
                    DA.GetData(i, ref inputString);
                    datahere = "\"" + inputString.Replace(Environment.NewLine,"\\n").Replace("'",@"\'").Replace("\"", "\\\\"+"\"").Replace("\\n", "\\\\n") + "\"";
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_Integer")
                {
                    convetToItem(ThisComponent, i, DA);
                    int inputInt = 0;
                    DA.GetData(i, ref inputInt);
                    datahere = inputInt.ToString();
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_Number")
                {
                    convetToItem(ThisComponent, i, DA);
                    double inputNum = 0.0;
                    DA.GetData(i, ref inputNum);
                    datahere = inputNum.ToString();
                }
                else if (paramType == "Grasshopper.Kernel.Types.GH_Boolean")
                {
                    convetToItem(ThisComponent, i, DA);
                    bool inputBool = false;
                    DA.GetData(i, ref inputBool);
                    datahere = inputBool ? "True" : "False";
                }
                else
                {
                    
                    datahere = "None";
                }

                }else
                {
                    string thisInputString = ThisComponent.Params.Input[i].VolatileData.DataDescription(false, false).Trim().Replace(System.Environment.NewLine, ",");
                    datahere = recomposeInputString(thisInputString);
                }

            }
            catch(Exception errf)
            { //MessageBox.Show(errf.ToString()); 
            }

            return datahere;
        }


        private void convetToItem(GH_Component ThisComponent, int i, IGH_DataAccess DA)
        {
            /*if (ThisComponent.Params.Input[i].Access == GH_ParamAccess.list)
            {
                ThisComponent.Params.Input[i].Access = GH_ParamAccess.item;
                ThisComponent.ExpireSolution(true);

            }*/
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


        private string recomposeInputStringTree(string thisInputString)
        {
            string datahere = "";

            // These temporary variables  are used for parsing input data into float, double or int. 
            float f;
            double d;
            int into;


            string[] newstr = thisInputString.Split(',');
            newstr[0] = newstr[0].Replace("[", "");

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
