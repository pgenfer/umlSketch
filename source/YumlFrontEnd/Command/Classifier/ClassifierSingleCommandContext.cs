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
        private readonly MessageSystem _messageSystem;

        public ClassifierSingleCommandContext(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            NotificationServices notificationServices,
            DeletionService deletionService,
            MessageSystem messageSystem)
        {
            _messageSystem = messageSystem;
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictionary,
                new ClassifierValidationService(classifierDictionary), 
                notificationServices.Classifier);
            CommandsForProperties = new PropertyListCommandContext(
                classifier,
                classifierDictionary,
                new PropertyValidationService(classifier.Properties), 
                notificationServices.Property);
            CommandsForMethods = new MethodListCommandContext(
                classifier,
                new MethodValidationService(classifier.Methods),
                notificationServices.Method);
            CommandsForAssociations = new AssociationListCommandContext(
                classifier,
                classifierDictionary,
                messageSystem);
            ChangeBaseClass = new ChangeBaseClassCommand(
                classifier,
                classifierDictionary, 
                notificationServices.Relation);
            Delete = new DeleteClassifierCommand(classifier, deletionService);
            // TODO: implement other commands from base class
        }

        public IListCommandContext<Property> CommandsForProperties { get; }
        public IListCommandContext<Method> CommandsForMethods { get; }
        public IListCommandContext<Relation> CommandsForAssociations { get; set; }
        public IChangeTypeToNullCommand ChangeBaseClass { get; }
    }
}
