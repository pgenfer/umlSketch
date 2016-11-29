using NUnit.Framework;
using UmlSketch.Settings;

namespace UmlSketch.Test
{
    public class DiagramWriterTest : SimpleTestBase
    {
        private DiagramWriter.DiagramWriter _diagram;

        protected override void Init()
        {
            _diagram = new DiagramWriter.DiagramWriter();
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

        [TestDescription("Write a class with a method")]
        public void DiagramWriter_CreateClassWithMethods()
        {
            var umlText = _diagram
                .StartClass()
                    .WithName("DiagramWriter")
                    .WithNewMethod()
                        .WithReturnType("void")
                        .WithName("Write")
                    .WithNewMethod()
                        .WithReturnType("int")
                        .WithName("CountEntries")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[DiagramWriter|void Write();int CountEntries();]";
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
                        .WithStartNode("Person",DiagramDirection.LeftToRight)
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
                        .WithStartNode("Car",DiagramDirection.LeftToRight)
                        .WithName("exists of")
                        .AsCompositeOwner()
                        .ToClassifier("Parts")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Car]exists of++-[Parts]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create a uses-relation between two classes")]
        public void DiagramWriter_CreateUsesRelation()
        {
            var umlText =
                _diagram
                    .StartRelation()
                        .WithStartNode("Car",DiagramDirection.LeftToRight)
                        .WithName("uses")
                        .AsUsesRelation()
                        .WithNavigation()
                        .ToClassifier("Fuel")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Car]uses-.->[Fuel]";
            Assert.AreEqual(expectedResult, umlText);
        }

        [TestDescription("Create an interface implementation")]
        public void DiagramWriter_CreateInterfaceImplementation()
        {
            var umlText = 
                _diagram
                    .StartRelation()
                        .WithStartNode("INamed", DiagramDirection.LeftToRight)
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
                        .WithStartNode("Base", DiagramDirection.LeftToRight)
                        .WithDerivation("Derived")
                    .Finish()
                .Finish()
                .ToString();

            const string expectedResult = "[Base]^-[Derived]";
            Assert.AreEqual(expectedResult, umlText);
        }
    }
}

