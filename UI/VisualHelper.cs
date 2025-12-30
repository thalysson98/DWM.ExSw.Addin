using DWM.ExSw.Addin.setup.info;
using DWM.ExSw.Addin.Validation;
using System.Drawing;
using System.Windows.Forms;

public static class VisualHelper
{
    public static void Aplicar(Control ctrl, ErroCampo erro)
    {
        var colors = ErrorList.GetLabelColors((int)erro);
        ctrl.BackColor = colors.Item1;
        ctrl.ForeColor = colors.Item2;
    }
}
