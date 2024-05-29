using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWM.ExSw.Addin.setup.info
{
    public class Ppr
    {
        public string codigo { get { return "NumeroDesenho"; } }
        public string denominacao { get { return "Denominação"; } }
        public string descricao { get { return "Descrição"; } }
        public string material { get { return "Material"; } }
        public string revisao { get { return "Revisão"; } }
        public string projetista { get { return "Projetista"; } }
        public string projetistaData { get { return "Data Projeto"; } }
        public string desenhista { get { return "Revisor"; } }
        public string desenhistaData { get { return "Data Revisão"; } }
        public string revisor { get { return "Aprovador"; } }
        public string revisorData { get { return "Data Aprovação"; } }
        public string perfilestrutural { get { return "PerfilEstrutural"; } }
        public string comercial { get { return "Soldagem"; } }
        public string comprimento { get { return "Comprimento"; } }
        public string tipo { get { return "Tipo"; } }
        public string subgrupo { get { return "SubGrupo"; } }
        public string unidade { get { return "Unidade"; } }
        public string massa { get { return "Massa"; } }
        public string pbruto { get { return "Peso Bruto"; } }


    }
}
