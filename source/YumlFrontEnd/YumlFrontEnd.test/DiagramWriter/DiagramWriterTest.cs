using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    [TestFixture]
    public class DiagramWriterTest
    {
        [TestDescription("Create the uml text for the diagram from a given class with its properties")]
        public void DiagramWriter_CreateClassWithProperties()
        {
            var diagram = new DiagramWriter();
            var umlText = diagram
                .StartClass()
                    .WithName("DiagramWriter")
                    .WithNewProperty()
                        .WithType("int")
                        .WithName("Length")
                    .WithNewProperty()
                        .WithType("string")
                        .WithName("Name")
                    .Finish()
                .Finish()
                .ToString();

            var expectedResult = "[DiagramWriter|int Length;string Name;]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create the uml text for the diagram from a given class without any members")]
        public void DiagramWriter_CreateEmptyClass()
        {
            var diagram = new DiagramWriter();
            var umlText = diagram
                .StartClass()
                    .WithName("DiagramWriter")
                .Finish()
                .ToString();

            var expectedResult = "[DiagramWriter]";
            Assert.AreEqual(expectedResult, umlText);
        }
    }
}

