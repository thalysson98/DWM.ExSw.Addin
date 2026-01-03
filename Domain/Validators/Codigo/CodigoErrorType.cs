namespace DWM.ExSw.Addin.Validation.Codigo
{
    public enum CodigoErrorType
    {
        None = 0,        // OK
        Warning = 1,     // Campo vazio / auto preenchido
        Changed = 2,     // Fora do padrão, mas permite seguir
        Invalid = 3      // Bloqueia salvar/exportar
    }
}
