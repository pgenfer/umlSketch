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
            Dictionary<Classifier, ClassifierDto> classifierMappings = new Dictionary<Classifier, ClassifierDto>();
            _mapperConfiguration = new MapperConfiguration(x =>
            {
                x.CreateMap<Classifier, ClassifierDto>()
                .PreserveReferences(); // needed, otherwise properties will not reference existing classifiers
                x.CreateMap<Property, PropertyDto>()
                .PreserveReferences(); // needed, otherwise properties will not reference existing classifiers
                x.CreateMap<ClassifierDto, Classifier>()
                .PreserveReferences();
                x.CreateMap<PropertyDto, Property>()
                .PreserveReferences();
                x.CreateMap<List<PropertyDto>, PropertyList>()
                .PreserveReferences()
                .AfterMap((s,d) =>
                {
                    // map the property DTOs to correct property domain objects
                    // and add them  to the property list
                    foreach (var propertyDto in s)
                        d.AddExistingProperty(_mapper.Map<Property>(propertyDto));                        
                });                
            });

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
        /// <param name="classifierDtos"></param>
        /// <returns></returns>
        public ClassifierDictionary ToDomain(IEnumerable<ClassifierDto> classifierDtos)
        {
            return new ClassifierDictionary(_mapper.Map<IEnumerable<Classifier>>(classifierDtos));
        }
    }
}
