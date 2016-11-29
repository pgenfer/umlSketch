namespace UmlSketch.Command
{
    public interface IShowOrHideCommand
    {
        /// <summary>
        /// changes the visibility of an object
        /// and returns the new visible state of the object.
        /// </summary>
        /// <returns></returns>
        bool ChangeVisibility();
        /// <summary>
        /// returns the current visibility state of the object
        /// </summary>
        bool IsVisible { get; }
    }
}
