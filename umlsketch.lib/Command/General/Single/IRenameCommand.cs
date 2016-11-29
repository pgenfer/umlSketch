using UmlSketch.Validation;

namespace UmlSketch.Command
{
    /// <summary>
    /// generic command used to rename a domain object.
    /// </summary>
    public interface IRenameCommand
    {
        void Rename(string newName);
        ValidationResult CanRenameWith(string newName);
    }
}