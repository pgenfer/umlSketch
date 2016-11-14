using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NSubstitute.Substitute;

namespace Yuml.Test
{
    /// <summary>
    /// base class for tests that need access to members of the application context
    /// like the classifier dictionary or the message system.
    /// The context objects are stubbed in the Init method
    /// and can be configured in every test case as needed.
    /// </summary>
    public class TestBaseWithContext : SimpleTestBase
    {
        protected MessageSystem _messageSystem;
        protected ClassifierDictionary _classifiers;

        protected override void Init()
        {
            _messageSystem = For<MessageSystem>();
            _classifiers = CreateDictionaryWithoutSystemTypes();
        }
    }
}
