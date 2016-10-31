namespace Yuml.Command
{
    /// <summary>
    /// command that is called when the color
    /// of a domain object is changed (classifier or note)
    /// </summary>
    public interface IChangeColorCommand
    {
        void ChangeColor(string newColor);
    }
}