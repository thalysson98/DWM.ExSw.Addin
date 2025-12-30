namespace DWM.ExSw.Addin.Validation.Revisao
{
    public enum RevisaoErrorType
    {
        None = 0,        // OK
        Warning = 1,     // Vazia / padrão
        Changed = 2,     // Fora do padrão esperado
        Invalid = 3      // Bloqueia salvar/exportar
    }
}
