using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;
using Yuml;

namespace Yuml.Command
{
    public class ClassifierSingleCommandContext : SingleCommandContextBase, ISingleClassifierCommands
    {
        public ClassifierSingleCommandContext(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            NotificationServices notificationServices)
        {
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictionary,
                new ClassifierValidationService(classifierDictionary), 
                notificationServices.Classifier);
            // TO DO: implement other commands
            CommandsForProperties = new PropertyListCommandContext(
                classifier,
                classifierDictionary,
                new PropertyValidationService(classifier.Properties), 
                notificationServices.Property); 
        }

        public IListCommandContext<Property> CommandsForProperties { get; }
    }
}
