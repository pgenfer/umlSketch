using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using static NSubstitute.Substitute;

namespace Yuml.Test
{
    public class ClassifierDictionaryTest : SimpleTestBase
    {
        private ClassifierDictionary _classifiers;

        [TestDescription("Check that classifier dictionary can rename classifiers")]
        public void RenameClassifierTest()
        {
            var oldName = RandomString(1);
            var newName = RandomString(2);

            var classifier = new Classifier(oldName);
            _classifiers = new ClassifierDictionary(classifier);
            _classifiers.RenameClassifier(classifier, newName);

            Assert.AreEqual(newName, classifier.Name);
            Assert.IsTrue(_classifiers.IsClassNameFree(oldName));
            Assert.IsNotNull(_classifiers.FindByName(newName));
        }

        [TestDescription("Creates classifier list without system types")]
        public void ClassifierDictionary_NoSystemTypes()
        {
            _classifiers = new ClassifierDictionary(false);
            Assert.IsEmpty(_classifiers);
        }

        [TestDescription("Creates classifier list with system types")]
        public void ClassifierDictionary_WithSystemTypes()
        {
            _classifiers = new ClassifierDictionary();
            Assert.IsNotEmpty(_classifiers);
        }

        [TestDescription("Returns the class which implements the given interface")]
        public void FindAllImplementersTest()
        {
            // setup a class that implements an interface
            var @interface = new Classifier("interface") {IsInterface = true};
            var classifier = For<Classifier>("class",false);
            var subSet = For<ImplementationList.SubSet>();
            subSet.IsNotEmpty().Returns(true);
            classifier.FindImplementationsOfInterface(@interface).Returns(subSet);
            _classifiers = new ClassifierDictionary(classifier);
            
            // return all classes that have an implementation for the given interface
            var implementers = _classifiers.FindAllImplementers(@interface);
            
            // ensure that the implementer is in the result list
            Assert.Contains(classifier, implementers.ToList());
        }
    }
}