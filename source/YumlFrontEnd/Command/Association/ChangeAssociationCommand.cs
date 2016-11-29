using UmlSketch.DomainObject;
using UmlSketch.Event;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Command
{
    /// <summary>
    /// when executed, changes the assocation of the start classifier to the new
    /// association type.
    /// This command works only for associations, derivation and implementation
    /// is not handled by this command.
    /// </summary>
    public class ChangeAssociationCommand : DomainObjectBaseCommand<Relation> 
    {
        public ChangeAssociationCommand(
            Relation domainObject,
            MessageSystem messageSystem) : base(domainObject,messageSystem)
        {
        }

        public void ChangeAssociation(RelationType newRelationType)
        {
            Requires(newRelationType != RelationType.Implementation);
            Requires(newRelationType != RelationType.Inheritance);
            Ensures(_domainObject.Type != OldValue(_domainObject.Type));

            _domainObject.Type = newRelationType;
            _messageSystem.Publish(_domainObject,new ChangeAssocationTypeEvent(_domainObject));
        }
    }
}
