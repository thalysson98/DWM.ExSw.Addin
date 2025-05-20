using DWM.ExSw.Addin;
using DWM.ExSw.Addin.Base;
using DWM.ExSw.Addin.Config;
using DWM.ExSw.Addin.setup.info;
using DWM.ExSw.Addin.setup;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using DWM.ExSw.Addin.Properties;
using Microsoft.SqlServer.Server;
using static System.Windows.Forms.DataFormats;
using DWM.ExSw.Addin.DataSRV;
using DWM.ExSw.Addin.Core;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Sockets;
using Xarial.XCad.Base.Attributes;

namespace DWM.TaskPaneHost
{
    [ProgId(TaskpaneIntegration.SWTASKPANE_PROGID)]
    [Icon(typeof(DWM.ExSw.Addin.Properties.Resources), nameof(DWM.ExSw.Addin.Properties.Resources.main))]

    public partial class TaskpaneHostUI : UserControl
    {
        #region Public Methods
        public SldWorks swApp;
        public ModelDoc2 swModel;
        public ClientComandsCardall cardalcomands;
        public swSpecialComands swComands;
        public object[,] EstruturaOBJ;
        public bool Comercial;
        public Vortex_In vortexcomands;
        #endregion

        #region Private Methods
        private int sortColumn = -1;
        DataMaterial materiais_bc;
        Ppr ppr;
        ErrorList errors;
        cardallData banco;
        #endregion
        public TaskpaneHostUI()
        {
            if(Settings.Default.DataServer == false)
            {
                banco = new cardallData();
                //cardalcomands.loadData(banco, XML_MATERIAIS);
                banco.Main();
            }
            if (Settings.Default.XMLMaterial !="") 
            {
                materiais_bc = new DataMaterial();
                XML_MATERIAIS = materiais_bc.lista_material(materiais_bc);
            }
            vortexcomands = new Vortex_In();
            cardalcomands = new ClientComandsCardall();
            swComands = new swSpecialComands();
            errors = new ErrorList();
            ppr = new Ppr();

            InitializeComponent();

        }
        private List<string[]> XML_MATERIAIS = new List<string[]>();
        public void TaskpaneHostUI_Load(object sender, EventArgs e)
        {
            try
            {
                sortColumn = 2;
                Estrutura_list.Sorting = SortOrder.Ascending;
                Estrutura_list.Sort();
                Estrutura_list.ListViewItemSorter = new ListViewItemComparer(sortColumn, Estrutura_list.Sorting);
                Atualizar_bt_Click(sender, e);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error while registering the addin: " + ex.Message);
            }
        }

        public void Verficacao(SldWorks app,ModelDoc2 model)
        {
            if (app != null)
            {
                Estrutura_list.Items.Clear();
                if (model != null ) 
                {
                    swModel = model;
                    swApp = app;
                    Comercial_check.Checked = cardalcomands.ComercialEstado(swModel);
                    EstadoComercial(Comercial_check.Checked);

                    if ((int)swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
                    {
                        ObterValores();
                    }
                    else if ((int)swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        ObterValores();
                    }
                    else if((int)swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                    {
                        DefaultForms(swApp);
                    }

                }
                else
                {
                    DefaultForms(swApp);
                }

            }
        }
        public void DefaultForms(SldWorks swApp)
        {
            swModel = null;
            var colors = ErrorList.GetLabelColors(0);
            Comercial_check.Checked = false;
            #region Labels text
            Codigo_txt.Text = "";
            Denominacao_txt.Text = "";
            textDenominacao_txt.Text = "";
            material_txt.Text = "";
            revisao_txt.Text = "";
            projetista_cb.Text = "";
            desenhista_cb.Text = "";
            revisor_cb.Text = "";
            projetistaData_txt.Text = "";
            desenhistaData_txt.Text = "";
            revisorData_txt.Text = "";
            tipo_cb.Text = "";
            sub_cb.Text = "";
            unidade_cb.Text = "";
            pesobt_txt.Text = "";

            #endregion

            //ListaDeCorte_check.Checked = false;
            

            #region Colors Labels
            Codigo_txt.BackColor = colors.Item1;
            Codigo_txt.ForeColor = colors.Item2;

            Denominacao_txt.BackColor = colors.Item1;
            Denominacao_txt.ForeColor = colors.Item2;

            revisao_txt.BackColor = colors.Item1;
            revisao_txt.ForeColor = colors.Item2;

            projetista_cb.BackColor = colors.Item1;
            projetista_cb.ForeColor= colors.Item2;
            projetistaData_txt.BackColor = colors.Item1;
            projetistaData_txt.ForeColor = colors.Item2;

            desenhista_cb.BackColor = colors.Item1;
            desenhista_cb.ForeColor = colors.Item2;
            desenhistaData_txt.BackColor  = colors.Item1;
            desenhistaData_txt.ForeColor=colors.Item2;

            revisor_cb.BackColor = colors.Item1;
            revisor_cb.ForeColor = colors.Item2;
            revisorData_txt.BackColor = colors.Item1;
            revisorData_txt.ForeColor = colors.Item2;
            #endregion

            Estrutura_list.Items.Clear();
        }
        private void ObterValores()
        {
            int err;
            string denominacao;
            string revisao;
            string material;
            string strVar;
            if(swModel != null)
            {
                #region APLICANDO PROPRIEDADES
                if (!Comercial)
                {
                    Codigo_txt.Text = cardalcomands.ValidandoCodigo(swModel, out err, false, swComands.sw_GetNameFile(swModel));//CAMPO CODIGO
                    if(Codigo_txt.Text == "")
                    {
                        Codigo_txt.Text = swModel.GetTitle();
                    }
                    #region REVISÃO
                    revisao_txt.Text = swComands.sw_GetCustomProperty(ppr.revisao, swModel, "", out revisao);//CAMPO REVISÃO
                    if (revisao_txt.Text == "")
                    {
                        revisao_txt.Text = "A";
                    }
                    Dictionary<string, List<string>> estrutura = new Dictionary<string, List<string>>();
                    string codigo = Codigo_txt.Text + revisao_txt.Text;
                    estrutura = banco.GetEstrutura(codigo);
                    list_estruturaBD.Items.Clear();
                    if (codigo != null)
                    {
                        foreach (var item in estrutura)
                        {
                            string chave = item.Key;
                            List<string> valores = item.Value;

                            Console.WriteLine($"Chave: {chave}");
                            Console.WriteLine("Valores:");
                            var items = new ListViewItem(new string[] { (string)valores[2], (string)valores[1], $"{(string)valores[3]} X {(string)valores[4]}", "", (string)valores[5] }, "");
                            list_estruturaBD.Items.Add(items);

                        }
                    }

                }

                #endregion

                #region DENOMINAÇÃO
                material_txt.Text = swComands.sw_GetCustomProperty(ppr.material, swModel, "", out material);
                swComands.sw_GetCustomProperty(ppr.denominacao, swModel, "", out denominacao);//obtem valor da propriedade
                textDenominacao_txt.Text = denominacao;//escreve o valor da propriedade no campo denominacao
                if (!Comercial)
                {
                    Denominacao_txt.Text = Codigo_txt.Text + revisao_txt.Text + " " + cardalcomands.validandoDenominacao(swModel, textDenominacao_txt.Text);
                }
                else
                {
                    Denominacao_txt.Text = cardalcomands.validandoDenominacao(swModel, textDenominacao_txt.Text);
                }
                #endregion

                projetista_cb.Text = swComands.sw_GetCustomProperty(ppr.projetista, swModel, "", out strVar);
                projetistaData_txt.Text = swComands.sw_GetCustomProperty(ppr.projetistaData, swModel, "", out strVar);

                desenhista_cb.Text = swComands.sw_GetCustomProperty(ppr.desenhista, swModel, "", out strVar);
                desenhistaData_txt.Text = swComands.sw_GetCustomProperty(ppr.desenhistaData, swModel, "", out strVar);

                revisor_cb.Text = swComands.sw_GetCustomProperty(ppr.revisor, swModel, "", out strVar);
                revisorData_txt.Text = swComands.sw_GetCustomProperty(ppr.revisorData, swModel, "", out strVar);

                tipo_cb.Text = swComands.sw_GetCustomProperty(ppr.tipo, swModel, "", out strVar);
                sub_cb.Text = swComands.sw_GetCustomProperty(ppr.subgrupo, swModel, "", out strVar);
                unidade_cb.Text = swComands.sw_GetCustomProperty(ppr.unidade, swModel, "", out strVar);

                pesobt_txt.Text = swComands.sw_GetCustomProperty(ppr.pbruto, swModel, "", out strVar);

                #endregion
            }

        }
        private void EstadoComercial(bool estado)
        {
            if (estado)
            {
                cabecalho_pane.Visible = false;
                CRM.Enabled = false;
            }
            else
            {
                cabecalho_pane.Visible=true;
                CRM.Enabled = true;
            }
            Comercial = estado;
        }
        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainconfig foms = new mainconfig();
            foms.Show();

        }
        private void Atualizar_bt_Click(object sender, EventArgs e)
        {
            swModel = null;
            if (swApp != null)
            {
                DefaultForms(swApp);
                swModel = swApp.ActiveDoc as ModelDoc2;
                Verficacao(swApp, swModel);
                if(swModel!= null)
                {
                    if ((int)swModel.GetType() == (int)swDocumentTypes_e.swDocPART)
                    {
                        ObterValores();
                        if (!Comercial)
                        {
                            #region Validação Estrutura
                            if (ListaDeCorte_check.Checked == true)
                            {
                                swComands.SW_DeleteCutList(swModel);
                                swComands.SW_UpdateCutlist(swModel);
                                ListaDeCorte_check.Checked = false;
                            }
                            EstruturaOBJ = null;
                            EstruturaOBJ = cardalcomands.GetEstrutura(swModel);
                            if (EstruturaOBJ != null)
                            {
                                for (int i = 0; i < EstruturaOBJ.GetLength(0); i++)
                                {
                                    if (EstruturaOBJ[i, 0] != null)
                                    {
                                        object[] arrayInterno = (object[])EstruturaOBJ[i, 0];
                                        if((string)arrayInterno[1] != "PESO ZERO")
                                        {
                                            var item = new ListViewItem(new string[] { (string)arrayInterno[0], (string)arrayInterno[1], (string)arrayInterno[2], (string)arrayInterno[3], (string)arrayInterno[4], (string)arrayInterno[6] });
                                            var colors = ErrorList.GetLabelColors(Convert.ToInt32(arrayInterno[5]));
                                            item.BackColor = colors.Item1;
                                            item.ForeColor = colors.Item2;

                                            Estrutura_list.Items.Add(item);
                                            
                                        }
                                    }
                                }
                                calcPB_bt_Click(sender, e);
                            }
                            #endregion
                        }
                        else
                        {
                            Estrutura_list.Items.Clear();
                        }
                    }
                    else if ((int)swModel.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
                    {
                        ObterValores();
                        object[] varComp;

                        varComp = swComands.GetCompModels((AssemblyDoc)swModel);
                        if (varComp != null)
                        {
                            int I = 0;
                            for (I = 0; I < varComp.Length; I++)
                            {
                                Component2 swComp = default(Component2);
                                swComp = (Component2)varComp[I];
                                if (!((swComp == null)))
                                {
                                    ModelDoc2 swCompModel;
                                    swCompModel = (ModelDoc2)swComp.GetModelDoc2();
                                    //listView1.Groups.Add(swComp.Name2, swComp.Name2);

                                    ListViewGroup grupo = new ListViewGroup(swCompModel.GetTitle());
                                    //item.Group = grupo;
                                    Estrutura_list.Groups.Add(grupo);

                                    //EstruturaOBJ = null;
                                    EstruturaOBJ = cardalcomands.GetEstrutura(swCompModel);
                                    if (EstruturaOBJ != null)
                                    {
                                        for (int i = 0; i < EstruturaOBJ.GetLength(0); i++)
                                        {
                                            if (EstruturaOBJ[i, 0] != null)
                                            {
                                                object[] arrayInterno = (object[])EstruturaOBJ[i, 0];
                                                if ((string)arrayInterno[1] != "PESO ZERO")
                                                {
                                                    var item = new ListViewItem(new string[] { (string)arrayInterno[0], (string)arrayInterno[1], (string)arrayInterno[2], (string)arrayInterno[3], (string)arrayInterno[4], (string)arrayInterno[6] },"",grupo);
                                                    var colors = ErrorList.GetLabelColors(Convert.ToInt32(arrayInterno[5]));
                                                    item.BackColor = colors.Item1;
                                                    item.ForeColor = colors.Item2;

                                                    Estrutura_list.Items.Add(item);

                                                }
                                            }
                                        }
                                        //calcPB_bt_Click(sender, e);
                                    }

                                }

                                Debug.Print("");
                            }
                        }


                    }
                    else if ((int)swModel.GetType() == (int)swDocumentTypes_e.swDocDRAWING)
                    {
                        DefaultForms(swApp);
                    }

                }

            }
        }

        #region TextChange
        private void textDenominacao_txt_TextChanged(object sender, EventArgs e)
        {
            if (swModel != null)
            {
                if (!Comercial)
                {
                    Denominacao_txt.Text = Codigo_txt.Text + revisao_txt.Text + " " + cardalcomands.validandoDenominacao(swModel, textDenominacao_txt.Text);
                }
                else
                {
                    Denominacao_txt.Text = cardalcomands.validandoDenominacao(swModel, textDenominacao_txt.Text);
                }
            }
        }
        private void Denominacao_txt_TextChanged(object sender, EventArgs e)
        {
            int err = 0;
            if (!Comercial)
            {
                if (Denominacao_txt.Text != material_txt.Text)
                {
                    err = 2;
                }
                if (Codigo_txt.Text == "" || revisao_txt.Text == "")
                {
                    err = 3;
                }
            }
            else
            {
                if (Denominacao_txt.Text != material_txt.Text)
                {
                    err = 2;
                }
                
            }
            if (Denominacao_txt.Text == "" || material_txt.Text == "") { err = 2; }

            var colors = ErrorList.GetLabelColors(err);
            Denominacao_txt.BackColor = colors.Item1;
            Denominacao_txt.ForeColor = colors.Item2;
            errors.denominacao = err;
        }

        private void Codigo_txt_TextChanged(object sender, EventArgs e)
        {
            int err=0;
            if (swModel != null)
            {
                cardalcomands.ValidandoCodigo(swModel, out err, false, Codigo_txt.Text);
                var colors = ErrorList.GetLabelColors(err);
                Codigo_txt.BackColor = colors.Item1;
                Codigo_txt.ForeColor = colors.Item2;
            }
            textDenominacao_txt_TextChanged(sender, e);
            errors.codigo = err;
        }
        private void revisao_txt_TextChanged(object sender, EventArgs e)
        {
            int err= 0;
            if (swModel != null)
            {
                cardalcomands.ValidandoRevisao(swModel, revisao_txt.Text,out err);
                string varRev = banco.ValidarRev(Codigo_txt.Text, revisao_txt.Text);
                if (varRev != revisao_txt.Text && varRev != null) { MessageBox.Show("Revisão diferente do banco de dados"); err = 3; }

                var colors = ErrorList.GetLabelColors(err);
                revisao_txt.BackColor = colors.Item1;
                revisao_txt.ForeColor = colors.Item2;
            }
            textDenominacao_txt_TextChanged(sender, e);
            errors.revisao = err;
        }
        #region PROJETISTA/DESENHISTA/APROVADOR
        private void projetista_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (projetista_cb.Text != swComands.sw_GetCustomProperty(ppr.projetista, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            projetista_cb.BackColor = colors.Item1;
            projetista_cb.ForeColor = colors.Item2;
            errors.projetista = err;
        }

        private void desenhista_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (desenhista_cb.Text != swComands.sw_GetCustomProperty(ppr.desenhista, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            desenhista_cb.BackColor = colors.Item1;
            desenhista_cb.ForeColor = colors.Item2;
            errors.desenhista = err;
        }

        private void revisor_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (revisor_cb.Text != swComands.sw_GetCustomProperty(ppr.revisor, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            revisor_cb.BackColor = colors.Item1;
            revisor_cb.ForeColor = colors.Item2;
            errors .revisor = err;
        }
        #endregion

        #region DATA PROJETISTA/DESENHISTA/APROVADOR
        private void projetistaData_txt_TextChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (projetistaData_txt.Text != swComands.sw_GetCustomProperty(ppr.projetistaData, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            projetistaData_txt.BackColor = colors.Item1;
            projetistaData_txt.ForeColor = colors.Item2;
        }
        private void desenhistaData_txt_TextChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (desenhistaData_txt.Text != swComands.sw_GetCustomProperty(ppr.desenhistaData, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            desenhistaData_txt.BackColor = colors.Item1;
            desenhistaData_txt.ForeColor = colors.Item2;
        }
        private void revisorData_txt_TextChanged(object sender, EventArgs e)
        {
            string strVar;
            int err = 0;
            if (swModel != null)
            {
                if (revisorData_txt.Text != swComands.sw_GetCustomProperty(ppr.revisorData, swModel, "", out strVar))
                {
                    err = 1;
                }
            }
            var colors = ErrorList.GetLabelColors(err);
            revisorData_txt.BackColor = colors.Item1;
            revisorData_txt.ForeColor = colors.Item2;
        }
        #endregion
        #endregion

        private void Estrutura_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Estrutura_list.SelectedItems.Count > 0 && EstruturaOBJ !=null)
                {
                    string[] listvalue = new string[5];
                    {
                        listvalue[0] = Estrutura_list.SelectedItems[0].SubItems[0].Text;
                        listvalue[1] = Estrutura_list.SelectedItems[0].SubItems[1].Text;
                        listvalue[2] = Estrutura_list.SelectedItems[0].SubItems[2].Text;
                        listvalue[3] = Estrutura_list.SelectedItems[0].SubItems[3].Text;
                        listvalue[4] = Estrutura_list.SelectedItems[0].SubItems[4].Text;
                    }
                    for (int i = 0; i < EstruturaOBJ.GetLength(0); i++)
                    {
                        if (EstruturaOBJ[i, 0] is Array subArray)
                        {
                            string listvaluetext;
                            string objectvaluetext;
                            objectvaluetext = (string)subArray.GetValue(0) + ";" + (string)subArray.GetValue(1) + ";" + (string)subArray.GetValue(2) + ";" + (string)subArray.GetValue(3) + ";" + (string)subArray.GetValue(4);
                            listvaluetext = listvalue[0] + ";" + listvalue[1] + ";" + listvalue[2] + ";" + listvalue[3] + ";" + listvalue[4];

                            if (objectvaluetext == listvaluetext)
                            {
                                swComands.SW_SelectOBJ(swModel, EstruturaOBJ[i, 1]);
                            }

                        }
                    }

                }
                else
                {
                    swModel.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while registering the addin: " + ex.Message);
            }

        }
        private void Estrutura_list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // a coluna é diferente da última?
            if (e.Column != sortColumn)
            {
                // seta ela como atual
                sortColumn = e.Column;
                // ascendente é default
                Estrutura_list.Sorting = SortOrder.Ascending;
            }
            else
            {
                // é mesma, vamos inverter a asc/desc
                if (Estrutura_list.Sorting == SortOrder.Ascending)
                    Estrutura_list.Sorting = SortOrder.Descending;
                else
                    Estrutura_list.Sorting = SortOrder.Ascending;

            }
            // chama o método de sort
            Estrutura_list.Sort();
            // associao o método de ordenação
            Estrutura_list.ListViewItemSorter = new ListViewItemComparer(e.Column, Estrutura_list.Sorting);
        }
        private void Comercial_check_CheckedChanged(object sender, EventArgs e)
        {
            EstadoComercial(Comercial_check.Checked);
            ObterValores();
        }

        private void ApplyProjetista_bt_Click(object sender, EventArgs e)
        {
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            projetista_cb.Text = Settings.Default.User_Projetista;
            projetistaData_txt.Text = Date;
        }

        private void ApplyDesenhista_bt_Click(object sender, EventArgs e)
        {
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            desenhista_cb.Text = Settings.Default.User_Projetista;
            desenhistaData_txt.Text = Date;
        }

        private void ApplyAprovador_bt_Click(object sender, EventArgs e)
        {
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            revisor_cb.Text = "cleber.marangoni";
            revisorData_txt.Text = Date;
        }

        private void salvar_bt_Click(object sender, EventArgs e)
        {
            if(swModel != null)
            {
                CustomPropertyManager swCustProp;
                string[] confg = swComands.sw_GetConfigurations(swModel);
                Array.Resize(ref confg, confg.Length + 1);
                confg[confg.Length - 1] = "";
                if (errors.validaErros(1))
                {
                    for (int i = 0; i < confg.Length; i++)
                    {
                        swCustProp = swModel.Extension.CustomPropertyManager[confg[i]];
                        #region DELETAR PROPRIEDADES
                        swComands.sw_DeleteProperty(swCustProp, ppr.codigo);
                        swComands.sw_DeleteProperty(swCustProp, ppr.revisao);
                        swComands.sw_DeleteProperty(swCustProp, ppr.denominacao);
                        swComands.sw_DeleteProperty(swCustProp, ppr.descricao);
                        swComands.sw_DeleteProperty(swCustProp, ppr.comprimento);
                        swComands.sw_DeleteProperty(swCustProp, ppr.material);
                        swComands.sw_DeleteProperty(swCustProp, ppr.projetista);
                        swComands.sw_DeleteProperty(swCustProp, ppr.projetistaData);
                        swComands.sw_DeleteProperty(swCustProp, ppr.desenhista);
                        swComands.sw_DeleteProperty(swCustProp, ppr.desenhistaData);
                        swComands.sw_DeleteProperty(swCustProp, ppr.revisor);
                        swComands.sw_DeleteProperty(swCustProp, ppr.revisorData);
                        swComands.sw_DeleteProperty(swCustProp, ppr.pbruto);
                        swComands.sw_DeleteProperty(swCustProp, "Soldagem");
                        #endregion

                        #region ADICIONAR PROPRIEDADES
                        if (Comercial == true)
                        {
                            swComands.SW_AddProperty(swCustProp, "Soldagem", "Sim");
                        }
                        else
                        {
                            swComands.SW_AddProperty(swCustProp, "Soldagem", "Não");
                        }

                        swComands.SW_AddProperty(swCustProp, ppr.codigo, Codigo_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.revisao, revisao_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.denominacao, textDenominacao_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.descricao, textDenominacao_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.comprimento, Codigo_txt.Text+ revisao_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.material, Denominacao_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.projetista, projetista_cb.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.projetistaData, projetistaData_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.desenhista, desenhista_cb.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.desenhistaData, desenhistaData_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.revisor, revisor_cb.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.revisorData, revisorData_txt.Text.Trim());
                        swComands.SW_AddProperty(swCustProp, ppr.pbruto, pesobt_txt.Text.Trim());
                        #endregion
                    }
                }
                Atualizar_bt_Click(sender, e);
            }
        }

        private void CRM_Control1_Click(object sender, EventArgs e)
        {
            if(swModel != null)
            {
                if (Comercial == false)
                {
                    dimenssoes dimenssao;
                    object form;
                    if (FormUtils.IsFormOpen(typeof(dimenssoes), out form))
                    {
                        dimenssao = form as dimenssoes;
                        dimenssao.TopLevel = true;
                        dimenssao.Visible = true;
                        dimenssao.Focus();
                        return;
                    }
                    else
                    {
                        dimenssao = new dimenssoes(swApp, swModel, this);
                        dimenssao.Show(); return;
                        // Faça algo se o formulário não estiver aberto
                    }
                }
            }

        }
        #region MENU ITEM
        private void bancoDeMateriaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object form;
            materiaisData material;
            if (FormUtils.IsFormOpen(typeof(materiaisData), out form))
            {
                material = (materiaisData)form;
                material.TopLevel = true;
                material.Visible = true;
                material.Focus();
                return;
            }
            else
            {
                material = new materiaisData();
                material.Show(); return;
                // Faça algo se o formulário não estiver aberto
            }
        }

        #endregion

        private void calcPB_bt_Click(object sender, EventArgs e)
        {
            if (Estrutura_list.Items.Count > 0)
            {
                double pbList=0;
                for (int i = 0;Estrutura_list.Items.Count > i; i++)
                {
                    if (Estrutura_list.Items[i].SubItems[4].Text != string.Empty && Estrutura_list.Items[i].SubItems[0].Text != string.Empty)
                    {
                        string var = @"^\d+(\,\d+)?$";
                        string pb = Estrutura_list.Items[i].SubItems[4].Text.Replace(".",",");
                        string qtd = Estrutura_list.Items[i].SubItems[0].Text.Replace(".", ",");
                        if (Regex.IsMatch(pb, var) && Regex.IsMatch(qtd, var))
                        {
                            pbList = pbList + (double.Parse(pb) * double.Parse(qtd));
                        }
                       
                    }
                }
                
                pesobt_txt.Text = pbList.ToString("0.00").Replace(",",".");
            }
        }

        private void pesobt_txt_TextChanged(object sender, EventArgs e)
        {
            if(swModel!=null)
            {
                int err = 0;
                if (!Comercial)
                {
                    string varOut;
                    string PBAtual = swComands.sw_GetCustomProperty("Peso Bruto", swModel, "", out varOut);
                    if (pesobt_txt.Text != PBAtual)
                    {
                        err = 2;
                    }
                    if (pesobt_txt.Text == "")
                    {
                        err = 3;
                    }
                }

                var colors = ErrorList.GetLabelColors(err);
                pesobt_txt.BackColor = colors.Item1;
                pesobt_txt.ForeColor = colors.Item2;
                errors.pesobruto = err;
            }
        }

        private void abrirVortexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vortexcomands.OpenVortex() && vortexcomands.is_loged())
            {
                PanePrincipal.Visible = true;
                vortexcomands.showVortex();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Estrutura_main estr = new Estrutura_main(banco, this);
            estr.Show();
            estr.GetValues(Codigo_txt.Text + revisao_txt.Text);
        }
    }
}

public static class FormUtils
{
    // Verifica se uma instância específica de um formulário está aberta
    public static bool IsFormOpen(Type formType, out object formVar)
    {
        formVar = null; 
        // Verifica todas as janelas abertas na aplicação
        foreach (Form form in Application.OpenForms)
        {
            // Se uma das janelas abertas for do tipo procurado, retorna verdadeiro
            if (form.GetType() == formType)
            {
                formVar = (dimenssoes)form;
                return true;
            }
        }
        // Se nenhum formulário do tipo procurado estiver aberto, retorna falso
        return false;
    }
}
