namespace Yuml.Commands
{
    /// <summary>
    /// interface that contains all commands that can be executed on a single
    /// classifier
    /// </summary>
    public interface IClassiferCommands
    {
        IRenameCommand RenameCommand { get; }
    }
}