using DWM.TaskPaneHost;
using DWM.ExSw.Addin.Core;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Shapes;
using Path = System.IO.Path;
using DWM.ExSw.Addin.DataSRV;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using Xarial.XCad.SolidWorks;
//using SolidWorks.Interop.cosworks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI;
using Xarial.XCad.Documents;
using Xarial.XCad.Features;
using Xarial.XCad.Documents.Services;
using Xarial.XCad.Documents.Extensions;
using Xarial.XCad.SolidWorks.Documents.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Xarial.XCad.Toolkit.Services;
namespace DWM.ExSw.Addin
{
    [ComVisible(true)]
    [Guid("7f17322d-15fe-4b8a-ba52-2390c2a60a1f")]
    [DisplayName("DailyWorkManager")]
    [Description("DWM SOLIDWORKS Add-in")]
    [Icon(typeof(DWM.ExSw.Addin.Properties.Resources), nameof(DWM.ExSw.Addin.Properties.Resources.main))]
    public class TaskpaneIntegration : SwAddInEx, IAddin
    {
        #region Event Handler Variables
        Hashtable openDocs;
        SldWorks SwEventPtr;
        #endregion

        #region Private Menbers
        private int mSwCookie;
        private TaskpaneView mTaskpaneView;
        private SldWorks mSolidWorksApplication;
        private TaskpaneHostUI mTaskpaneHost;
        private ICommandManager iCmdMgr;
        private ISldWorks iSwApp;
        private IXCustomPanel<TaskpaneHostUI> m_FeatMgrTab;
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

        #region Events


        #endregion




    }
}
