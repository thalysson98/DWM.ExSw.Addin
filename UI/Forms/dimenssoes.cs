using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.cosworks;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using DWM.ExSw.Addin.Base;

namespace DWM.ExSw.Addin.setup
{
    public partial class dimenssoes : Form
    {
        SldWorks swApp;
        ModelDoc2 swModel;
        TaskpaneHostUI taskpane;
        public object[,] DimensionSelect;
        swSpecialComands swComands;
        public dimenssoes(SldWorks app, ModelDoc2 model,TaskpaneHostUI task)
        {
            swApp = app;
            swModel = model;
            taskpane = task;
            swComands = new swSpecialComands();
            InitializeComponent();
            TraverseDimensions(swModel);
        }

        private void Apply_bt_Click(object sender, EventArgs e)
        {
            if(dimens_list.SelectedItems.Count != 0)
            {
                taskpane.textDenominacao_txt.Text = taskpane.textDenominacao_txt.Text + '\"' + dimens_list.SelectedItems[0].SubItems[0].Text + '\"';
            }
        }
        public void TraverseDimensions(ModelDoc2 swModel)
        {
            Configuration config = (Configuration)swModel.GetActiveConfiguration();
            Feature startFeat = (Feature)swModel.FirstFeature();
            object[] vFeats = (object[])GetAllFeatures(startFeat);
            object[] vDispDims = (object[])GetAllDimensions(vFeats);
            DimensionSelect = new object[vDispDims.Length, 2];
            if (vDispDims != null && vDispDims.Length > 0)
            {
                for (int i = 0; i < vDispDims.Length; i++)
                {
                    DisplayDimension swDispDim = (DisplayDimension)vDispDims[i];
                    Dimension swDim = (Dimension)swDispDim.GetDimension2(0);
                    double val = (double)swDim.GetSystemValue2(config.Name)*1000;

                    ListViewItem item = new ListViewItem(new string[] { swDim.GetNameForSelection().ToString() , val.ToString("0.00") });
                    dimens_list.Items.Add(item);
                    DimensionSelect[i,0]= swDim;
                    DimensionSelect[i, 1] = swDim.GetNameForSelection().ToString();
                }
                
            }
        }

        private object GetAllDimensions(object[] vFeats)
        {
            List<DisplayDimension> swDimsColl = new List<DisplayDimension>();

            for (int i = 0; i < vFeats.Length; i++)
            {
                Feature swFeat = (Feature)vFeats[i];
                DisplayDimension swDispDim = (DisplayDimension)swFeat.GetFirstDisplayDimension();

                while (swDispDim != null)
                {
                    if (!Contains(swDimsColl, swDispDim))
                    {
                        swDimsColl.Add(swDispDim);
                    }

                    swDispDim = (DisplayDimension)swFeat.GetNextDisplayDimension(swDispDim);
                }
            }

            return swDimsColl.ToArray();
        }

        private object GetAllFeatures(Feature startFeat)
        {
            List<Feature> swProcFeatsColl = new List<Feature>();

            Feature swFeat = startFeat;

            while (swFeat != null)
            {
                if (swFeat.GetTypeName2() != "HistoryFolder")
                {
                    if (!Contains(swProcFeatsColl, swFeat))
                    {
                        swProcFeatsColl.Add(swFeat);
                    }

                    CollectAllSubFeatures(swFeat, swProcFeatsColl);
                }

                swFeat = (Feature)swFeat.GetNextFeature();
            }

            return swProcFeatsColl.ToArray();
        }

        private void CollectAllSubFeatures(Feature parentFeat, List<Feature> procFeatsColl)
        {
            Feature swSubFeat = (Feature)parentFeat.GetFirstSubFeature();

            while (swSubFeat != null)
            {
                if (!Contains(procFeatsColl, swSubFeat))
                {
                    procFeatsColl.Add(swSubFeat);
                }

                CollectAllSubFeatures(swSubFeat, procFeatsColl);
                swSubFeat = (Feature)swSubFeat.GetNextSubFeature();
            }
        }

        private bool Contains(List<Feature> coll, Feature item)
        {
            foreach (Feature feat in coll)
            {
                if (feat == item)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Contains(List<DisplayDimension> coll, DisplayDimension item)
        {
            foreach (DisplayDimension dim in coll)
            {
                if (dim == item)
                {
                    return true;
                }
            }
            return false;
        }

        private void dimens_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dimens_list.SelectedItems.Count > 0)
            {
                string listvalue = dimens_list.SelectedItems[0].SubItems[0].Text;

                for (int i = 0; i < DimensionSelect.GetLength(0); i++)
                {
                    if (listvalue == DimensionSelect[i, 1].ToString())
                    {
                        swModel.ClearSelection();
                        swModel.Extension.SelectByID2(listvalue, "DIMENSION", 0, 0, 0, false, 0, null, 0);
                        (swModel).GraphicsRedraw2();
                    }
                }
            }
        }

    }

}
