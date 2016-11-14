using NUnit.Framework;
using System.Collections.Generic;
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
        /// <summary>
        /// representation of a car
        /// </summary>
        protected readonly Classifier Car = new Classifier("Car");
        /// <summary>
        /// representation of a car's wheel
        /// </summary>
        protected readonly Classifier Wheel = new Classifier("Wheel");
        /// <summary>
        /// Vehicle is the base class of a Car
        /// </summary>
        protected readonly Classifier Vehicle = new Classifier("Vehicle");

        // some default data types
        internal readonly ClassifierDto StringDto = new ClassifierDto{Name = "string",IsSystemType = true};
        internal readonly ClassifierDto IntegerDto = new ClassifierDto { Name = "int", IsSystemType = true };
        internal readonly ClassifierDto VoidDto = new ClassifierDto { Name = "void", IsSystemType = true };
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
        protected override void Init() { }
        
        
        /// <summary>
        /// setup the classifiers that can be used during tests
        /// </summary>
        private void InitClassifiers()
        {
            // string has a length property
            String.CreateProperty("Length", Integer,false);
            // integer has a property for its type name
            Integer.CreateProperty("TypeName", String,false);

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
            CarDto.Associations = new List<RelationDto> {CarHasTiresDto};
        }

        private void InitRelationDtos()
        {
            CarHasTiresDto.Start = CarDto;
            CarHasTiresDto.End = TireDto;
            // a car would only have a limited number of tires, but we assume there can be unlimited tires
            CarHasTiresDto.EndMultiplicity = Multiplicity.ZeroToMany;
            // the tires exist after the car was destroyed (might depend on the use case)
            CarHasTiresDto.RelationType = RelationType.Aggregation;
            CarHasTiresDto.Name = "has";
        }

        protected TestBase()
        {
            InitClassifiers();
            InitClassifierDtos();
            InitRelationDtos();
        }
    }
}
