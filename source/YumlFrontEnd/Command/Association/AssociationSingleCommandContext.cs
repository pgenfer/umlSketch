

using Yuml.DomainObject;
using Yuml.Service;

namespace Yuml.Command
{
    public class SingleAssociationCommands : 
        SingleCommandContextBase<Association>, 
        ISingleAssociationCommands
    {
        public SingleAssociationCommands(
            ClassifierAssociationList associations,
            Association association,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem,IAskUserBeforeDeletionService askUserBeforeDeletion):
            base(associations,association,messageSystem, askUserBeforeDeletion)
        {
            Rename = new RenameAssociationCommand(association, messageSystem);
            ChangeAssociationTargetCommand = new 
                ChangeAssociationTargetCommand(association,classifiers,messageSystem);
            ChangeAssociationTypeCommand = new 
                ChangeAssociationCommand(association,messageSystem);
        }

        public IChangeTypeCommand ChangeAssociationTargetCommand { get; }
        public ChangeAssociationCommand ChangeAssociationTypeCommand { get; }
    }
}
