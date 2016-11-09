namespace Yuml.Command
{
    /// <summary>
    /// generic commands for a complete list of domain objects.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IListCommandContext<out TDomain>
    {
        INewCommand New { get; }
        ShowOrHideAllObjectsInListCommand Visibility { get; }
        IQuery<TDomain> All { get; }
    }
}