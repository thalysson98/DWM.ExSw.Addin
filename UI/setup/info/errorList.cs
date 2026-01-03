using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DWM.ExSw.Addin.setup.info
{
    public class ErrorList
    {
        public int codigo { get; set; }
        public int revisao { get; set; }
        public int denominacao { get; set; }
        public int projetista { get; set; }
        public int desenhista { get; set; }
        public int revisor { get; set; }
        public int pesobruto { get; set; }

        public static Tuple<Color,Color> GetLabelColors(int caseNumber)
        {
            switch (caseNumber)
            {
                case 1:
                    //Propriedade ausente, foi aplicado um valor novo generico, POSSIVEL SALVAR
                    return Tuple.Create(Color.LightYellow, Color.Black); 
                case 2:
                    //Propriedade substituida, POSSIVEL SALVAR
                    return Tuple.Create(Color.Orange, Color.Black); 
                case 3:
                    //Valor inválido, IMPOSSIVEL SALVAR
                    return Tuple.Create(Color.IndianRed, Color.DarkRed);
                default:
                    //ITEM VALIDO
                    return Tuple.Create((Color.White), Color.Black);
                    
            }
        }
        public bool validaErros(int cenario)
        {
            switch (cenario)
            {
                case 1:
                    //APLICAR PROPRIEDADE
                    if (codigo == 3) { return false; }
                    if (revisao == 3) { return false; }
                    if (denominacao == 3) { return false; }
                    if (projetista == 3) { return false; }
                    if (desenhista == 3) { return false; }
                    if (revisor == 3) { return false; }

                    return true;
                case 2:
                    //EXPORTAR
                    if (codigo != 0) { return false; }
                    if (revisao != 0) { return false; }
                    if (denominacao != 0) { return false; }
                    if (projetista != 0) { return false; }
                    if (desenhista != 0) { return false; }
                    if (revisor != 0) { return false; }

                    return true;
                default:
                    return false;
            }
        }
        public void descricaoErros()
        {
            #region INFORMAÇÕES
            //INFO 01: medida acima do padrão comercial
            //INFO 03: VALOR EDITADO MANUALMENTE
            #endregion

            #region ERROS
            //ERRO 101: ITEM NÃO ESTÁ NA BIBLIOTECA DE MATERIAIS
            //ERRO 104: PADRÃO EM M2 POREM ESTÁ COM M LINEAR
            //ERRO 103: PESO TABELA ZERADO, VERIFICAR ARQUIVO XML
            //ERRO 102: ITEM NÃO ESTÁ NA BIBLIOTECA DE MATERIAIS



            //'<erro#101>: Numero do desenho da descrição e da medida de corte estão diferentes.
            //'<erro#102>: O desenho não chama revisão.
            //'<erro#103>: A lista de materiais está chamando o próprio desenho."
            //'<erro#104>: Está chamando algum material sem código.
            //'<erro#105>: Está sem largura/comprimento.
            //'<erro#105>: Está sem largura/comprimento.
            //'<erro#106>: A quantidade está zerada.
            //'<erro#107>: Está chamando algum material sem cadastro.
            //'<erro#108>: <erro#108>: Largura e comprimento está 1,0 X 1,0m.
            //'<erro#109>: Unidade do material é 'M' porem está com largura e comprimento.
            //'<erro#110>: Unidade do material é 'M2' porem está sem comprimento.
            //'<erro#111>: Unidade do material é 'PC' porem está com comprimento e/ou largura.
            //010273 - KG

            #endregion
        }

    }
}
