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
            ClassifierDictionary classifierDictioanry,
            ValidationServices validationServices,
            ClassifierNotificationService classifierNotificationService)
        {
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictioanry,
                validationServices.Classifier,
                classifierNotificationService);
            // TO DO: implement other commands
            CommandsForProperties = new PropertyListCommandContext(
                classifier,
                validationServices.Property); 
        }


        public IListCommandContext<Property> CommandsForProperties { get; }
    }
}
