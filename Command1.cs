namespace addloads
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            double constLd;
            double sDl;
            double sLl;

            string paramName = "Construction Load";
            string paramName2 = "Superimposed Dead Load";
            string paramName3 = "Superimposed Live Load";

            GlobalParameter gParam1 = GetGlobalParameterByName(doc, paramName);
            GlobalParameter gParam2 = GetGlobalParameterByName(doc, paramName2);
            GlobalParameter gParam3 = GetGlobalParameterByName(doc, paramName3);

            //check to see if values exist, if not create them

            if (gParam1 == null)
            {
                using (Transaction t1 = new Transaction(doc))
                {
                    t1.Start("Add global parameter");
                    // create the parameter

                    GlobalParameter.Create(doc, paramName, SpecTypeId.Number);

                    t1.Commit();

                    gParam1 = GetGlobalParameterByName(doc, paramName);
                }
            }

            if (gParam2 == null)
            {
                using (Transaction t1 = new Transaction(doc))
                {
                    t1.Start("Add global parameter2");

                    GlobalParameter.Create(doc, paramName2, SpecTypeId.Number);

                    t1.Commit();

                    gParam2 = GetGlobalParameterByName(doc, paramName2);
                }
            }

            if (gParam3 == null)
            {
                using (Transaction t1 = new Transaction(doc))
                {
                    t1.Start("Add global parameter3");

                    GlobalParameter.Create(doc, paramName3, SpecTypeId.Number);

                    t1.Commit();

                    gParam3 = GetGlobalParameterByName(doc, paramName3);
                }
            }

            constLd = GetGlobalParamValueDouble(gParam1);
            sDl = GetGlobalParamValueDouble(gParam2);
            sLl = GetGlobalParamValueDouble(gParam3);


            input_loads currentForm = new input_loads(constLd, sDl, sLl)
            {
                Width = 400,
                Height = 220,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentForm.ShowDialog();
            string txtConst = currentForm.GetTextBoxConst();
            //TaskDialog.Show("const", txtConst);

            string txtSDL = currentForm.GetTextBoxSDL();
            //TaskDialog.Show("sdl", txtSDL);

            string txtSLL = currentForm.GetTextBoxSLL();
            //TaskDialog.Show("SLL", txtSLL);

            constLd = double.Parse(txtConst);
            sDl = double.Parse(txtSDL);
            sLl = double.Parse(txtSLL);


            using (Transaction t2 = new Transaction(doc))
            {
                t2.Start("Set Global Parameter Value");
                SetGlobalParamValue(gParam1, constLd);
                SetGlobalParamValue(gParam2, sDl);
                SetGlobalParamValue(gParam3, sLl);
                t2.Commit();
            }
            TaskDialog.Show("const ", constLd.ToString("F2"));
            TaskDialog.Show("sD", sDl.ToString("F2"));
            TaskDialog.Show("sll ", sLl.ToString("F2"));



            return Result.Succeeded;
        }


        public static GlobalParameter GetGlobalParameterByName(Document document, String name)
        {
            GlobalParameter returnParam = null;
            if (GlobalParametersManager.AreGlobalParametersAllowed(document))
            {
                ElementId paramId = GlobalParametersManager.FindByName(document, name);
                returnParam = document.GetElement(paramId) as GlobalParameter;
            }

            return returnParam;
        }


        public static double GetGlobalParamValueDouble(GlobalParameter parameter)
        {
            ParameterValue paramValue = parameter.GetValue();
            if (paramValue is DoubleParameterValue doubleValue)
            {
                return doubleValue.Value;
            }
            return 0;
        }



        public static void SetGlobalParamValue(GlobalParameter parameter, double value)
        {
            DoubleParameterValue paramValue = new DoubleParameterValue(value);
            parameter.SetValue(paramValue);
        }

    }

}
