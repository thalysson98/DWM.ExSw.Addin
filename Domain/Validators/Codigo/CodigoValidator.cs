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

            string nomeArquivo = model?.GetTitle() ?? string.Empty;

            const string PadraoCodigo = @"^\d{3}\.\d{3}\.\d{4}$";
            const string PadraoFicticio = @"^F\d{2}\.\d{3}\.\d{4}$";
            const string PadraoComercial = @"^\d{6}\$";

            // 1️⃣ Campo vazio → usa nome do arquivo
            if (string.IsNullOrWhiteSpace(text))
            {
                result.Error = CodigoErrorType.Warning;
                result.CodigoFinal = nomeArquivo;
                result.Message = "Código vazio, usando nome do arquivo.";
                return result;
            }

            // 2️⃣ Texto diferente do nome do arquivo → ERRO BLOQUEANTE
            if (!string.Equals(text, nomeArquivo))
            {
                result.Error = CodigoErrorType.Invalid;
                result.Message = "Código diferente do nome do arquivo.";
                return result;
            }

            // 3️⃣ Validação de padrão
            if (!Regex.IsMatch(text, PadraoCodigo))
            {
                bool permitido =
                    Regex.IsMatch(text, PadraoFicticio) ||
                    Regex.IsMatch(text, PadraoComercial);

                if (!permitido)
                {
                    result.Error = CodigoErrorType.Changed;
                    result.Message = "Código fora do padrão.";
                }
            }

            return result;
        }
    }
}
