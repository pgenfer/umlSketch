using NUnit.Framework;
using YumlFrontEnd.editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Test;

namespace Yuml.Test
{
    public class SerializationMixinTest : TestBase
    {
        [TestDescription("Check that system types are loaded after diagram was reset")]
        public void ResetDiagram_SystemTypesAvailable()
        {
            var diagram = new Diagram();
            diagram.Reset();
            Assert.AreEqual(new SystemTypes().Count(),diagram.Classifiers.Count);
        }
    }
}