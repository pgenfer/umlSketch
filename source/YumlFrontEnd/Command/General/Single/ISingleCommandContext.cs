namespace UmlSketch.Command
{
    /// <summary>
    /// marker interface for commands
    /// that are used by a single domain item.
    /// </summary>
    /// <typeparam name="TDomain">Generic type is used to
    /// map the type of the domain object to the interface,
    /// should be specified by derived classes/interfaces.</typeparam>
    public interface ISingleCommandContext<TDomain>
    {
        IRenameCommand Rename { get; }
        IDeleteCommand Delete { get; }
        IShowOrHideCommand Visibility { get; }
    }
}