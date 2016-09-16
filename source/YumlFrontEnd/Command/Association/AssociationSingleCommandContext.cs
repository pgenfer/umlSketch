

namespace Yuml.Command
{
    public class SingleAssociationCommands : SingleCommandContextBase, ISingleAssociationCommands
    {
        public SingleAssociationCommands(
            ClassifierDictionary classifiers,
            Relation association,
            MessageSystem messageSystem)
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
