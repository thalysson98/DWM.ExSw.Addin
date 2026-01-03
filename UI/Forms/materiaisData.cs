using DWM.ExSw.Addin.Base;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DWM.ExSw.Addin.setup
{
    public partial class materiaisData : Form
    {
        DataMaterial banco;
        public materiaisData()
        {
            banco = new DataMaterial();
            InitializeComponent();
            carregarLista();
        }

        private void carregarLista()
        {
            System.Windows.Forms.ListViewItem item = null;
            foreach (MaterialInfo material in banco.DataMaterial_load())
            {
                item = new System.Windows.Forms.ListViewItem(material.Name);
                item.SubItems.Add(material.Description);
                item.SubItems.Add(material.Density);
                string[] var = new string[3];
                int i = 0;
                foreach (CustomProperty prop in material.CustomProperties)
                {
                    item.SubItems.Add(prop.Value);
                    switch (i)
                    {
                        case 0:
                            var[0] = prop.Value; break;
                        case 1:
                            var[1] = prop.Value; break;
                        case 2:
                            var[2] = prop.Value; break;
                        default: break;
                    }
                    i++;
                }
                
                materiais_list.Items.Add(item);
                valoresOriginaisListBox.Add(new string[] { material.Name, material.Description, material.Density, var[0], var[1], var[2] });

            }
        }


        private List<string[]> valoresOriginaisListBox = new List<string[]>();
        private void filtro_txt_TextChanged(object sender, EventArgs e)
        {
            if(filtro_txt.Text.Length >= 3 || filtro_txt.Text.Length == 0)
            {
                string textoFiltrado = filtro_txt.Text.ToLower();
                materiais_list.Items.Clear();

                System.Collections.IList list = valoresOriginaisListBox;
                for (int i = 0; i < list.Count; i++)
                {
                    System.Collections.IList subitens = (System.Collections.IList)list[i];
                    string valor = (string)subitens[0];
                    if (valor.ToLower().Contains(textoFiltrado))
                    {
                        System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem((string)subitens[0]);
                        item.SubItems.Add((string)subitens[1]);
                        item.SubItems.Add((string)subitens[2]);
                        item.SubItems.Add((string)subitens[3]);
                        item.SubItems.Add((string)subitens[4]);
                        item.SubItems.Add((string)subitens[5]);

                        materiais_list.Items.Add(item);
                    }
                }

            }
        }

    }
}
