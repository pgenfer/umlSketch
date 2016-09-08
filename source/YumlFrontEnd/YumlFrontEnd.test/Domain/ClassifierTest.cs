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
    public class ClassifierTest : TestBase
    {
        private DiagramWriter _diagramWriter;
        private ClassifierDictionary _classifiers;
        private string _result;

        protected override void Init()
        {
            _diagramWriter = new DiagramWriter();
        }

        [TestDescription("Create a diagram from classifiers without properties")]
        public void Classifiers_WriteTo()
        {
            _result = "[string][int]";

            _classifiers = new ClassifierDictionary(String, Integer);
            String.IsVisible = true;
            Integer.IsVisible = true;

            _classifiers.WriteTo(_diagramWriter);
            var umlText = _diagramWriter.ToString();

            Assert.AreEqual(_result, umlText);
        }

        [TestDescription("Create a diagram from a classifier with properties")]
        public void ClassifiersWithProperties_WriteTo()
        {
            _result = "[string|int Length;]";

            _classifiers = new ClassifierDictionary(String);
            String.IsVisible = true;
            String.Properties.Single().IsVisible = true;


            _classifiers.WriteTo(_diagramWriter);
            var umlText = _diagramWriter.ToString();

            Assert.AreEqual(_result, umlText);
        }

        [TestDescription("Create a diagram from a classifier with methods")]
        public void ClassifiersWithMethods_WriteTo()
        {
            _result = "[Service|void DoSomething();]";
            _classifiers = new ClassifierDictionary(Service);
            Service.IsVisible = true;
            Service.Methods.Single().IsVisible = true;

            _classifiers.WriteTo(_diagramWriter);
            var umlText = _diagramWriter.ToString();

            Assert.AreEqual(_result, umlText);
        }
    }
}