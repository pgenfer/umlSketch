namespace Yuml.Command
{
    /// <summary>
    /// generic commands for a complete list of domain objects.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IListCommandContext<TDomain>
    {
        INewCommand New { get; }
        ShowOrHideAllObjectsInListCommand Visibility { get; }
        IQuery<TDomain> All { get; }
        /// <summary>
        /// returns the commands that are available for a single item
        /// within the list. Result of this method can be casted to the concrete
        /// type if required.
        /// </summary>
        ISingleCommandContext GetCommandsForSingleItem(TDomain domainObject);
    }
}