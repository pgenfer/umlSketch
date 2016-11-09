

namespace Yuml.Command
{
    public interface ISingleAssociationCommands : ISingleCommandContext<Relation>
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
