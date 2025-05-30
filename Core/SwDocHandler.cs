using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Linq;
using System.Windows.Shapes;
using Xarial.XCad;
using Xarial.XCad.Annotations;
using Xarial.XCad.Data;
using Xarial.XCad.Documents;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Documents.Services;

namespace DWM.ExSw.Addin.Core
{
    public class SwDocHandler : SwDocumentHandler
    {
        private ISwDocument m_Model;
        private IXProperty m_DescPrp;
        private IXDimension m_D1Dim;
        private ISwApplication swApp;
        private TaskpaneHostUI paneUI;

        private static TaskpaneHostUI s_TaskpaneHost;

        public static void SetTaskpaneHost(TaskpaneHostUI taskpaneHost)
        {
            s_TaskpaneHost = taskpaneHost;
        }


        protected override void AttachAssemblyEvents(AssemblyDoc assm)
        {
            m_Model.Selections.NewSelection += OnNewSelection;
            m_Model.Selections.ClearSelection += OnClearSelection;
            base.AttachAssemblyEvents(assm);
        }


        protected override void AttachPartEvents(PartDoc part)
        {
            part.AddItemNotify += OnAddItemNotify;
        }

        protected override void DetachPartEvents(PartDoc part)
        {
            part.AddItemNotify -= OnAddItemNotify;

        }
        protected override void DetachAssemblyEvents(AssemblyDoc assm)
        {
            m_Model.Selections.NewSelection -= OnNewSelection;
            m_Model.Selections.ClearSelection -= OnClearSelection;

        }
        protected override void OnInit(ISwApplication app, ISwDocument doc)
        {
            //base.OnInit(app, doc);
            m_Model = doc;
            swApp = app;
            paneUI = s_TaskpaneHost;
        }

        protected override void Dispose(bool disposing)
        {
            m_Model.Selections.NewSelection -= OnNewSelection;
            m_Model.Selections.ClearSelection -= OnClearSelection;
            base.Dispose(disposing);
        }

        private int OnAddItemNotify(int EntityType, string itemName)
        {
            // Implementação desejada
            return 0;
        }

        private void OnNewSelection(IXDocument doc, IXSelObject selObject)
        {

            ISwSelectionCollection sel = m_Model.Selections;
            ISwComponent comp = (ISwComponent)sel.FirstOrDefault();
            ModelDoc2 mode = comp.Component.IGetModelDoc();
            paneUI.Verficacao((SldWorks)swApp.Sw, mode);
            if (paneUI.formEstrutura != null)
            {
                paneUI.formEstrutura.GetValues("");
            }

        }
        private void OnClearSelection(IXDocument doc)
        {
            IModelDoc2  swModel = swApp.Sw.ActiveDoc as ModelDoc2;
            if(swModel == m_Model.Model) { paneUI.Verficacao((SldWorks)swApp.Sw, (ModelDoc2)m_Model.Model); }
            
        }


    }
}
