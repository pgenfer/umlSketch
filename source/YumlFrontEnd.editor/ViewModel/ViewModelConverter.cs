using AutoMapper;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    /// <summary>
    /// converter class used to init viewmodels out of
    /// domain objects
    /// </summary>
    public class ViewModelConverter
    {
        /// <summary>
        /// mapping configuration
        /// </summary>
        private MapperConfiguration _mapperConfiguration;
        /// <summary>
        /// Mapper used to map between domain and view model objects
        /// </summary>
        private IMapper _mapper;

        public ViewModelConverter()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            var noteResolver = new NoteResolver();

            // store the classfier mappings so that we can resolve them later
            _mapperConfiguration = new MapperConfiguration(x =>
            {
                // classifier mapping
                x.CreateMap<Classifier, ClassifierViewModel>()
                    // map base class of classifier to base class name or empty string if no base class available
                    .ForMember(
                        d => d.InitialBaseClass,
                        c => c.MapFrom(s => s.BaseClass != null ? s.BaseClass.Name : string.Empty))
                    .ForMember(d => d.InitialColor, c => c.MapFrom(s => s.Color.ToColorFromFriendlyName()))
                    // other view models will be created explicitly and not via mapping
                    .ForMember(d => d.Properties, c => c.Ignore())
                    .ForMember(d => d.Methods, c => c.Ignore())
                    .ForMember(d => d.Associations, c => c.Ignore())
                    .ForMember(d => d.Interfaces, c => c.Ignore())
                    .ForMember(d => d.IsInterface,c => c.Ignore()) // don't set, otherwise command will be executed
                    .ForMember(d => d.IsInterfaceInitial,c => c.MapFrom(s => s.IsInterface))
                    .ForMember(d => d.Note, c => c.ResolveUsing(noteResolver));
               // store note information
                x.CreateMap<Note, NoteViewModel>()
                    .ForMember(d => d.Text, c => c.MapFrom(s => s.Text))
                    .ForMember(d => d.InitialColor, c => c.MapFrom(s => s.Color.ToColorFromFriendlyName()));
                x.CreateMap<Property, PropertyViewModel>()
                    .ForMember(d => d.InitialPropertyType, c => c.MapFrom(s => s.Type.Name));
                x.CreateMap<Parameter, ParameterViewModel>()
                    .ForMember(d => d.InitialParameterType, c => c.MapFrom(s => s.Type.Name));
                x.CreateMap<Method, MethodViewModel>()
                    .ForMember(d => d.InitialReturnType, c => c.MapFrom(s => s.ReturnType.Name))
                    .ForMember(d => d.Parameters, c => c.Ignore());
                x.CreateMap<Relation, AssociationViewModel>()
                    .ForMember(d => d.Name, c => c.MapFrom(s => s.Start.Name))
                    .ForMember(d => d.InitialAssociationType, c => c.MapFrom(s => s.Type))
                    .ForMember(d => d.InitialTargetClassiferName, c => c.MapFrom(s => s.End.Classifier.Name));
                x.CreateMap<Implementation, InterfaceImplementationViewModel>()
                    .ForMember(d => d.InitialInterfaceImplementation, c => c.MapFrom(s => s.End.Classifier.Name));
            });

            _mapper = _mapperConfiguration.CreateMapper();
            // set mapper to note resolver so note and noteview model can be mapped correctly
            noteResolver.Mapper = _mapper;
        }

        /// <summary>
        /// used to initialize the view model with the data
        /// from the domain object.
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public TViewModel InitViewModel<TDomain, TViewModel>(TDomain source, TViewModel destination) =>
            _mapper.Map(source, destination);


        /// <summary>
        /// The classifier view model already has an instance of the note view model, we just want to fill this instance
        /// during mapping and don't want to recreate a new note view model. Therefore this resolver ensures
        /// that the existing note view model will be mapped. See here for details:
        /// http://stackoverflow.com/questions/3672447/how-do-you-map-a-dto-to-an-existing-object-instance-with-nested-objects-using-au
        /// </summary>
        internal class NoteResolver : IValueResolver<Classifier, ClassifierViewModel, NoteViewModel>
        {
            /// <summary>
            /// mapper will be set after initialization
            /// </summary>
            public IMapper Mapper { get; internal set; }

            public NoteViewModel Resolve(
                Classifier source, 
                ClassifierViewModel destination, 
                NoteViewModel destMember,
                ResolutionContext context) => Mapper.Map(source.Note, destMember);
        }
    }
}
