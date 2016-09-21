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
    public class ClassifierValidationServiceTest : SimpleTestBase
    {
        private ClassifierDictionary _classifiers;
        private ClassifierValidationService _validationService;
        private const string NewName = "newName";

        protected override void Init()
        {
            _classifiers = CreateDictionaryWithoutSystemTypes();
            _validationService = new ClassifierValidationService(_classifiers);
        }

        [TestDescription("Classifiers cannot have empty names")]
        public void ValidateClassifier_NameIsEmpty_Error()
        {
            var validation = _validationService.ValidateNameChange(string.Empty, string.Empty);

            Assert.IsTrue(validation.HasError);
            Assert.AreEqual(Strings.NameMustNotBeEmpty, validation.Message);
        }

        [TestDescription("Classifiers cannot have duplicate names")]
        public void ValidationClassifier_NameExists_Error()
        {
            _classifiers.IsClassNameFree(NewName).Returns(false);

            var validation = _validationService.ValidateNameChange("oldName", NewName);

            Assert.IsTrue(validation.HasError);
            Assert.AreEqual(Strings.NameAlreadyExists, validation.Message);
        }

        [TestDescription("Classifier name was not changed")]
        public void ValidationClassifier_NameNotChanged_NoError()
        {
            var validation = _validationService.ValidateNameChange(NewName, NewName);
            Assert.IsFalse(validation.HasError);
        }

        [TestDescription("Classifier name can be changed when called initially")]
        public void ValidationClassifier_Initially_NoError()
        {
            var validation = _validationService.ValidateNameChange(string.Empty, NewName);
            Assert.IsFalse(validation.HasError);
        }

        [TestDescription("Classifier name does not exist and can be changed")]
        public void ValidationClassifier_NameNotExists_NoError()
        {
            _classifiers.IsClassNameFree(NewName).Returns(true);

            var validation = _validationService.ValidateNameChange("oldName", NewName);

            Assert.IsFalse(validation.HasError);
        }
    }
}