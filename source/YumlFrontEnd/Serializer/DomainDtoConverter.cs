using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// Converter used to convert between domain objects and their DTOs.
    /// Can be used to serialize the domain objects to and from a source (like a json file or a DB).
    /// The mapping between domain and DTOs is managed by AutoMapper.
    /// </summary>
    internal class DomainDtoConverter
    {
        /// <summary>
        /// mapping configuration
        /// </summary>
        private MapperConfiguration _mapperConfiguration;
        /// <summary>
        /// Mapper used to map between domain and DTO objects
        /// </summary>
        private IMapper _mapper;

        public DomainDtoConverter()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            // store the classfier mappings so that we can resolve them later
            _mapperConfiguration = new MapperConfiguration(x =>
            {
                // classifier mapping
                x.CreateMap<Classifier, ClassifierDto>()
                    .PreserveReferences()
                    .ReverseMap()
                    .PreserveReferences();
                // property mapping
                x.CreateMap<Property, PropertyDto>()
                    .PreserveReferences()
                    .ReverseMap()
                    .PreserveReferences();
                x.CreateMap<List<PropertyDto>, PropertyList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var property in s)
                            d.AddExistingMember(_mapper.Map<Property>(property));
                    });
                // method mapping
                x.CreateMap<Method, MethodDto>()
                    .PreserveReferences()
                    .ReverseMap()
                    .PreserveReferences();

                x.CreateMap<List<MethodDto>, MethodList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var member in s)
                            d.AddExistingMember(_mapper.Map<Method>(member));
                    });
                // parameter mapping
                x.CreateMap<Parameter, ParameterDto>()
                    .PreserveReferences()
                    .ReverseMap()
                    .PreserveReferences();

                x.CreateMap<List<ParameterDto>, ParameterList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var parameter in s)
                            d.AddExistingMember(_mapper.Map<Parameter>(parameter));
                    });
            });
            // TODO: add mapping for relations

            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// converts the given classifier dictonary to a list of
        /// classifier DTOs
        /// </summary>
        /// <param name="classifiers"></param>
        /// <returns></returns>
        public IEnumerable<ClassifierDto> ToDto(ClassifierDictionary classifiers)
        {
            return _mapper.Map<IEnumerable<ClassifierDto>>(classifiers);
        }

        /// <summary>
        /// converts the list of classifier DTOs back to a classifier dictionary
        /// </summary>
        /// <param name="classifierDtos">DTOs which were loaded from persistant storage</param>
        /// <param name="classifierDictionary">dictionary where the domain objects hydrated from the DTOs should be stored</param>
        /// <returns></returns>
        public void ToDomain(
            IEnumerable<ClassifierDto> classifierDtos,
            ClassifierDictionary classifierDictionary)
        {
            // remove all existing classifiers before adding new ones
            classifierDictionary.Clear();

            var classifiers = _mapper.Map<IEnumerable<Classifier>>(classifierDtos);
            foreach (var classifier in classifiers)
                classifierDictionary.AddNewClassifier(classifier);
            // at the end, add all missing system types
            classifierDictionary.AddMissingSystemTypes();
        }
    }
}
