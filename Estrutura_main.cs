using DWM.ExSw.Addin.Base;
using DWM.ExSw.Addin.DataSRV;
using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xarial.XCad.SolidWorks.Documents;

namespace DWM.ExSw.Addin
{
    public partial class Estrutura_main: Form
    {
        private TaskpaneHostUI Taskpane;
        private SldWorks swApp;
        private ModelDoc2 swModel;


        public Estrutura_main(TaskpaneHostUI taskpane)
        {
            InitializeComponent();
            Taskpane = taskpane;
        }
        public void GetValues(string codigo)
        {
            Dictionary<string, List<string>> estrutura = new Dictionary<string, List<string>>();
            estrutura = Taskpane.banco.GetEstrutura(codigo);
            Dt_EstruturaBD.Rows.Clear();
            if (codigo != null)
            {
                foreach (var item in estrutura)
                {
                    List<string> valores = item.Value;

                    string coluna1 = valores[2];
                    string coluna2 = valores[1];
                    string coluna3 = valores[3]; 
                    string coluna4 = valores[4];
                    string coluna5 = valores[5];

                    Dt_EstruturaBD.Rows.Add(coluna1, coluna2, coluna3, coluna4, coluna5);

                }
            }
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
            AssemblyStructureParser m = new AssemblyStructureParser();
            AssemblyDoc sw = (AssemblyDoc)Taskpane.swModel;
            TreeNode r = new TreeNode(m.GetTopLevelNodes(sw));
        }
        
        private void Form1_FormClosed(Object sender, FormClosedEventArgs e)
        {
            Taskpane.formEstrutura = null;
        }

        private void tree_estrutura_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
