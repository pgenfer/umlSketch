using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Serializer.Dto;

namespace Yuml.Test
{
    [TestFixture]
    public class TestBase
    {
        /// <summary>
        /// dummy classifier that represents an integer
        /// </summary>
        protected readonly Classifier Integer = new Classifier("int");
        /// <summary>
        /// dummy classifier that represents a string
        /// </summary>
        protected readonly Classifier String = new Classifier("string");

        internal readonly ClassifierDto StringDto = new ClassifierDto{Name = "string"};
        internal readonly ClassifierDto IntegerDto = new ClassifierDto { Name = "int" };

        /// <summary>
        /// setup the classifiers that can be used during tests
        /// </summary>
        private void InitClassifiers()
        {
            // string has a length property
            String.CreateProperty("Length", Integer);
            // integer has a property for its type name
            Integer.CreateProperty("TypeName", String);
        }

        private void InitClassifierDtos()
        {
            StringDto.Properties = new List<PropertyDto>
            {
                new PropertyDto { Name = "Length", Type = IntegerDto }
            };
            IntegerDto.Properties = new List<PropertyDto>
            {
                new PropertyDto { Name = "TypeName", Type = StringDto }
            };
        }

        protected TestBase()
        {
            InitClassifiers();
            InitClassifierDtos();
        }
    }
}
