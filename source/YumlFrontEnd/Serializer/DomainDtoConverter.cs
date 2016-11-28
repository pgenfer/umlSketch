using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.DomainObject;

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
                x.CreateMap<Diagram, DiagramDataDto>()
                    .PreserveReferences()
                    .ReverseMap()
                    .ForMember(d => d.Note, c => c.Ignore())
                    .ForMember(d => d.Classifiers, c => c.Ignore())
                    .AfterMap((s, d) => // load classifiers from dto to diagram
                    {
                        foreach (var classifierDto in s.Classifiers)
                            d.Classifiers.AddNewClassifier(_mapper.Map<Classifier>(classifierDto));
                        d.Classifiers.AddMissingSystemTypes();
                        d.Note.Color = s.NoteColor;
                        d.Note.Text = s.NoteText;
                    });
                x.CreateMap<DiagramDataDto, Note>() // map diagram dto to note
                    .ForMember(d => d.Text, c => c.MapFrom(s => s.NoteText))
                    .ForMember(d => d.Color, c => c.MapFrom(s => s.NoteColor));
                // classifier mapping
                x.CreateMap<Classifier, ClassifierDto>().PreserveReferences().ReverseMap()
                    // this is a way of unflattening the note DTO data back to the classifier
                    // see here for details:
                    // http://stackoverflow.com/questions/3145062/using-automapper-to-unflatten-a-dto
                    .ForMember(d => d.Note,c => c.MapFrom(s => s)) // map note to classifierDto
                    .PreserveReferences();
                x.CreateMap<ClassifierDto, Note>() // and set properties from classifierDto to note
                    .ForMember(d => d.Text, c => c.MapFrom(s => s.NoteText))
                    .ForMember(d => d.Color, c => c.MapFrom(s => s.NoteColor));
                // property mapping
                x.CreateMap<Property, PropertyDto>().PreserveReferences().ReverseMap().PreserveReferences();
                x.CreateMap<List<PropertyDto>, PropertyList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var property in s)
                            d.AddExistingMember(_mapper.Map<Property>(property));
                    });
                // method mapping
                x.CreateMap<Method, MethodDto>().PreserveReferences().ReverseMap().PreserveReferences();
                x.CreateMap<List<MethodDto>, MethodList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var member in s)
                            d.AddExistingMember(_mapper.Map<Method>(member));
                    });
                // parameter mapping
                x.CreateMap<Parameter, ParameterDto>().PreserveReferences().ReverseMap().PreserveReferences();
                x.CreateMap<List<ParameterDto>, ParameterList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var parameter in s)
                            d.AddExistingMember(_mapper.Map<Parameter>(parameter));
                    });
                // mapping from relation => DTO
                x.CreateMap<Relation, RelationDto>().PreserveReferences()
                    .ForMember(d => d.Start, c =>
                    {
                        c.MapFrom(s => _mapper.Map<ClassifierDto>(s.Start.Classifier));
                    })
                    .ForMember(d => d.End, c =>
                    {
                        c.MapFrom(s => _mapper.Map<ClassifierDto>(s.End.Classifier));
                    })
                    .ForMember(d => d.RelationType, c => c.MapFrom(s => s.Type));
                // mapping for implementations works like relation mapping
                x.CreateMap<Implementation, ImplementationDto>().PreserveReferences()
                    .IncludeBase<Relation,RelationDto>();
                x.CreateMap<Association, AssociationDto>().PreserveReferences()
                    .IncludeBase<Relation, RelationDto>();
                
                // mapping from DTO => relation
                x.CreateMap<RelationDto,Relation>().PreserveReferences()
                    .ForMember(d => d.Start, c =>
                    {
                        c.MapFrom(s => new StartNode(
                            _mapper.Map<Classifier>(s.Start),
                            string.Empty,
                            s.BiDirectional));
                    })
                    .ForMember(d => d.End, c =>
                    {
                        c.MapFrom(s => new EndNode(
                            _mapper.Map<Classifier>(s.End),
                            string.Empty,
                            true));
                    })
                    .ForMember(d => d.Type,c => c.MapFrom(s => s.RelationType));
                // mapping for implementation dtos
                x.CreateMap<ImplementationDto, Implementation>().PreserveReferences()
                    .IncludeBase<RelationDto, Relation>();
                x.CreateMap<List<ImplementationDto>, ImplementationList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var implementationDto in s)
                            d.AddInterfaceToList(_mapper.Map<Implementation>(implementationDto));
                    });
                // mapping for associations
                x.CreateMap<AssociationDto, Association>().PreserveReferences()
                    .IncludeBase<RelationDto, Relation>();
                x.CreateMap<List<AssociationDto>, ClassifierAssociationList>().PreserveReferences()
                    .AfterMap((s, d) =>
                    {
                        foreach (var associationDto in s)
                            d.AddExistingRelation(_mapper.Map<Association>(associationDto));
                    });
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// converts the list of classifier DTOs back to a classifier dictionary
        /// </summary>
        public void ToDomain(Diagram diagram, DiagramDataDto diagramDto)
        {
            // remove all existing classifiers before adding new ones
            diagram.Classifiers.Clear();
            _mapper.Map(diagramDto,diagram);
        }

        public DiagramDataDto ToDto(Diagram diagram) => _mapper.Map<DiagramDataDto>(diagram);
    }
}
