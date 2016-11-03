using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command.Interface;

namespace Yuml.Command
{
    public class SingleInterfaceCommandContext : ISingleCommandContext
    {
        /// <summary>
        /// this is a bit ugly, because the command is not available for interfaces but is in the
        /// base interface that is used by all other view models, we have to keep it there.
        /// </summary>
        public IRenameCommand Rename
        {
            get { throw new NotImplementedException("Rename is not available for interfaces"); }
        }

        public IDeleteCommand Delete { get; }
        public ChangeInterfaceOfClassifierCommand ChangeInterface { get; }

        /// <summary>
        /// currently, interfaces cannot be made visible / invisible
        /// We could change this later, but then we must create concrete interface objects
        /// that we can store in the interface list (with an additional IsVisible property)
        /// </summary>
        public ShowOrHideSingleObjectCommand Visibility
        {
            get { throw new NotImplementedException("ChangeVisibility is not available for interfaces"); }
        }

        public SingleInterfaceCommandContext(
            Classifier interfaceOwner,
            Classifier existingInterface,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            Delete = new RemoveInterfaceFromClassifierCommand(interfaceOwner, existingInterface, messageSystem);
            ChangeInterface = new ChangeInterfaceOfClassifierCommand(
                interfaceOwner,
                existingInterface,
                classifiers,
                messageSystem);
        }
    }
}

