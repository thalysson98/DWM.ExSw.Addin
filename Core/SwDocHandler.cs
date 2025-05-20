using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
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

        protected override void AttachAssemblyEvents(AssemblyDoc assm)
        {
            //m_Model.Selections.NewSelection += OnNewSelection;
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

        protected override void OnInit(ISwApplication app, ISwDocument doc)
        {
            
            base.OnInit(app, doc);
            m_Model = doc;

        }

        protected override void Dispose(bool disposing)
        {
            m_Model.Selections.NewSelection -= OnNewSelection;
            base.Dispose(disposing);
        }
        private int OnAddItemNotify(int EntityType, string itemName)
        {
            //Implement
            return 0;
        }

        private void OnNewSelection(IXDocument doc, IXSelObject selObject)
        {


        }





    }
}
