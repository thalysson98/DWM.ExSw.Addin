using System;
using System.Windows.Forms;

namespace DWM.ExSw.Addin.Config
{
    public partial class mainconfig : Form
    {
        public mainconfig()
        {
            InitializeComponent();
        }

        private void SaveConfig_bt_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.XMLMaterial = Caminho_txt.Text;
            Properties.Settings.Default.User_Projetista = projetista_txt.Text;
            Properties.Settings.Default.Save();

        }

        private void mainconfig_Load(object sender, EventArgs e)
        {
            Caminho_txt.Text = Properties.Settings.Default.XMLMaterial;
            projetista_txt.Text = Properties.Settings.Default.User_Projetista;
        }
    }
}
