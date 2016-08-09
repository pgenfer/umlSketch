using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    public class ClassifierDictionaryTest : SimpleTestBase
    {
        [TestDescription("Check that classifier dictionary can rename classifiers")]
        public void RenameClassifierTest()
        {
            var oldName = RandomString(1);
            var newName = RandomString(2);

            var classifier = new Classifier(oldName);
            var dictionary = new ClassifierDictionary(classifier);
            dictionary.RenameClassifier(classifier, newName);

            Assert.AreEqual(newName, classifier.Name);
            Assert.IsTrue(dictionary.IsClassNameFree(oldName));
            Assert.IsNotNull(dictionary.FindByName(newName));
        }
    }
}