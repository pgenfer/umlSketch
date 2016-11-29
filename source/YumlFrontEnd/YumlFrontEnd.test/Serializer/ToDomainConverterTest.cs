using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UmlSketch.DomainObject;
using UmlSketch.Serializer;
using UmlSketch.Serializer.Dto;

namespace UmlSketch.Test.Serializer
{
    public class ToDomainConverterTest : TestBase
    {
        private ClassifierDictionary _classifierDictionary;

        protected override void Init()
        {
            _classifierDictionary = new ClassifierDictionary();
        }

        [TestDescription(
            @"Ensures that DTOs are converted back to domain objects and 
              relations between the original references are kept")]
        public void Classifiers_ConvertToDto_KeepReferences()
        {
            // Arrange
            var dtos = new List<ClassifierDto> { StringDto, IntegerDto };
            var converter = new DomainDtoConverter();
            // Act
            converter.ToDomain(new Diagram(_classifierDictionary), new DiagramDataDto {Classifiers = dtos});

            // ensure that properties use the same object references
            // as the classifier dictionary
            Assert.AreEqual(
                _classifierDictionary.ElementAt(0), 
                _classifierDictionary.ElementAt(1).Properties.ElementAt(0).Type);
            Assert.AreEqual(_classifierDictionary.ElementAt(1),
                _classifierDictionary.ElementAt(0).Properties.ElementAt(0).Type);
        }

        [TestDescription("Checks that methods are converted correctly between domain and dto")]
        public void Methods_ConvertToDtoAndDomain()
        {
            // Arrange
            var dtos = new List<ClassifierDto> {VoidDto, StringDto, ServiceDto};
            var converter = new DomainDtoConverter();
            // Act
            converter.ToDomain(new Diagram(_classifierDictionary), new DiagramDataDto { Classifiers = dtos });
            // Assert return type has correct reference
            Assert.AreEqual(
                _classifierDictionary.Void,
                _classifierDictionary
                    .FindByName(Service.Name)
                    .Methods.ElementAt(0).ReturnType);
            // Assert parameters have correct reference
            Assert.AreEqual(
                _classifierDictionary.String,
                _classifierDictionary
                    .FindByName(Service.Name)
                    .Methods.ElementAt(0)
                    .Parameters.ElementAt(0).Type);
        }

        [TestDescription("Check that correct number of system types is available after loading")]
        public void LoadSystemTypes_AddMissingSystemTypes()
        {
            // Arrange
            var dtos = new List<ClassifierDto> { StringDto };
            var converter = new DomainDtoConverter();
            // Act
            converter.ToDomain(new Diagram(_classifierDictionary), new DiagramDataDto { Classifiers = dtos });
            // Assert => one system type was loaded, the other ones should be added automatically
            Assert.AreEqual(new SystemTypes().Count(),_classifierDictionary.Count);
        }

        [TestDescription("Check that system types are referenced correctly after loading")]
        public void LoadClassifiers_SystemTypeReferences_AreCorrect()
        {
            const string classifierName = "ClassifierWithSystemType";
            // Arrange
            var dtoWithSystemTypeProperty = new ClassifierDto
            {
                Name = classifierName,
                Properties = new List<PropertyDto>{new PropertyDto {Name = "Property", Type = StringDto}}
            };
            var dtos = new List<ClassifierDto> { dtoWithSystemTypeProperty,StringDto };
            var converter = new DomainDtoConverter();
            // Act
            converter.ToDomain(new Diagram(_classifierDictionary), new DiagramDataDto { Classifiers = dtos });
            // Assert => ensure that the instance of String is always the same in the dictionary
            Assert.AreEqual(
                _classifierDictionary.String,
                _classifierDictionary.FindByName(classifierName).Properties.Single().Type);
        }
    }
}
