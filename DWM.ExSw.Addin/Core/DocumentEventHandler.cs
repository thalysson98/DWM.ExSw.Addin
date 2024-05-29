using DWM.TaskPaneHost;
using DWM.ExSw.Addin;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DWM.ExSw.Addin.Core
{
    internal class DocumentEventHandler
    {
        ModelDoc2 m_Model;
        SldWorks swApp;
        TaskpaneHostUI mTaskpaneHost;
        TaskpaneIntegration Integration;
        internal void AttachEventHandlers(SldWorks app, ModelDoc2 model, TaskpaneHostUI taskpane, TaskpaneIntegration main)
        {
            m_Model = model;
            swApp = app;
            mTaskpaneHost = taskpane;
            Integration = main;
            if (m_Model is PartDoc)
            {
                (m_Model as PartDoc).DestroyNotify2 += OnDestroyNotify;
                (m_Model as PartDoc).FileReloadNotify += OnFileReloadNotify;
            }
            else if (m_Model is AssemblyDoc)
            {
                (m_Model as AssemblyDoc).DestroyNotify2 += OnDestroyNotify;
                (m_Model as AssemblyDoc).FileReloadNotify += OnFileReloadNotify;
            }
            else if (m_Model is DrawingDoc)
            {
                (m_Model as DrawingDoc).DestroyNotify2 += OnDestroyNotify;
                (m_Model as DrawingDoc).FileReloadNotify += OnFileReloadNotify;
            }
            mTaskpaneHost.Verficacao(swApp, m_Model);
        }

        internal void DetachEventHandlers()
        {
            if (m_Model is PartDoc)
            {
                (m_Model as PartDoc).DestroyNotify2 -= OnDestroyNotify;
                (m_Model as PartDoc).FileReloadNotify -= OnFileReloadNotify;
            }
            else if (m_Model is AssemblyDoc)
            {
                (m_Model as AssemblyDoc).DestroyNotify2 -= OnDestroyNotify;
                (m_Model as AssemblyDoc).FileReloadNotify -= OnFileReloadNotify;
            }
            else if (m_Model is DrawingDoc)
            {
                (m_Model as DrawingDoc).DestroyNotify2 -= OnDestroyNotify;
                (m_Model as DrawingDoc).FileReloadNotify -= OnFileReloadNotify;
            }
            
        }
        private int OnFileReloadNotify()
        {
            return 0;
        }
        private int OnDestroyNotify(int destroyType)
        {
            const int S_OK = 0;

            if (destroyType == (int)swDestroyNotifyType_e.swDestroyNotifyDestroy)
            {
                Integration.DetachModelEventHandler(m_Model);
            }
            else if (destroyType == (int)swDestroyNotifyType_e.swDestroyNotifyHidden)
            {
                Integration.DetachModelEventHandler(m_Model);
            }
            else
            {
                Debug.Assert(false, "Not supported type of destroy");
            }

            return S_OK;
        }


    }
}
