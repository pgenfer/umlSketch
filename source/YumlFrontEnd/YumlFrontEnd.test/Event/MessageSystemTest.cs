using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    [TestFixture()]
    public class MessageSystemTest : SimpleTestBase
    {
        private MessageSystem _messageSystem;
        private readonly object _source = new object();
        private readonly object _otherSource = new object();

        protected override void Init()
        {
            _messageSystem = new MessageSystem();
        }

        /// <summary>
        /// dummy event used for testing
        /// </summary>
        public class TestEvent : IDomainEvent
        { }

        /// <summary>
        /// event handler class is used for testing
        /// </summary>
        public class TestEventHandler
        {
            public bool EventWasHandled { get; private set; }
            public void HandleEvent(TestEvent testEvent) => EventWasHandled = true;
        }

        [TestDescription("MessageSystem - Publish/Subscribe")]
        public void PublishAndSubscribe()
        {
            var messageReceived = false;
            _messageSystem.Subscribe<TestEvent>(_source, _ => messageReceived = true);
            _messageSystem.Publish(_source, new TestEvent());

            Assert.IsTrue(messageReceived);
        }

        [TestDescription("MessageSystem - Publish without subscription - event handler not fired")]
        public void Publish_Without_Subscription()
        {
            var messageReceived = false;
            _messageSystem.Subscribe<TestEvent>(_otherSource, _ => messageReceived = true);
            _messageSystem.Publish(_source, new TestEvent());

            Assert.IsFalse(messageReceived);
        }

        [TestDescription("MessageSystem - Multiple subscriptions")]
        public void MultipleSubscriptionsForOneEvent()
        {
            var firstMessageReceived = false;
            var secondMessageReceived = false;
            _messageSystem.Subscribe<TestEvent>(_source, _ => firstMessageReceived = true);
            _messageSystem.Subscribe<TestEvent>(_source, _ => secondMessageReceived = true);
            _messageSystem.Publish(_source, new TestEvent());

            Assert.IsTrue(firstMessageReceived && secondMessageReceived);
        }

        [TestDescription("Non generic subscription - used when event handlers are registered via reflection")]
        public void SubscribeNonGeneric()
        {
            var eventHandler = new TestEventHandler();
            _messageSystem.Subscribe(_source, typeof(TestEvent),x => eventHandler.HandleEvent((TestEvent)x));
            _messageSystem.Publish(_source, new TestEvent());

            Assert.IsTrue(eventHandler.EventWasHandled);
        }
    }
}