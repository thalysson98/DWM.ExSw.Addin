using SolidWorks.Interop.sldworks;
using DWM.ExSw.Addin.Base;

namespace DWM.ExSw.Addin.Validation.Denominacao
{
    public static class DenominacaoValidator
    {
        public static DenominacaoValidationResult Validate(
            ModelDoc2 model,
            string textoProcessado)
        {
            var result = new DenominacaoValidationResult
            {
                Error = DenominacaoErrorType.None,
                DenominacaoFinal = textoProcessado
            };

            var comandos = new ClientComandsCardall();
            string denominacaoModel = comandos.GetDenominacao(model);

            if (string.IsNullOrWhiteSpace(denominacaoModel))
            {
                result.Error = DenominacaoErrorType.Warning;
                result.Message = "Propriedade Denominação ausente no modelo.";
                return result;
            }

            if (textoProcessado != denominacaoModel)
            {
                result.Error = DenominacaoErrorType.Changed;
                result.Message = "Denominação diferente da propriedade do modelo.";
                return result;
            }

            return result;
        }
    }
}
