

namespace Yuml.Command
{
    public class SingleAssociationCommands : SingleCommandContextBase<Relation>, ISingleAssociationCommands
    {
        public SingleAssociationCommands(
            Relation association,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            Rename = new RenameAssociationCommand();
            ChangeAssociationTargetCommand = new 
                ChangeAssociationTargetCommand(association,classifiers,messageSystem);
            ChangeAssociationTypeCommand = new 
                ChangeAssociationCommand(association,messageSystem);
            Visibility = new ShowOrHideSingleObjectCommand(association, messageSystem);
        }

        public IChangeTypeCommand ChangeAssociationTargetCommand { get; }
        public ChangeAssociationCommand ChangeAssociationTypeCommand { get; }
    }
}
