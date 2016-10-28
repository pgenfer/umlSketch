﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    class ChangeAssociationTargetCommand : DomainObjectBaseCommand<Relation>, IChangeTypeCommand
    {
        private readonly ClassifierDictionary _classifiers;
        
        public ChangeAssociationTargetCommand(
            Relation domainObject,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem) : base(domainObject,messageSystem)
        {
            Requires(classifiers != null);
            _classifiers = classifiers;
        }

        public void ChangeType(string nameOfOldType, string nameOfNewType)
        {
            var newClass = _classifiers.FindByName(nameOfNewType);
            _domainObject.End.Classifier = newClass;
            _messageSystem.Publish(_domainObject, new ChangeAssociationTargetEvent(_domainObject, newClass));
        }
    }
}