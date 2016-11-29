#define TEST


using NSubstitute;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Test;
using static NSubstitute.Substitute;

namespace Yuml.Command.Association.Test
{


    public class ChangeAssociationCommandTest : TestBase
    {
        private MessageSystem _messageSystem;
        private Relation _association;
        private ChangeAssociationCommand _command;

        protected override void Init()
        {
            _messageSystem = For<MessageSystem>();
            _association = For<Relation>();
            _command = new ChangeAssociationCommand(_association, _messageSystem);
        }

        [TestDescription("Ensures that the new relation type is set")]
        public void ChangeAssociation_SetNewRelationType()
        {
            _command.ChangeAssociation(RelationType.Aggregation);
            _association.Received().Type = RelationType.Aggregation;
        }

        [TestDescription("Ensures that a mesage is published when the association is changed")]
        public void ChangeAssociation_PublishMessage()
        {
            _command.ChangeAssociation(RelationType.Composition);
            _messageSystem.Received().Publish(
                _association,
                Arg.Any<ChangeAssocationTypeEvent>());
        }
    }
}