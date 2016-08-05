using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    public class ClassifierTest : TestBase
    {
        [TestDescription("Create a diagram from classifiers without properties")]
        public void Classifiers_WriteTo()
        {
            var diagramWriter = new DiagramWriter();
            var result = "[string][int]";

            var classifiers = new ClassifierDictionary(String, Integer);
            String.IsVisible = true;
            Integer.IsVisible = true;

            classifiers.WriteTo(diagramWriter);
            var umlText = diagramWriter.ToString();            

            Assert.AreEqual(result, umlText);
        }

        [TestDescription("Create a diagram from a classifier with properties")]
        public void ClassifiersWithProperties_WriteTo()
        {
            var diagramWriter = new DiagramWriter();
            var result = "[string|int Length;]";

            var classifiers = new ClassifierDictionary(String);
            String.IsVisible = true;
            String.Properties.Single().IsVisible = true;


            classifiers.WriteTo(diagramWriter);
            var umlText = diagramWriter.ToString();

            Assert.AreEqual(result, umlText);
        }
    }
}