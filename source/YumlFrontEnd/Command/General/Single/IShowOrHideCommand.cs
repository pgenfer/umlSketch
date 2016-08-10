namespace Yuml.Command
{
    /// <summary>
    /// generic command used to show or hide a domain
    /// object in the resulting diagram
    /// </summary>
    public interface IShowOrHideCommand
    {
        void Show();
        void Hide();
    }
}