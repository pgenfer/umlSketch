using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;
using Yuml;
using Yuml.Service;

namespace Yuml.Command
{
    public class ClassifierSingleCommandContext : SingleCommandContextBase, ISingleClassifierCommands
    {
        public ClassifierSingleCommandContext(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            DeletionService deletionService,
            MessageSystem messageSystem)
        {
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictionary,
                new ClassifierValidationService(classifierDictionary),
                messageSystem);
            CommandsForProperties = new PropertyListCommandContext(
                classifier,
                classifierDictionary,
                new PropertyValidationService(classifier.Properties), 
                messageSystem);
            CommandsForMethods = new MethodListCommandContext(
                classifier,
                classifierDictionary,
                new MethodValidationService(classifier.Methods),
                messageSystem);
            CommandsForAssociations = new AssociationListCommandContext(
                classifier,
                classifierDictionary,
                messageSystem);
            ChangeBaseClass = new ChangeBaseClassCommand(
                classifier,
                classifierDictionary,
                messageSystem);
            Delete = new DeleteClassifierCommand(classifier, deletionService);
            Visibility = new ShowOrHideSingleObjectCommand(classifier, messageSystem);
            ChangeClassifierColor = new ChangeColorCommand(classifier, messageSystem);
            ChangeNoteColor = new ChangeNoteColorCommand(classifier.Note,messageSystem);
            ChangeNoteText = new ChangeNoteTextCommand(classifier.Note,messageSystem);
            ChangeIsInterface = new MakeClassifierToInterfaceCommand(classifier, messageSystem);
        }

        public IListCommandContext<Property> CommandsForProperties { get; }
        public IListCommandContext<Method> CommandsForMethods { get; }
        public IListCommandContext<Relation> CommandsForAssociations { get; set; }
        public IChangeTypeToNullCommand ChangeBaseClass { get; }
        public IChangeColorCommand ChangeClassifierColor { get; }
        public IChangeColorCommand ChangeNoteColor { get; }
        public ChangeNoteTextCommand ChangeNoteText { get; }
        public MakeClassifierToInterfaceCommand ChangeIsInterface { get; }
    }
}
