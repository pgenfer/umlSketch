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
            NotificationServices notificationServices,
            DeletionService deletionService,
            MessageSystem messageSystem)
        {
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictionary,
                new ClassifierValidationService(classifierDictionary), 
                notificationServices.Classifier);
            CommandsForProperties = new PropertyListCommandContext(
                classifier,
                classifierDictionary,
                new PropertyValidationService(classifier.Properties), 
                notificationServices.Property,
                messageSystem);
            CommandsForMethods = new MethodListCommandContext(
                classifier,
                new MethodValidationService(classifier.Methods),
                notificationServices.Method,
                messageSystem);
            CommandsForAssociations = new AssociationListCommandContext(
                classifier,
                classifierDictionary,
                messageSystem);
            ChangeBaseClass = new ChangeBaseClassCommand(
                classifier,
                classifierDictionary, 
                notificationServices.Relation);
            Delete = new DeleteClassifierCommand(classifier, deletionService);
            Visibility = new ShowOrHideSingleObjectCommand(classifier, messageSystem);
        }

        public IListCommandContext<Property> CommandsForProperties { get; }
        public IListCommandContext<Method> CommandsForMethods { get; }
        public IListCommandContext<Relation> CommandsForAssociations { get; set; }
        public IChangeTypeToNullCommand ChangeBaseClass { get; }
    }
}
