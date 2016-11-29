namespace Common
{
    /// <summary>
    /// interface for all objects that can be drawn on the diagram.
    /// </summary>
    public interface IVisible
    {
        /// <summary>
        /// if set to true, object will be rendered,
        /// otherwise object is not visible on the diagram
        /// </summary>
        bool IsVisible { get; set; }
    }
}