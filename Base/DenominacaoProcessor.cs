using SolidWorks.Interop.sldworks;

public static class DenominacaoProcessor
{
    public static string Processar(ModelDoc2 model, string texto)
    {
        if (model == null || string.IsNullOrWhiteSpace(texto))
            return "";

        string stDenominacao, stSubstituido, stDimensao, stChar;
        int i, c;

        texto = texto.Replace("=", ":").Replace("&", "");
        stDenominacao = texto.Replace("\r\n", "");

        stSubstituido = "";
        stDimensao = "";
        c = 0;

        for (i = 0; i < stDenominacao.Length; i++)
        {
            stChar = stDenominacao.Substring(i, 1);

            if (stChar != "\"" && c == 0)
            {
                stSubstituido += stChar;
            }
            else if (stChar == "\"" && c == 0)
            {
                c = 1;
            }
            else if (stChar != "\"" && c == 1)
            {
                stDimensao += stChar;
            }
            else if (stChar == "\"" && c == 1)
            {
                c = 0;
                stSubstituido += RetornaDimensao(stDimensao, model);
                stDimensao = "";
            }

            if (i == stDenominacao.Length - 1 && c == 1)
            {
                stSubstituido += "''" + stDimensao;
                stDimensao = "";
                c = 0;
            }
        }

        return stSubstituido;
    }

    private static string RetornaDimensao(string stNomeDimensao, ModelDoc2 model)
    {
        Dimension swDimension = model.Parameter(stNomeDimensao) as Dimension;

        if (swDimension == null)
            return "'" + stNomeDimensao + "'";

        double valor = swDimension.GetValue2("");
        string texto = valor.ToString("0.00");

        if (texto.EndsWith("00"))
            texto = texto.Substring(0, texto.Length - 3);
        else if (texto.EndsWith("0"))
            texto = texto.Substring(0, texto.Length - 1);

        string filtrado = "";
        foreach (char c in texto)
        {
            if (char.IsDigit(c) || c == ',' || c == '.')
                filtrado += c;
        }

        return filtrado;
    }

    #region Denominação
    //public static string validandoDenominacao(ModelDoc2 model, string denomincao_textbox)
    //{
    //    string stDenominacao, stSubstituido, stDimensao, stChar;
    //    int i, c;
    //    denomincao_textbox = denomincao_textbox.Replace("=", ":").Replace("&", "");
    //    stDenominacao = denomincao_textbox.Replace("\r\n", "");

    //    // Retorna a descrição da peça com os nomes de dimensões substituídas pelos seus valores.
    //    stSubstituido = "";
    //    stDimensao = "";
    //    c = 0;
    //    for (i = 0; i < stDenominacao.Length; i++)
    //    {
    //        stChar = stDenominacao.Substring(i, 1);
    //        if (stChar != "\"" && c == 0)
    //        {
    //            stSubstituido += stChar;
    //        }
    //        else if (stChar == "\"" && c == 0)
    //        {
    //            c = 1;
    //        }
    //        else if (stChar != "\"" && c == 1)
    //        {
    //            stDimensao += stChar;
    //        }
    //        else if (stChar == "\"" && c == 1)
    //        {
    //            c = 0;
    //            stSubstituido += RetornaDimensao(stDimensao, model);
    //            stDimensao = "";
    //        }
    //        if (i == stDenominacao.Length - 1 && c == 1)
    //        {
    //            stSubstituido += "''" + stDimensao;
    //            stDimensao = "";
    //            c = 0;
    //        }
    //    }
    //    return stSubstituido;
    //}
    //public string RetornaDimensao(string stNomeDimensao, ModelDoc2 model)
    //{
    //    Dimension swDimension = null;
    //    swDimension = (Dimension)model.Parameter(stNomeDimensao);

    //    if (swDimension != null)
    //    {
    //        double valor = swDimension.GetValue2("");
    //        // Formata o texto da dimensão
    //        stNomeDimensao = (valor).ToString("0.00");

    //        if (stNomeDimensao.EndsWith("00"))
    //        {
    //            stNomeDimensao = stNomeDimensao.Substring(0, stNomeDimensao.Length - 3);
    //        }
    //        else if (stNomeDimensao.EndsWith("0"))
    //        {
    //            stNomeDimensao = stNomeDimensao.Substring(0, stNomeDimensao.Length - 1);
    //        }

    //        // Remove qualquer caracter que não seja ponto, vírgula ou número
    //        var stOriginal = stNomeDimensao;
    //        stNomeDimensao = "";
    //        foreach (char c in stOriginal)
    //        {
    //            if (char.IsDigit(c) || c == ',' || c == '.')
    //            {
    //                stNomeDimensao += c;
    //            }
    //        }

    //        // Retorna o nome da dimensão
    //        return stNomeDimensao;
    //    }
    //    else
    //    {
    //        return "'" + stNomeDimensao + "'";
    //    }
    //}
    public static int ValidandoDenominacao(
       bool comercial,
       string denominacaoGerada,
       string material,
       string codigo,
       string revisao)
    {
        int err = 0;

        if (!comercial)
        {
            if (string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(revisao))
            {
                return 3;
            }
        }

        if (string.IsNullOrWhiteSpace(denominacaoGerada) ||
            string.IsNullOrWhiteSpace(material))
        {
            return 2;
        }

        if (denominacaoGerada != material)
        {
            return 2;
        }

        return err;
    }
    public static string GerarDenominacao(
            bool comercial,
            string codigo,
            string revisao,
            string denominacaoBase)
    {
        if (comercial)
            return denominacaoBase;

        return $"{codigo}{revisao} {denominacaoBase}";
    }
    #endregion

}
