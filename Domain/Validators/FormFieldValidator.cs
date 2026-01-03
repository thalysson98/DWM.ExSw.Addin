using DWM.ExSw.Addin.Base;
using SolidWorks.Interop.sldworks;
using System.Text.RegularExpressions;

namespace DWM.ExSw.Addin.Validation
{
    public enum ErroCampo
    {
        Ok = 0,              // válido
        Info = 1,            // informação / alterado
        Alterado = 2,        // diferente do original
        Invalido = 3         // erro crítico
    }

    public static class FormFieldValidator
    {
        // ----------------------------
        // NOME: nome.sobrenome
        // ----------------------------
        public static ErroCampo ValidarNome(
            ModelDoc2 model,
            string textoDigitado,
            string propriedadeSW,
            swSpecialComands swCmd)
        {
            string valorSW;
            string valorAtual = swCmd.sw_GetCustomProperty(
                propriedadeSW, model, "", out valorSW);

            if (string.IsNullOrWhiteSpace(textoDigitado))
                return ErroCampo.Invalido;

            var padrao = @"^[a-zA-Z]+\.[a-zA-Z]+$";
            if (!Regex.IsMatch(textoDigitado, padrao))
                return ErroCampo.Invalido;

            if (textoDigitado != valorAtual)
                return ErroCampo.Info;

            return ErroCampo.Ok;
        }

        // ----------------------------
        // DATA: dd-mm-aaaa
        // ----------------------------
        public static ErroCampo ValidarData(
            ModelDoc2 model,
            string textoDigitado,
            string propriedadeSW,
            swSpecialComands swCmd)
        {
            string valorSW;
            string valorAtual = swCmd.sw_GetCustomProperty(
                propriedadeSW, model, "", out valorSW);

            if (string.IsNullOrWhiteSpace(textoDigitado))
                return ErroCampo.Invalido;

            var padrao = @"^\d{2}-\d{2}-\d{4}$";
            if (!Regex.IsMatch(textoDigitado, padrao))
                return ErroCampo.Invalido;

            if (textoDigitado != valorAtual)
                return ErroCampo.Info;

            return ErroCampo.Ok;
        }
    }
}
