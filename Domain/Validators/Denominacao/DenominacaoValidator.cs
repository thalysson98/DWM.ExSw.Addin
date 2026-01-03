namespace DWM.ExSw.Addin.Validation.Denominacao
{
    public static class DenominacaoValidator
    {
        public static DenominacaoValidationResult Validate(
            bool comercial,
            string denominacaoGerada,
            string material,
            string codigo,
            string revisao)
        {
            var result = new DenominacaoValidationResult
            {
                DenominacaoFinal = denominacaoGerada,
                Error = DenominacaoErrorType.None
            };

            // Regra 1: não comercial exige código e revisão
            if (!comercial)
            {
                if (string.IsNullOrWhiteSpace(codigo) ||
                    string.IsNullOrWhiteSpace(revisao))
                {
                    result.Error = DenominacaoErrorType.Invalid;
                    result.Message = "Código e revisão são obrigatórios.";
                    return result;
                }
            }

            // Regra 2: campos obrigatórios
            if (string.IsNullOrWhiteSpace(denominacaoGerada) ||
                string.IsNullOrWhiteSpace(material))
            {
                result.Error = DenominacaoErrorType.Changed;
                result.Message = "Denominação ou material vazio.";
                return result;
            }

            // Regra 3: denominação deve bater com material
            if (denominacaoGerada != material)
            {
                result.Error = DenominacaoErrorType.Changed;
                result.Message = "Denominação diferente do material.";
                return result;
            }

            return result;
        }
    }
}
