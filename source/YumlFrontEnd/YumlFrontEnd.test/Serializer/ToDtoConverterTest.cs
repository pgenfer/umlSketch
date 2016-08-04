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
        [TestDescription("Ensures that the DTO converter keeps the relations between the original references")]
        public void Classifiers_ConvertToDto_KeepReferences()
        {
            // Arrange

            var classifiers = new ClassifierDictionary(Integer, String);
            var converter = new DomainDtoConverter();

            // Act
            var dtos = converter.ToDto(classifiers).ToArray();

            // ensure that integer classifier is the same reference
            // for the dto and the type of the string's property
            Assert.AreEqual(dtos[0], dtos[1].Properties[0].Type);
            Assert.AreEqual(dtos[1], dtos[0].Properties[0].Type);
        }
    }
}
