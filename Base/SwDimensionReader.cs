using SolidWorks.Interop.sldworks;

public class SwDimensionReader
{
    public string ObterValor(ModelDoc2 model, string nomeDimensao)
    {
        var dim = model.Parameter(nomeDimensao) as Dimension;
        if (dim == null) return $"'{nomeDimensao}'";

        double valor = dim.GetValue2("");
        return valor.ToString("0.##");
    }
}
