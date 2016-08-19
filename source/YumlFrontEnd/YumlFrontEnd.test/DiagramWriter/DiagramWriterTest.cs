using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    public class DiagramWriterTest : SimpleTestBase
    {
        private DiagramWriter _diagram;

        protected override void Init()
        {
            _diagram = new DiagramWriter();
        }

        [TestDescription("Create the uml text for the diagram from a given class with its properties")]
        public void DiagramWriter_CreateClassWithProperties()
        {
            var umlText = _diagram
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

            const string expectedResult = "[DiagramWriter|int Length;string Name;]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create the uml text for the diagram from a given class without any members")]
        public void DiagramWriter_CreateEmptyClass()
        {
            var umlText = _diagram
                .StartClass()
                    .WithName("DiagramWriter")
                .Finish()
                .ToString();

            const string expectedResult = "[DiagramWriter]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create a simple association between two classes")]
        public void DiagramWriter_CreateSimpleAssociation()
        {
            var umlText = _diagram
                    .StartRelation()
                        .WithStartNode("Person")
                        .WithName("has")
                        .AsSimpleAssociation()
                        .ToClassifier("Name")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Person]has-[Name]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create a composition between two classes")]
        public void DiagramWriter_CreateCompositeAssociation()
        {
            var umlText =
                _diagram
                    .StartRelation()
                        .WithStartNode("Car")
                        .WithName("exists of")
                        .AsCompositeOwner()
                        .ToClassifier("Parts")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Car]exists of++-[Parts]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create an interface implementation")]
        public void DiagramWriter_CreateInterfaceImplementation()
        {
            var umlText = 
                _diagram
                    .StartRelation()
                        .WithStartNode("INamed")
                        .WithInterfaceImplementation("Name")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[INamed]^-.-[Name]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create derivation")]
        public void DiagramWriter_CreateDerivation()
        {
            var umlText = 
                _diagram
                    .StartRelation()
                        .WithStartNode("Base")
                        .WithDerivation("Derived")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Base]^-[Derived]";
            Assert.AreEqual(expectedResult, umlText);
        }
    }
}

