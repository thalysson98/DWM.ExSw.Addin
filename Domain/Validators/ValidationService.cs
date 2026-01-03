//using DWM.ExSw.Addin.Base.Validation;
//using DWM.ExSw.Addin.Base.Validation.Codigo;
//using DWM.ExSw.Addin.Base.Validation.Denominacao;
//using DWM.ExSw.Addin.DTO;
//using DWM.ExSw.Addin.Validation.Codigo;
//using DWM.ExSw.Addin.Validation.Denominacao;

//namespace DWM.ExSw.Addin.Services
//{
//    public class ValidationService
//    {
//        private readonly CodigoValidator _codigoValidator;
//        private readonly DenominacaoValidator _denominacaoValidator;

//        public ValidationService()
//        {
//            _codigoValidator = new CodigoValidator();
//            _denominacaoValidator = new DenominacaoValidator();
//        }

//        public ValidationResult ValidateForm(CardallFormData formData)
//        {
//            var codigoResult = _codigoValidator.Validate(formData.Codigo);
//            if (!codigoResult.IsValid)
//                return codigoResult;

//            var denominacaoResult = _denominacaoValidator.Validate(formData.Denominacao);
//            if (!denominacaoResult.IsValid)
//                return denominacaoResult;

//            return ValidationResult.Success();
//        }
//    }
//}
