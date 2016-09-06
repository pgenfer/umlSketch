namespace Yuml.Command
{
    /// <summary>
    /// commands that can be executed on a single classifier.
    /// </summary>
    public interface ISingleClassifierCommands : ISingleCommandContext
    {
        /// <summary>
        /// returns all available commands that can be executed on the
        /// properties of this class
        /// </summary>
        IListCommandContext<Property> CommandsForProperties { get; }
        /// <summary>
        /// returns all available commands for the methods of this class
        /// </summary>
        IListCommandContext<Method> CommandsForMethods { get; }
        /// <summary>
        /// command for changing the base class of this classifier
        /// </summary>
        IChangeTypeToNullCommand ChangeBaseClass { get; }
    }
}