using DWM.ExSw.Addin.Core;
using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Documents.Extensions;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI;


namespace DWM.ExSw.Addin
{
    [ComVisible(true)]
    [Guid("7f17322d-15fe-4b8a-ba52-2390c2a60a1f")]
    [DisplayName("DailyWorkManager")]
    [Description("DWM SOLIDWORKS Add-in")]
    [Icon(typeof(DWM.ExSw.Addin.Properties.Resources), nameof(DWM.ExSw.Addin.Properties.Resources.main))]
    public class TaskpaneIntegration : SwAddInEx, IAddin
    {

        #region Private Menbers
        private TaskpaneHostUI mTaskpaneHost;
        #endregion

        #region Properties
        ///<inheritdoc/>
        public string AddinDirectory { get; private set; }

        ///<inheritdoc/>
        public IServiceProvider Services { get; private set; }

        public ISldWorks Sw => Application.Sw;

        public IntPtr SwHandle => Application.WindowHandle;
        #endregion


        #region Public Menbers
        public int buttonIdx;
        public Vortex_In Vortex_In;
        public const string SWTASKPANE_PROGID = "DWM.ExSw.Addin.Taskpane";
        #endregion


        #region SolidWorks Add-in Implementation
        public override void OnConnect()
        {

            var pane = CreateTaskPane<TaskpaneHostUI>();
            mTaskpaneHost = pane.Control;
            SwDocHandler.SetTaskpaneHost(mTaskpaneHost); // passa para o handler via static

            this.Application.Documents.RegisterHandler<SwDocHandler>();
            Application.Documents.DocumentActivated += OnDocumentActivated;

        }

        public override void OnDisconnect()
        {
            this.Application.Documents.UnregisterHandler<SwDocHandler>();
            Application.Documents.DocumentActivated -= OnDocumentActivated;

        }
        private void OnDocumentActivated(IXDocument doc)
        {
            ModelDoc2 model = (ModelDoc2)Application.Documents.Active.Model;
            SldWorks app = (SldWorks)Application.Sw;
            mTaskpaneHost.Verficacao(app, model);
        }


        #endregion

    }
}
