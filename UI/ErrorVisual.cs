using System.Drawing;
using DWM.ExSw.Addin.Validation.Codigo;

namespace DWM.ExSw.Addin.UI
{
    public static class ErrorVisual
    {
        public static (Color back, Color fore) GetColors(CodigoErrorType error)
        {
            switch (error)
            {
                case CodigoErrorType.Warning:
                    return (Color.LightYellow, Color.Black);

                case CodigoErrorType.Changed:
                    return (Color.Orange, Color.Black);

                case CodigoErrorType.Invalid:
                    return (Color.IndianRed, Color.DarkRed);

                default:
                    return (Color.White, Color.Black);
            }
        }
    
    }
}
