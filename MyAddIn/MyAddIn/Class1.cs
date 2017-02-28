using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

namespace MyAddIn
{
    [PluginAttribute("MyTest", "CECI", ToolTip = "This is a test", DisplayName = "MyTest")]
    [AddInPluginAttribute(AddInLocation.AddIn)]

    public class Class1 : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            Model ofirstModel = oDoc.Models[0];
            string filename = ofirstModel.FileName;
            ModelItem oRootItem = ofirstModel.RootItem;
            string displayname = oRootItem.DisplayName;

            ModelItemCollection oMColl = oDoc.CurrentSelection.SelectedItems;
            int number = oMColl.Count;

            if (number != 0)
            {
                foreach (PropertyCategory oPC in oMColl[0].PropertyCategories)
                {
                    string PCName = oPC.DisplayName;
                    foreach (DataProperty oDP in oPC.Properties)
                    {
                        string DPName = oDP.DisplayName;
                        if (oDP.Value.IsDisplayString)
                        {
                            string DPValue = oDP.Value.ToDisplayString();
                        }
                    }
                }
            }

            // Search
            Search oSearch =new Search();
            oSearch.Selection.SelectAll();
            oSearch.SearchConditions.Add(SearchCondition.HasPropertyByDisplayName("圖元控點", "值").EqualValue(VariantData.FromDisplayString("16C17")));
            ModelItemCollection oNewColl = oSearch.FindAll(oDoc, false);
            oDoc.CurrentSelection.CopyFrom(oNewColl);

            return 0;
        }
    }

}
