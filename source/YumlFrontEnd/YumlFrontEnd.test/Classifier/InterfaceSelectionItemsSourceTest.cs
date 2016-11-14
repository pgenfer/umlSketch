using NUnit.Framework;
using YumlFrontEnd.editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Yuml;
using Yuml.Command;
using Yuml.Test;
using static NSubstitute.Substitute;

namespace YumlFrontEnd.Test
{
    public class InterfaceSelectionItemsSourceTest : TestBaseWithContext
    {
        private IQuery<Classifier> _query;

        protected override void Init()
        {
            base.Init();
            _query = For<IQuery<Classifier>>();
            _query.Get().Returns(Enumerable.Empty<Classifier>());
        }

        [TestDescription("Checks that only interfaces are added to the item source")]
        public void NewClass_Created_NotAddedToList()
        {
            var domainEvent = new DomainObjectCreatedEvent<Classifier>(new Classifier("class"));
            var interfaceSelectionSource = 
                new InterfaceSelectionItemsSource(_classifiers, _query, _messageSystem);
            // act: let source react on add event
            interfaceSelectionSource.OnNewClassifierCreated(domainEvent);
            // assert: class should not be added
            Assert.IsEmpty(interfaceSelectionSource);
        }

        [TestDescription("Checks that only interfaces are added to the item source")]
        public void NewInterface_Created_AddedToList()
        {
            var domainEvent = new DomainObjectCreatedEvent<Classifier>(
                new Classifier("interface") {IsInterface = true});
            var interfaceSelectionSource =
                new InterfaceSelectionItemsSource(_classifiers, _query, _messageSystem);
            // act: let source react on add event
            interfaceSelectionSource.OnNewClassifierCreated(domainEvent);
            // assert: interface should be added
            Assert.IsTrue(interfaceSelectionSource.Count == 1);
        }
    }
}