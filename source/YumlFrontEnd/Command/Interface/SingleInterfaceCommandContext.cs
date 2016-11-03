﻿using System;
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
        public IRenameCommand Rename => null;

        public IDeleteCommand Delete { get; }
        public ChangeInterfaceOfClassifierCommand ChangeInterface { get; }

        public ShowOrHideSingleObjectCommand Visibility { get; }

        public SingleInterfaceCommandContext(
            Classifier interfaceOwner,
            Implementation existingInterface,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            Delete = new RemoveInterfaceFromClassifierCommand(interfaceOwner, existingInterface, messageSystem);
            ChangeInterface = new ChangeInterfaceOfClassifierCommand(
                interfaceOwner,
                existingInterface,
                classifiers,
                messageSystem);
            Visibility = new ShowOrHideSingleObjectCommand(existingInterface, messageSystem);
        }
    }
}

