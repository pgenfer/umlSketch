using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace Yuml.Test
{
    [TestFixture]
    public class ClassifierValidationServiceTest
    {
        private ClassifierDictionary _classifiers;
        private ClassifierValidationService _validationService;

        [SetUp]
        public void Init()
        {
            _classifiers = Substitute.For<ClassifierDictionary>();
            _validationService = new ClassifierValidationService(_classifiers);
        }

        [TestDescription("Classifiers cannot have empty names")]
        public void ValidateClassifier_NameIsEmpty_Error()
        {
            var validation = _validationService.CheckName(string.Empty, string.Empty);

            Assert.IsTrue(validation.HasError);
            Assert.AreEqual(Strings.ClassNameMustNotBeEmpty, validation.Message);
        }

        [TestDescription("Classifiers cannot have duplicate names")]
        public void ValidationClassifier_NameExists_Error()
        {
            var newName = "newName";
            _classifiers.IsClassNameFree(newName).Returns(false);

            var validation = _validationService.CheckName("oldName", newName);

            Assert.IsTrue(validation.HasError);
            Assert.AreEqual(Strings.ClassNameAlreadyExists, validation.Message);
        }

        [TestDescription("Classifier name was not changed")]
        public void ValidationClassifier_NameNotChanged_NoError()
        {
            var newName = "newName";
            var validation = _validationService.CheckName(newName, newName);

            Assert.IsFalse(validation.HasError);
        }

        [TestDescription("Classifier name can be changed when called initially")]
        public void ValidationClassifier_Initially_NoError()
        {
            var newName = "newName";
            var validation = _validationService.CheckName(string.Empty, newName);

            Assert.IsFalse(validation.HasError);
        }

        [TestDescription("Classifier name does not exist and can be changed")]
        public void ValidationClassifier_NameNotExists_NoError()
        {
            var newName = "newName";
            _classifiers.IsClassNameFree(newName).Returns(true);

            var validation = _validationService.CheckName("oldName", newName);

            Assert.IsFalse(validation.HasError);
        }


    }
}