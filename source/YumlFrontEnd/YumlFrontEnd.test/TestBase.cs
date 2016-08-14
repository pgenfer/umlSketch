using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Serializer.Dto;

namespace Yuml.Test
{

    /// <summary>
    /// test base class that already contains
    /// some test dummy data that can be used by test cases
    /// </summary>
    public class TestBase : SimpleTestBase
    {
        /// <summary>
        /// dummy classifier that represents an integer
        /// </summary>
        protected readonly Classifier Integer = new Classifier("int");
        /// <summary>
        /// dummy classifier that represents a string
        /// </summary>
        protected readonly Classifier String = new Classifier("string");
        /// <summary>
        /// void data type
        /// </summary>
        protected readonly Classifier Void = new Classifier("void");
        /// <summary>
        /// a classifier that has a method
        /// </summary>
        protected readonly Classifier Service = new Classifier("Service");

        // some default data types
        internal readonly ClassifierDto StringDto = new ClassifierDto{Name = "string"};
        internal readonly ClassifierDto IntegerDto = new ClassifierDto { Name = "int" };
        internal readonly ClassifierDto VoidDto = new ClassifierDto { Name = "void" };
        // some classifiers used for relations
        internal readonly ClassifierDto CarDto = new ClassifierDto { Name = "Car" };
        internal readonly ClassifierDto TireDto = new ClassifierDto { Name = "Tyre" };
        // relations
        internal readonly RelationDto CarHasTiresDto = new RelationDto();

        /// <summary>
        /// a dto to a service class that provides some methods
        /// </summary>
        internal readonly ClassifierDto ServiceDto = new ClassifierDto { Name = "Service" };

        [SetUp]
        public void SetupTestCase()
        {
            Init();
        }

        /// <summary>
        /// method can be overridden to provide test initialisation code
        /// </summary>
        protected virtual void Init() { }
        
        
        /// <summary>
        /// setup the classifiers that can be used during tests
        /// </summary>
        private void InitClassifiers()
        {
            // string has a length property
            String.CreateProperty("Length", Integer);
            // integer has a property for its type name
            Integer.CreateProperty("TypeName", String);

            Service.CreateMethod("DoSomething", Void)
                .CreateParameter(String, "firstParameter");
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
            ServiceDto.Methods = new List<MethodDto>
            {
                new MethodDto
                {
                    Name = "ServiceMethod",
                    ReturnType = VoidDto,
                    Parameters = new List<ParameterDto>
                    {
                        new ParameterDto {Name = "args",Type = StringDto }
                    }
                }
            };
        }

        private void InitRelationDtos()
        {
            CarHasTiresDto.Start = CarDto;
            CarHasTiresDto.End = TireDto;
            // a car would only have a limited number of tires, but we assume there can be unlimited tires
            CarHasTiresDto.EndMultiplicity = Multiplicity.ZeroToMany;
            // the tires exist after the car was destroyed (might depend on the use case)
            CarHasTiresDto.Relation = Relation.Aggregation;
            CarHasTiresDto.Name = "has";
        }

        protected TestBase()
        {
            InitClassifiers();
            InitClassifierDtos();
            InitRelationDtos();
        }

        
    }

    /// <summary>
    /// base class that contains common helper functions
    /// used by other test cases
    /// </summary>
    [TestFixture]
    public class SimpleTestBase
    {
        private const int RandomStringLength = 10;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomString(int? seed = null) =>
            new string(
                Enumerable.Repeat(Alphabet, RandomStringLength)
                .Select(s => s[new Random(seed ?? (int)DateTime.Now.Ticks).Next(s.Length)])
                .ToArray());
    }
}
