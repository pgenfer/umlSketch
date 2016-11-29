

using UmlSketch.DomainObject;

namespace UmlSketch.Command
{
    public interface ISingleAssociationCommands : ISingleCommandContext<Association>
    {
        /// <summary>
        /// command that is executed when the target classifier of the association
        /// is changed.
        /// </summary>
        IChangeTypeCommand ChangeAssociationTargetCommand { get; }
        /// <summary>
        /// command changes the type of the relation
        /// </summary>
        ChangeAssociationCommand ChangeAssociationTypeCommand { get; }
    }
}
