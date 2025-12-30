using SolidWorks.Interop.sldworks;

//namespace DWM.ExSw.Addin.Validation.Denominacao
//{
//    public static class DenominacaoProcessor
//    {
//        public static string Processar(
//            ModelDoc2 model,
//            string texto)
//        {
//            string stDenominacao, stSubstituido, stDimensao, stChar;
//            int i, c;

//            texto = texto.Replace("=", ":").Replace("&", "");
//            stDenominacao = texto.Replace("\r\n", "");

//            stSubstituido = "";
//            stDimensao = "";
//            c = 0;

//            for (i = 0; i < stDenominacao.Length; i++)
//            {
//                stChar = stDenominacao.Substring(i, 1);

//                if (stChar != "\"" && c == 0)
//                {
//                    stSubstituido += stChar;
//                }
//                else if (stChar == "\"" && c == 0)
//                {
//                    c = 1;
//                }
//                else if (stChar != "\"" && c == 1)
//                {
//                    stDimensao += stChar;
//                }
//                else if (stChar == "\"" && c == 1)
//                {
//                    c = 0;
//                    stSubstituido += RetornaDimensao(stDimensao, model);
//                    stDimensao = "";
//                }

//                if (i == stDenominacao.Length - 1 && c == 1)
//                {
//                    stSubstituido += "''" + stDimensao;
//                    stDimensao = "";
//                    c = 0;
//                }
//            }

//            return stSubstituido;
//        }

//        private static string RetornaDimensao(
//            string stNomeDimensao,
//            ModelDoc2 model)
//        {
//            Dimension swDimension = model.Parameter(stNomeDimensao) as Dimension;

//            if (swDimension != null)
//            {
//                double valor = swDimension.GetValue2("");
//                string texto = valor.ToString("0.00");

//                if (texto.EndsWith("00"))
//                    texto = texto.Substring(0, texto.Length - 3);
//                else if (texto.EndsWith("0"))
//                    texto = texto.Substring(0, texto.Length - 1);

//                string filtrado = "";
//                foreach (char c in texto)
//                {
//                    if (char.IsDigit(c) || c == ',' || c == '.')
//                        filtrado += c;
//                }

//                return filtrado;
//            }

//            return "'" + stNomeDimensao + "'";
//        }
//    }
//}
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
}
