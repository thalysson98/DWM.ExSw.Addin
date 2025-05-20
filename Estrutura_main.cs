using DWM.ExSw.Addin.DataSRV;
using DWM.TaskPaneHost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWM.ExSw.Addin
{
    public partial class Estrutura_main: Form
    {
        cardallData Banco;
        TaskpaneHostUI Taskpane;

        public Estrutura_main(cardallData banco, TaskpaneHostUI taskpane)
        {
            InitializeComponent();
            Banco = banco;
            Taskpane = taskpane;
        }
        public void GetValues(string codigo)
        {
            Dictionary<string, List<string>> estrutura = new Dictionary<string, List<string>>();
            estrutura = Banco.GetEstrutura(codigo);
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
    }
}
