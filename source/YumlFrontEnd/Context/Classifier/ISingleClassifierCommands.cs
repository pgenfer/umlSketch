namespace Yuml.Command
{
    public interface ISingleClassifierCommands : ISingleCommandContext
    {
        IListCommandContext<Property> CommandsForProperties { get; }
    }
}