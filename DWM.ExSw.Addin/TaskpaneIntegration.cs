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


namespace DWM.ExSw.Addin
{
    [ComVisible(true)]
    [Guid("7f17322d-15fe-4b8a-ba52-2390c2a60a1f")]
    [DisplayName("DailyWorkManager")]
    [Description("DWM SOLIDWORKS Add-in")]

    public class TaskpaneIntegration : ISwAddin
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
        #endregion

        #region Public Menbers
        public int buttonIdx;
        private ICommandManager iCmdMgr;
        private ISldWorks iSwApp;
        public const string SWTASKPANE_PROGID = "DWM.ExSw.Addin.Taskpane";
        #endregion

        #region Registration

        private const string ADDIN_KEY_TEMPLATE = @"SOFTWARE\SolidWorks\Addins\{{{0}}}";
        private const string ADDIN_STARTUP_KEY_TEMPLATE = @"Software\SolidWorks\AddInsStartup\{{{0}}}";
        private const string ADD_IN_TITLE_REG_KEY_NAME = "Title";
        private const string ADD_IN_DESCRIPTION_REG_KEY_NAME = "Description";

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            try
            {
                var addInTitle = "";
                var loadAtStartup = true;
                var addInDesc = "";

                var dispNameAtt = t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (dispNameAtt != null)
                {
                    addInTitle = dispNameAtt.DisplayName;
                }
                else
                {
                    addInTitle = t.ToString();
                }

                var descAtt = t.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault();

                if (descAtt != null)
                {
                    addInDesc = descAtt.Description;
                }
                else
                {
                    addInDesc = t.ToString();
                }

                var addInkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(
                    string.Format(ADDIN_KEY_TEMPLATE, t.GUID));

                addInkey.SetValue(null, 0);

                addInkey.SetValue(ADD_IN_TITLE_REG_KEY_NAME, addInTitle);
                addInkey.SetValue(ADD_IN_DESCRIPTION_REG_KEY_NAME, addInDesc);

                var addInStartupkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                    string.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));

                addInStartupkey.SetValue(null, Convert.ToInt32(loadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error while registering the addin: " + ex.Message);
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(
                    string.Format(ADDIN_KEY_TEMPLATE, t.GUID));

                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(
                    string.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while unregistering the addin: " + e.Message);
            }
        }

        #endregion
        
        public ISldWorks SwApp
        {
            get { return iSwApp; }
        }
        public ICommandManager CmdMgr
        {
            get { return iCmdMgr; }
        }
        public Hashtable OpenDocs
        {
            get { return openDocs; }
        }

        #region SolidWorks Add-in Implementation
        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            mSolidWorksApplication = (SldWorks)ThisSW;
            mSwCookie = Cookie;

            var ok = mSolidWorksApplication.SetAddinCallbackInfo2(0, this, mSwCookie);

            LoadUI();
            #region Setup the Event Handlers
            //SwEventPtr = mSolidWorksApplication;
            iSwApp = (ISldWorks)ThisSW;
            SwEventPtr = (SldWorks)iSwApp;
            openDocs = new Hashtable();
            AttachEventHandlers();
            #endregion

            return true;
        }

        public bool DisconnectFromSW()
        {
            UnloadUI();
            DetachEventHandlers();
            iSwApp = null;
            return true;
        }

        #endregion

        #region UI Methods
        public bool LoadUI()
        {
            //Coletando o diretorio da pasta com o icone
            var imagePath = Path.Combine(Path.GetDirectoryName(typeof(TaskpaneIntegration).Assembly.CodeBase).Replace(@"file:\", ""), "main.png");
            //Criando o painel lateral
            mTaskpaneView = mSolidWorksApplication.CreateTaskpaneView2(imagePath, "DWM SOLIDWORKS");
            mTaskpaneHost = (TaskpaneHostUI)mTaskpaneView.AddControl(SWTASKPANE_PROGID, string.Empty);

            //mTaskpaneView.AddCustomButton(imagePath, "Save (custom png)");

            if (mTaskpaneHost != null)
            {
                mTaskpaneHost.swApp = mSolidWorksApplication;
            }




            return true;
        }
        private void UnloadUI()
        {
            mTaskpaneHost = null;
            mTaskpaneView.DeleteView();
            Marshal.ReleaseComObject(mTaskpaneView);
            mTaskpaneView = null;

        }


        #endregion

        #region Events Methods SolidWorks
        public bool AttachEventHandlers()
        {
            AttachSwEvents();
            AttachEventsToAllDocuments();
            return true;
        }
        public bool AttachSwEvents()
        {
            try
            {
                mTaskpaneView.TaskPaneToolbarButtonClicked += this.swTaskPane_TaskPaneToolbarButtonClicked;
                SwEventPtr.ActiveDocChangeNotify += new DSldWorksEvents_ActiveDocChangeNotifyEventHandler(OnDocChange);
                SwEventPtr.DocumentLoadNotify2 += new DSldWorksEvents_DocumentLoadNotify2EventHandler(OnDocLoad);
                SwEventPtr.FileNewNotify2 += new DSldWorksEvents_FileNewNotify2EventHandler(OnFileNew);
                SwEventPtr.ActiveModelDocChangeNotify += new DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler(OnModelChange);
                SwEventPtr.FileOpenPostNotify += new DSldWorksEvents_FileOpenPostNotifyEventHandler(FileOpenPostNotify);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DetachSwEvents()
        {
            try
            {
                SwEventPtr.ActiveDocChangeNotify -= new DSldWorksEvents_ActiveDocChangeNotifyEventHandler(OnDocChange);
                SwEventPtr.DocumentLoadNotify2 -= new DSldWorksEvents_DocumentLoadNotify2EventHandler(OnDocLoad);
                SwEventPtr.FileNewNotify2 -= new DSldWorksEvents_FileNewNotify2EventHandler(OnFileNew);
                SwEventPtr.ActiveModelDocChangeNotify -= new DSldWorksEvents_ActiveModelDocChangeNotifyEventHandler(OnModelChange);
                SwEventPtr.FileOpenPostNotify -= new DSldWorksEvents_FileOpenPostNotifyEventHandler(FileOpenPostNotify);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public void AttachEventsToAllDocuments()
        {
            ModelDoc2 modDoc;
            modDoc = mSolidWorksApplication.ActiveDoc as ModelDoc2;

            if (modDoc != null)
            {
                if (!openDocs.Contains(modDoc))
                {
                    AttachModelDocEventHandler(modDoc);
                }
                else
                {
                    mTaskpaneHost.Verficacao(mSolidWorksApplication,modDoc);
                }
            }
        }
        public bool AttachModelDocEventHandler(ModelDoc2 modDoc)
        {
            if (modDoc == null)
                return false;

            DocumentEventHandler docHandler = null;
            
            if (!openDocs.Contains(modDoc))
            {
                switch (modDoc.GetType())
                {
                    case (int)swDocumentTypes_e.swDocPART:
                        {
                            docHandler = new PartEventHandler(modDoc, this);
                            
                            break;
                        }
                    case (int)swDocumentTypes_e.swDocASSEMBLY:
                        {
                            docHandler = new AssemblyEventHandler(modDoc, this);
                            break;
                        }
                    case (int)swDocumentTypes_e.swDocDRAWING:
                        {
                            docHandler = new DrawingEventHandler(modDoc, this);
                            break;
                        }
                    default:
                        {
                            return false; //Unsupported document type
                        }
                }
                
                docHandler.AttachEventHandlers(mSolidWorksApplication, modDoc,mTaskpaneHost,this);
                openDocs.Add(modDoc, docHandler);
            }
            return true;
        }
        public bool DetachModelEventHandler(ModelDoc2 modDoc)
        {
            DocumentEventHandler docHandler;
            docHandler = (DocumentEventHandler)openDocs[modDoc];
            openDocs.Remove(modDoc);
            if (openDocs.Count==0)
            {
                mTaskpaneHost.DefaultForms(mSolidWorksApplication);
            }
            modDoc = null;
            docHandler = null;
            return true;
        }
        public bool DetachEventHandlers()
        {
            DetachSwEvents();
            //Close events on all currently open docs
            DocumentEventHandler docHandler;
            int numKeys = openDocs.Count;
            object[] keys = new Object[numKeys];

            //Remove all document event handlers
            openDocs.Keys.CopyTo(keys, 0);
            foreach (ModelDoc2 key in keys)
            {
                docHandler = (DocumentEventHandler)openDocs[key];
                docHandler.DetachEventHandlers(); //This also removes the pair from the hash
                docHandler = null;
            }
            return true;
        }

        #region Events
        public int OnDocChange()
        {
            return 0;
        }
        public int FileOpenPreNotify(string FileName)
        {
            return 0;
        }
        public int OnDocLoad(string docTitle, string docPath)
        {
            return 0;
        }
        int FileOpenPostNotify(string FileName)
        {
            return 0;
        }
        public int OnFileNew(object newDoc, int docType, string templateName)
        {
            return 0;
        }
        public int OnModelChange()
        {
            AttachEventsToAllDocuments();
            return 0;
        }
        public int swTaskPane_TaskPaneToolbarButtonClicked(int ButtonIndex)
        {
            switch ((ButtonIndex + 1))
            {
                case 1:
                    Debug.Print("Save (custom png) button clicked.");

                    break;
                case 2:
                    Debug.Print("Next button clicked.");
                    
                    break;
                case 3:
                    Debug.Print("Back button clicked.");
                    break;
                case 4:
                    Debug.Print("Okay button clicked.");
                    break;
                case 5:
                    Debug.Print("Close button clicked and tab deleted.");
                    
                    break;
            }
            return 1;
        }
        #endregion

        #endregion

    }

    #region DocumentHandler

    internal class DrawingEventHandler : DocumentEventHandler
    {
        private ModelDoc2 modDoc;
        private TaskpaneIntegration taskpaneIntegration;

        public DrawingEventHandler(ModelDoc2 modDoc, TaskpaneIntegration taskpaneIntegration)
        {
            this.modDoc = modDoc;
            this.taskpaneIntegration = taskpaneIntegration;
        }
    }

    internal class AssemblyEventHandler : DocumentEventHandler
    {
        private ModelDoc2 modDoc;
        private TaskpaneIntegration taskpaneIntegration;

        public AssemblyEventHandler(ModelDoc2 modDoc, TaskpaneIntegration taskpaneIntegration)
        {
            this.modDoc = modDoc;
            this.taskpaneIntegration = taskpaneIntegration;
        }
    }

    internal class PartEventHandler : DocumentEventHandler
    {
        private ModelDoc2 modDoc;
        private TaskpaneIntegration taskpaneIntegration;

        public PartEventHandler(ModelDoc2 modDoc, TaskpaneIntegration taskpaneIntegration)
        {
            this.modDoc = modDoc;
            this.taskpaneIntegration = taskpaneIntegration;
        }
    }
    #endregion

}
