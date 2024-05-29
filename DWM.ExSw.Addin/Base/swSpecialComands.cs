using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;

namespace DWM.ExSw.Addin.Base
{
    public class swSpecialComands
    {
        public string sw_GetNameFile(ModelDoc2 model)
        {
            try
            {
                string NameFile = model.GetPathName();
                string extractedName = NameFile.Substring(NameFile.LastIndexOf('\\') + 1);
                return extractedName.Substring(0, extractedName.Length - 7);
            }
            catch { return ""; }
            
        }

        #region Properties
        public string sw_GetCustomProperty(string PropertyName, ModelDoc2 model,string config,out string val)
        {
            CustomPropertyManager swCustProp;
            swCustProp = model.Extension.CustomPropertyManager[config];
            string valout;
            swCustProp.Get3(PropertyName, false, out val, out valout);
            return valout;
        }
        public void sw_GetAllvaluesProperty(string PropertyName, CustomPropertyManager swCustProp,out string val1, out string val2)
        {
            swCustProp.Get2(PropertyName, out val1, out val2);
        }
        #endregion

        public string[] sw_GetConfigurations(ModelDoc2 model)
        {
            return (string[])model.GetConfigurationNames();
        }
        public void SW_SelectOBJ(ModelDoc2 model, object varObj)
        {
            if (model == null) { return; }
            SelectionMgr selMgr;
            selMgr = (SelectionMgr)model.SelectionManager;
            if (varObj != null & selMgr != null)
            {
                selMgr.AddSelectionListObject(varObj, selMgr);
                (model).GraphicsRedraw2();
            }
        }//Seleciona Objeto no ModelView
        public void SW_DeleteCutList(ModelDoc2 model)
        {
            bool isInit = false;
            Feature[] swCutListFeats = null;

            if(model == null) { return; }
            model.ClearSelection2(true);
            Feature swFeat = (Feature)model.FirstFeature();
            
            while (swFeat != null)
            {
                if (swFeat.GetTypeName2() == "CutListFolder")
                {
                    if (!isInit)
                    {
                        SelectionMgr selMgr = (SelectionMgr)model.SelectionManager;
                        selMgr.AddSelectionListObject(swFeat, null);
                        isInit = true;
                        swCutListFeats = new Feature[1];
                    }
                    else
                    {
                        SelectionMgr selMgr = (SelectionMgr)model.SelectionManager;
                        selMgr.AddSelectionListObject(swFeat, null);
                        Array.Resize(ref swCutListFeats, swCutListFeats.Length + 1);
                    }
                    swCutListFeats[swCutListFeats.Length - 1] = swFeat;
                }
                swFeat = (Feature)swFeat.GetNextFeature();
            }
            model.DeleteSelection(true);

        }//Exclui lista de corte
        public void SW_UpdateCutlist(ModelDoc2 model)
        {
            if (model == null) { return; }
            Feature swFeat = (Feature)model.FirstFeature();
            while (swFeat != null)
            {
                if (swFeat.GetTypeName2() == "SolidBodyFolder")
                {
                    BodyFolder swBodyFolder = (BodyFolder)swFeat.GetSpecificFeature2();
                    swBodyFolder.UpdateCutList();
                }
                swFeat = (Feature)swFeat.GetNextFeature();
            }
        }
        public void SW_AddProperty(CustomPropertyManager swCustProp, string PPRName, string PPRValue)
        {
            swCustProp.Add2(PPRName, (int)swCustomInfoType_e.swCustomInfoText, PPRValue);
        }
        public void sw_DeleteProperty(CustomPropertyManager swCustProp, string PPRName)
        {
            swCustProp.Delete2(PPRName);
        }
        public object GetCutLists(ModelDoc2 model)
        {
            Feature[] swCutListFeats = null;
            bool isInit = false;
            try
            {
                Feature swFeat = (Feature)model.FirstFeature();
                if (swFeat != null)
                {
                    for (int i = 0; i < model.GetFeatureCount(); i++)
                    {
                        if (swFeat != null)
                        {
                            if (swFeat.GetTypeName2() == "CutListFolder")
                            {
                                if (!isInit)
                                {
                                    isInit = true;
                                    swCutListFeats = new Feature[1];
                                }
                                else
                                {
                                    Array.Resize(ref swCutListFeats, swCutListFeats.Length + 1);
                                }
                                swCutListFeats[swCutListFeats.Length - 1] = swFeat;
                            }
                            if(i+1< model.GetFeatureCount())
                            {
                                swFeat = (Feature)swFeat.GetNextFeature();
                            }
                            
                        }
                    }
                }

                
            }
            catch
            {
                return null;
            }

            if (isInit)
            {
                return swCutListFeats;
            }
            else
            {
                return null;
            }
        }


        public Component2[] GetCompModels(AssemblyDoc assy)
        {
            object[] vComps = (object[])assy.GetComponents(true);

            List<Component2> peçatotal = new List<Component2>();

            foreach (object comp in vComps)
            {
                Component2 swComp = (Component2)comp;

                if (!swComp.IsSuppressed())
                {
                    if (!ContainsComponent(peçatotal.ToArray(), swComp))
                    {
                        peçatotal.Add(swComp);
                    }
                }
            }

            if (peçatotal.Count == 0)
            {
                return new Component2[0];
            }
            else
            {
                return peçatotal.ToArray();
            }
        }
        private bool ContainsComponent(Component2[] comps, Component2 swComp)
        {
            foreach (Component2 thisComp in comps)
            {
                if (thisComp.GetPathName() == swComp.GetPathName())
                {
                    return true;
                }
            }

            return false;
        }




    }
}
