

using Yuml.DomainObject;

namespace Yuml.Command
{
    public class SingleAssociationCommands : SingleCommandContextBase<Relation>, ISingleAssociationCommands
    {
        public SingleAssociationCommands(
            ClassifierAssociationList associations,
            Relation association,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem):base(associations,association,messageSystem)
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
