using SolidWorks.Interop.sldworks;
using System.Text.RegularExpressions;

namespace DWM.ExSw.Addin.Validation.Codigo
{
    public static class CodigoValidator
    {
        public static CodigoValidationResult Validate(
            ModelDoc2 model,
            string text)
        {
            var result = new CodigoValidationResult
            {
                Error = CodigoErrorType.None,
                CodigoFinal = text
            };

            string nomeArquivo = model.GetTitle();

            string PadraoCodigo = @"^\d{3}\.\d{3}\.\d{4}$";
            string PadraoFicticio = @"^F\d{2}\.\d{3}\.\d{4}$";
            string PadraoComercial = @"^\d{6}\$";

            // Campo vazio
            if (string.IsNullOrWhiteSpace(text))
            {
                result.Error = CodigoErrorType.Warning;
                result.CodigoFinal = nomeArquivo;
                result.Message = "Código vazio, usando nome do arquivo.";
                return result;
            }

            // Diferente do nome do arquivo
            if (text != nomeArquivo)
            {
                result.Error = CodigoErrorType.Invalid;
                result.Message = "Código diferente do nome do arquivo.";
                return result;
            }

            // Padrão
            if (!Regex.IsMatch(text, PadraoCodigo))
            {
                if (Regex.IsMatch(text, PadraoFicticio) ||
                    Regex.IsMatch(text, PadraoComercial))
                {
                    result.Error = CodigoErrorType.None;
                }
                else
                {
                    result.Error = CodigoErrorType.Changed;
                    result.Message = "Código fora do padrão.";
                }
            }

            return result;
        }
    }
}
