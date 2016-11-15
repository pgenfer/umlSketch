using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.DomainObject;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    public class DeleteAssocationCommand : IDeleteCommand
    {
        private readonly ClassifierAssociationList _associations;
        private readonly Relation _association;
        private readonly MessageSystem _messageSystem;

        public DeleteAssocationCommand(
            ClassifierAssociationList associations,
            Relation association,
            MessageSystem messageSystem)
        {
            Requires(association != null);
            Requires(association != null);
            Requires(messageSystem != null);
            Requires(associations.Contains(association));

            _associations = associations;
            _association = association;
            _messageSystem = messageSystem;
        }

        public void DeleteItem() => _associations.RemoveAssociation(_association, _messageSystem);
    }
}
