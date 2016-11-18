

using Yuml.DomainObject;
using Yuml.Service;

namespace Yuml.Command
{
    public class SingleAssociationCommands : SingleCommandContextBase<Relation>, ISingleAssociationCommands
    {
        public SingleAssociationCommands(
            ClassifierAssociationList associations,
            Relation association,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem,IAskUserBeforeDeletionService askUserBeforeDeletion):
            base(associations,association,messageSystem, askUserBeforeDeletion)
        {
            Rename = new RenameAssociationCommand();
            ChangeAssociationTargetCommand = new 
                ChangeAssociationTargetCommand(association,classifiers,messageSystem);
            ChangeAssociationTypeCommand = new 
                ChangeAssociationCommand(association,messageSystem);
        }

        public IChangeTypeCommand ChangeAssociationTargetCommand { get; }
        public ChangeAssociationCommand ChangeAssociationTypeCommand { get; }
    }
}
