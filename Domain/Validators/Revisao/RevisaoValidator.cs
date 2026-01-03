using DWM.ExSw.Addin.Base;
using SolidWorks.Interop.sldworks;
using System.Text.RegularExpressions;

namespace DWM.ExSw.Addin.Validation.Revisao
{
    public static class RevisaoValidator
    {
        public static RevisaoValidationResult Validate(
            ModelDoc2 model,
            string text)
        {
            var result = new RevisaoValidationResult
            {
                Error = RevisaoErrorType.None,
                RevisaoFinal = text
            };

            var comandos = new ClientComandsCardall();
            string revisaoModel = comandos.GetRevisao(model);
            string padraoLetra = @"^[A-Z]$";      // A, B, C

            // Propriedade não existe
            if (string.IsNullOrWhiteSpace(revisaoModel))
            {
                result.Error = RevisaoErrorType.Warning;
                result.RevisaoFinal = "A";
                result.Message = "Propriedade Revisão ausente no modelo.";
                return result;
            }
            // Letra inválida
            if (!Regex.IsMatch(text, padraoLetra))
            {
                result.Error = RevisaoErrorType.Invalid;
                return result;
            }
            // Texto diferente da propriedade
            if (text != revisaoModel)
            {
                result.Error = RevisaoErrorType.Changed;
                result.Message = "Revisão diferente da propriedade do modelo.";
                return result;
            }


            // OK
            return result;
        }
    }
    
}
