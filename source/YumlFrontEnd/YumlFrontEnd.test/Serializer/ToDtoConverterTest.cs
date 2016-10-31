using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Serializer.Dto;

namespace Yuml.Test
{
    public class ToDtoConverterTest : TestBase
    {
        private DomainDtoConverter _converter;
        private ClassifierDictionary _classifiers;

        protected override void Init()
        {
            _converter = new DomainDtoConverter();
        }

        [TestDescription("Ensures that the DTO converter keeps the relations between the original references")]
        public void Classifiers_ConvertToDto_KeepReferences()
        {
            // Arrange
            _classifiers = new ClassifierDictionary(Integer, String);
          
            // Act
            var dtos = _converter.ToDto(new Diagram(_classifiers)).Classifiers;

            // ensure that integer classifier is the same reference
            // for the dto and the type of the string's property
            Assert.AreEqual(dtos[0], dtos[1].Properties[0].Type);
            Assert.AreEqual(dtos[1], dtos[0].Properties[0].Type);
        }

        [TestDescription("Ensures that the DTO converter converts methods correctly with their references")]
        public void ClassifierWithMethod_ConvertToDto_KeepReferences()
        {
            // Arrange
            _classifiers = new ClassifierDictionary(Void, String, Service);
          
            // Act
            var dtos = _converter.ToDto(new Diagram(_classifiers)).Classifiers;

            // Assert
            Assert.AreEqual(dtos[0], dtos[2].Methods[0].ReturnType);
            Assert.AreEqual(dtos[1], dtos[2].Methods[0].Parameters[0].Type);
        }

        [TestDescription("Check that base class is serialized")]
        public void ClassifierWithBaseClass_ConvertToDto_KeepBaseClass()
        {
            Car.BaseClass = Vehicle;
            _classifiers = new ClassifierDictionary(Car, Vehicle);
            var dtos = _converter.ToDto(new Diagram(_classifiers)).Classifiers;

            Assert.AreEqual(dtos[0].BaseClass,dtos[1]);
        }

        [TestDescription("Missing system types should be added after hydrating")]
        public void ClassifierWithSystemTypes_ConvertFromDto_MissingSystemTypesAreAdded()
        {
            _classifiers = new ClassifierDictionary();
            // act: convert dtos to classifier types
            _converter = new DomainDtoConverter();
            _converter.ToDomain(new Diagram(_classifiers),new DiagramDataDto {Classifiers = new List<ClassifierDto>()});

            Assert.AreEqual(new SystemTypes().Count(), _classifiers.Count);
        }
    }
}
