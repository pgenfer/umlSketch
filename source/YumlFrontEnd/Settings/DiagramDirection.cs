namespace Yuml
{

    /// <summary>
    /// available directions for the diagram
    /// </summary>
    public enum DiagramDirection
    {
        TopDown,
        LeftToRight,
        RightToLeft
    }

    public static class DiagramDirectionExtension
    {
        public static string ToDsl(this DiagramDirection direction)
        {
            switch (direction)
            {
                case DiagramDirection.LeftToRight:
                    return "LR";
                case DiagramDirection.RightToLeft:
                    return "RL";
                case DiagramDirection.TopDown:
                    return "TD";
                default:
                    return string.Empty;
            }
        }
    }
}