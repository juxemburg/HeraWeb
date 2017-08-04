

namespace Entities.Colors
{
    public enum Color
    {
        Blue,
        Lightblue,
        Cyan,
        Teal,
        Indigo,
        Green,
        Lime,
        Yellow,
        Orange,
        Red
    }
    public static class ColorHelper
    {
        public static string Get_ColorName(Color color)
        {
            switch (color)
            {
                case Color.Blue:
                    return "blue";
                case Color.Lightblue:
                    return "lightblue";
                case Color.Cyan:
                    return "cyan";
                case Color.Teal:
                    return "tela";
                case Color.Indigo:
                    return "indigo";
                case Color.Green:
                    return "green";
                case Color.Lime:
                    return "lime";
                case Color.Yellow:
                    return "yellow";
                case Color.Orange:
                    return "orange";
                case Color.Red:
                    return "red";
                default:
                    return "blue";
            }
        }
    }
}
