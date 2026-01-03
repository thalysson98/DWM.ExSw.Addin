using DWM.ExSw.Addin.DataSRV;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ListViewItem = System.Windows.Forms.ListViewItem;
using Xarial.XCad;
using Xarial.XCad.Documents;
using System.Linq;
namespace DWM.ExSw.Addin.Base
{

    public class ClientComandsCardall
    {

        #region Variaveis
        swSpecialComands swComand = new swSpecialComands();
        stringcorrection stringmaneger = new stringcorrection();
        cardallData banco;
        List<string[]> XML_MATERIAIS;
        string[] config;
        
        #endregion
        public void loadData(cardallData _data, List<string[]> _dataXML)
        {
            banco = _data;
            XML_MATERIAIS = _dataXML;
        }
        public bool ComercialEstado(ModelDoc2 model)
        {
            string prpComercial;
            string varOut;
            bool Estado = false;

            prpComercial = swComand.sw_GetCustomProperty("Soldagem", model, "", out varOut);
            if (prpComercial == "Sim")
            {
                Estado = true;
            }

            return Estado;
        }

        #region Codigo
        public string ValidandoCodigo(ModelDoc2 model, out int err, bool comercial, string TextString)
        {
            err = 0;
            string codigo = GetCodigo(model);
            string PadraoCodigo = @"^\d{3}\.\d{3}\.\d{4}$";
            string PadraoFicticio = @"^F\d{2}\.\d{3}\.\d{4}$";
            string Padraocomercial = @"^\d{6}\$";

            if (codigo != "")//Existe a propriedade codigo
            {
                if (TextString != codigo)//Texto diferente da propriedade
                {
                    if (TextString != "")
                    {
                        codigo = TextString;
                        err = 2;
                    }

                }
            }
            else if (TextString == "")//Arquivo novo
            {
                TextString = model.GetTitle(); ;
                err = 1;
                codigo = TextString;
            }


            if (TextString != model.GetTitle())//Texto escrito diferente do nome do arquivo
            {
                err = 3;
                //IMPOSSIVEL EXPORTAR E SALVAR
            }
            else if (!Regex.IsMatch(TextString, PadraoCodigo))//texto codigo padrao
            {
                if (Regex.IsMatch(TextString, PadraoFicticio))//texto ficticio
                {
                    err = 0;
                }
                else if (Regex.IsMatch(TextString, Padraocomercial))//texto comercial
                {
                    err = 0;
                }
                else
                {
                    err = 2;
                }

            }

            return codigo;
        }
        #endregion

        #region Revisão
        public void ValidandoRevisao(ModelDoc2 model, string textrevisao, out int err)
        {
            err = 0;
            string revisao = GetRevisao(model);
            if (revisao == "")
            {
                err = 1;
            }
            else if (textrevisao != revisao)
            {
                err = 2;
            }
        }

        #endregion


        #region Responsaveis
        public int ValidarResponsavel(
                string texto,
                string valorPropriedade)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 1;

            // nome.sobrenome
            string padrao = @"^[a-zA-Z]+\.[a-zA-Z]+$";

            if (!Regex.IsMatch(texto, padrao))
                return 2;

            if (texto != valorPropriedade)
                return 1;

            return 0;
        }
        public int ValidarData(
                string texto,
                string valorPropriedade)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 1;

            string padrao = @"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$";

            if (!Regex.IsMatch(texto, padrao))
                return 2;

            if (texto != valorPropriedade)
                return 1;

            return 0;
        }

        #endregion

        #region Obter Prorpiedades
        public string GetCodigo(ModelDoc2 model)
        {
            string codigo = "";
            string var;
            codigo = swComand.sw_GetCustomProperty("NumeroDesenho", model, "", out var);
            return codigo;
        }
        public string GetDenominacao(ModelDoc2 model)
        {
            string denomincao = "";
            string var;
            denomincao = swComand.sw_GetCustomProperty("Material", model, "", out var);
            return denomincao;
        }
        public string GetRevisao(ModelDoc2 model)
        {
            string rev = "";
            string var;
            rev = swComand.sw_GetCustomProperty("Revisão", model, "", out var);
            return rev;
        }
        #endregion

        #region Estrutura Lista de Corte
        public object[,] GetEstrutura(ModelDoc2 model)
        {
            object vCutLists = swComand.GetCutLists(model);

            if (vCutLists != null && vCutLists is Array cutLists && cutLists.Length > 0)
            {
                object[,] selObj = new object[cutLists.Length, 2];
                int i = 0;

                foreach (Feature swCutListFeat in cutLists)
                {
                    CustomPropertyManager custPrpMgr;
                    BodyFolder swBodyFolder;
                    string prpVal;
                    string[] Val2 = new string[7];
                    object[] Bodies;
                    int err = 0;

                    custPrpMgr = swCutListFeat.CustomPropertyManager;
                    swBodyFolder = swCutListFeat.GetSpecificFeature2() as BodyFolder;
                    Bodies = swBodyFolder.GetBodies() as object[];

                    custPrpMgr.Get2("MATERIAL", out prpVal, out Val2[1]);
                    for (int j = 0; j <= (swBodyFolder.GetBodyCount() - 1); j++)
                    {
                        string un1, un2, peso;
                        bool InTable;
                        Body2 swBody;
                        swBody = (Body2)Bodies[j];
                        if (Val2[1] != "SAE 1020")
                        {
                            InTable = validarMaterial(Val2[1], out un1, out un2, out peso);
                            if (InTable)
                            {
                                err = validarCorpo(custPrpMgr, swBody.IsSheetMetal(), un1, un2, peso, out Val2[6], InTable);
                            }
                            else { err = 3; }
                        }
                        else
                        {
                            err = 3;
                            Val2[6] = "<info#002>";
                        }
                    }
                    Val2[5] = err.ToString();
                    custPrpMgr.Get2("QUANTITY", out prpVal, out Val2[0]);
                    custPrpMgr.Get2("COMPRIMENTO", out prpVal, out Val2[2]);
                    custPrpMgr.Get2("Massa", out prpVal, out Val2[3]);
                    custPrpMgr.Get2("Peso Bruto", out prpVal, out Val2[4]);

                    Val2[2] = Val2[2].Replace(",", ".");
                    Val2[3] = Val2[3].Replace(",", ".");
                    Val2[4] = Val2[4].Replace(",", ".");


                    var item1 = new ListViewItem(new string[] { Val2[0], Val2[1], Val2[2], Val2[3], Val2[4], Val2[5], Val2[6] });
                    selObj[i, 0] = Val2; // Atribuir um array de strings
                    selObj[i, 1] = swCutListFeat;

                    i++;
                }

                return selObj;
            }

            return null; // Retornar null se não houver listas de corte
        }//Obtem lista de corte
        private int validarCorpo(CustomPropertyManager custPrpMgr, bool isSheetMetal, string un1, string un2, string pesoTabela, out string info, bool inTable)
        {
            string[] pprVal1 = new string[5];
            string[] pprVal2 = new string[5];
            string varPB, varOut;
            int err = 0;
            info = "";

            swComand.sw_GetAllvaluesProperty("Peso Bruto", custPrpMgr, out varOut, out varPB);
            if (inTable)
            {
                if (isSheetMetal)
                {
                    swComand.sw_GetAllvaluesProperty("Largura da Caixa delimitadora", custPrpMgr, out pprVal1[0], out pprVal2[0]);
                    swComand.sw_GetAllvaluesProperty("Comprimento da Caixa delimitadora", custPrpMgr, out pprVal1[1], out pprVal2[1]);
                    swComand.sw_GetAllvaluesProperty("Espessura da Chapa metálica", custPrpMgr, out pprVal1[2], out pprVal2[2]);
                    swComand.sw_GetAllvaluesProperty("Tolerância da dobra", custPrpMgr, out pprVal1[3], out pprVal2[3]);
                    swComand.sw_GetAllvaluesProperty("Raio de dobra", custPrpMgr, out pprVal1[4], out pprVal2[4]);

                    if (!pprEditada(custPrpMgr))//Verifica se o valor é editado
                    {
                        if (un1 == "KG" && un2 == "M2")
                        {
                            double largura = double.Parse(pprVal2[0].Replace(".", ",")) / 1000;
                            double comprimento = double.Parse(pprVal2[1].Replace(".", ",")) / 1000;
                            double peso = double.Parse(pesoTabela.Replace(".", ","));
                            double pb = largura * comprimento * peso;

                            swComand.sw_DeleteProperty(custPrpMgr, "Peso Bruto");
                            swComand.SW_AddProperty(custPrpMgr, "Peso Bruto", pb.ToString("0.00"));

                            info = $"#{pprVal2[2]} K{pprVal2[3]} R{pprVal2[4]}";
                            if (varPB != pb.ToString("0.00")) { err = 1; }//informa qual item foi mudado o peso bruto
                            if (largura > 1.2 || comprimento > 6) { err = 1; info = "<info#001>"; }//medida maior que o padrão
                            else if (largura > 6 || comprimento > 1.2) { err = 1; info = "<info#001>"; }//medida maior que o padrão

                            swComand.sw_DeleteProperty(custPrpMgr, "COMPRIMENTO");
                            swComand.SW_AddProperty(custPrpMgr, "COMPRIMENTO", pprVal1[0] + " X " + pprVal1[1]);
                        }
                        else if (un1 == "M" || un2 == "M")
                        {
                            info = "<ERRO#104>";
                            err = 3;
                        }
                        else
                        {
                            if (!pprEditada(custPrpMgr))
                            {
                                swComand.sw_DeleteProperty(custPrpMgr, "COMPRIMENTO");
                                swComand.sw_DeleteProperty(custPrpMgr, "Peso Bruto");
                                swComand.SW_AddProperty(custPrpMgr, "COMPRIMENTO", pprVal1[0] + " X " + pprVal1[1]);
                                swComand.SW_AddProperty(custPrpMgr, "Peso Bruto", "0.00");
                            }
                            else { err = 1; info = "<info#003>"; }


                        }


                    }
                    else
                    {
                        swComand.sw_GetAllvaluesProperty("COMPRIMENTO", custPrpMgr, out pprVal1[0], out pprVal2[0]);
                        string M2 = @"^\d+(\.\d+)? X \d+(\.\d+)?$";
                        string M = @"^\d+(\.\d+)?$";
                        if (un1 == "M" || un2 == "M")
                        {
                            if (!Regex.IsMatch(pprVal2[0], M))
                            {
                                swComand.SW_AddProperty(custPrpMgr, "COMPRIMENTO", "0 X 0");
                                info = "<ERRO#104>";
                                return err = 3;
                            }
                        }
                        else if (un1 == "M2" || un2 == "M2")
                        {
                            if (!Regex.IsMatch(pprVal2[0], M2))
                            {
                                swComand.SW_AddProperty(custPrpMgr, "COMPRIMENTO", "0 X 0");
                                info = "<ERRO#104>";
                                return err = 3;
                            }
                        }
                        err = 1;
                        info = "<info#003>";
                    }
                }
                else
                {
                    if (un1 == "KG" && un2 == "M")
                    {
                        swComand.sw_GetAllvaluesProperty("COMPRIMENTO", custPrpMgr, out pprVal1[0], out pprVal2[0]);
                        swComand.sw_GetAllvaluesProperty("Peso Bruto", custPrpMgr, out varOut, out varPB);
                        if (pprVal1[0] != "")
                        {
                            double comprimento = double.Parse(pprVal2[0].Replace(".", ",")) / 1000;
                            double peso = double.Parse(pesoTabela.Replace(".", ","));
                            double pb = comprimento * peso;
                            swComand.sw_DeleteProperty(custPrpMgr, "Peso Bruto");
                            swComand.SW_AddProperty(custPrpMgr, "Peso Bruto", pb.ToString("0.00"));
                            if (varPB != pb.ToString("0.00")) { err = 1; }
                            if (comprimento > 6) { err = 1; info = "<info#002>"; }
                            if (pesoTabela == "") { err = 3; info = "<ERRO#103>"; }
                        }
                    }
                    else if(un1 == "PC") { err = 0; }
                    else
                    {
                        swComand.sw_GetAllvaluesProperty("Denominação", custPrpMgr, out pprVal1[0], out pprVal2[0]);
                        if (pprVal2[0] == "")
                        {
                            err = 3;
                            info = "<ERRO#101>";
                        }
                    }
                }
            }
            else
            {
                swComand.sw_GetAllvaluesProperty("Denominação", custPrpMgr, out pprVal1[0], out pprVal2[0]);
                if (pprVal2[0] == "")//verifica se é multi corpo
                {
                    //significa que não é multi corpo e não está no XML
                    err = 3;
                    info = "<ERRO#102>";
                }
            }
            custPrpMgr.LinkProperty("Massa", true);
            return err;
        }
        private bool pprEditada(CustomPropertyManager custPrpMgr)
        {
            string edit, outedit;
            swComand.sw_GetAllvaluesProperty("EDITADO", custPrpMgr, out outedit, out edit);
            if (edit.ToUpper() == "SIM") { return true; }

            return false;
        }
        private bool validarMaterial(string pprMaterial, out string UN1, out string UN2, out string peso)
        {
            UN1 = string.Empty;
            UN2 = string.Empty;
            peso = string.Empty;

            if (pprMaterial != string.Empty)
            {
                string material;
                if(ValTipoCod(pprMaterial, out material) == 0)
                {
                    
                    string cod = pprMaterial.Split(' ')[0];
                    string desc = string.Join(" ", pprMaterial.Split(' ').Skip(1));

                    string varCod = banco.GetCodigoDesc(cod);
                    string varDesc = string.Join(" ", varCod.Split(' ').Skip(1));
                    varCod = varCod.Split(' ')[0];
                    

                    if (cod == varCod)
                    {
                        if(varDesc == desc) { return true; }
                        return false;
                    }

                }
                else if (ValTipoCod(pprMaterial, out material) == 1)
                {
                    string[] var = XML_MATERIAIS.Find(m => m[0].StartsWith(material));

                    if (var != null)
                    {
                        for (int i = 0; i < banco.comercial_DATA.Count; i++)
                        {
                            if (material == banco.comercial_DATA[i])
                            {
                                bool intable = false;
                                if (var[3] == banco.um_DATA[i]) { intable = true; }
                                if (var[4] == banco.um_DATA[i]) { intable = true; }
                                if (intable)
                                {
                                    UN1 += var[3];
                                    UN2 += var[4];
                                    peso += var[5];
                                    return true;
                                }
                                else
                                {
                                    if (var[3] == "" && var[4] == "")
                                    {
                                        UN1 += banco.um_DATA[i];
                                        return true;
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Codigo: {pprMaterial} não está com a unidade de medida(m² ou m linear) igual a do banco de dados", "CADASTRO INVÁLIDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < banco.comercial_DATA.Count; i++)
                        {
                            if (material == banco.comercial_DATA[i])
                            {
                                UN1 += banco.um_DATA?[i];
                                UN2 += banco.um_DATA?[i];
                                peso += "0";
                                return true;

                            }
                        }

                    }
                }

            }
            return false;

        }
        public int ValTipoCod( string TextString, out string OutString)
        {
            string PadraoCodigo = @"^\d{3}\.\d{3}\.\d{4}$";
            string PadraoFicticio = @"^F\d{2}\.\d{3}\.\d{4}$";
            string Padraocomercial = @"^\d{6}$";
            OutString = "";

            for (int i =0; i < 13; i++)
            {
                OutString = TextString.Substring(0, i);
                if (Regex.IsMatch(OutString, PadraoCodigo))//texto codigo padrao
                {
                    return 0;
                }
                else if (Regex.IsMatch(OutString, PadraoFicticio))//texto ficticio
                {
                    return 0;
                }
                else if (Regex.IsMatch(OutString, Padraocomercial))//texto comercial
                {
                    return 1;
                }

            }
            return 3;
        }

        #endregion


    }
}
