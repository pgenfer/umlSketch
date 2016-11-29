using UmlSketch.DomainObject;

namespace UmlSketch.Command
{
    /// <summary>
    /// additional commands available for a single property
    /// </summary>
    public interface ISinglePropertyCommands : ISingleCommandContext<Property>
    {
        IChangeTypeCommand ChangeTypeOfProperty { get; }
    }
}
