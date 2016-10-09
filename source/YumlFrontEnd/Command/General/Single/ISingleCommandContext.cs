namespace Yuml.Command
{
    /// <summary>
    /// generic commands available for a single domain object
    /// </summary>
    public interface ISingleCommandContext
    {
        IRenameCommand Rename { get; }
        IDeleteCommand Delete { get; }
        ShowOrHideSingleObjectCommand Visibility { get; }
    }
}