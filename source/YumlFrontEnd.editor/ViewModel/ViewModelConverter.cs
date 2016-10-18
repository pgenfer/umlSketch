using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AutoMapper;
using Yuml;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// converter class used to init viewmodels out of
    /// domain objects
    /// </summary>
    internal class ViewModelConverter
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
            // store the classfier mappings so that we can resolve them later
            _mapperConfiguration = new MapperConfiguration(x =>
            {
                // classifier mapping
                x.CreateMap<Classifier, ClassifierViewModel>()
                    // map base class of classifier to base class name or empty string if no base class available
                    .ForMember(
                        d => d.InitialBaseClass, 
                        c => c.MapFrom(s => s.BaseClass != null ? s.BaseClass.Name : string.Empty))
                    .ForMember(d => d.InitialColor,c => c.MapFrom(s => s.Color.ToColorFromFriendlyName()))
                    // other view models will be created explicitly and not via mapping
                    .ForMember( d => d.Properties, c => c.Ignore())
                    .ForMember( d => d.Methods, c => c.Ignore())
                    .ForMember(d => d.Associations, c => c.Ignore());
                x.CreateMap<Property, PropertyViewModel>()
                    .ForMember(d => d.InitialPropertyType, c => c.MapFrom(s => s.Type.Name));
                x.CreateMap<Method, MethodViewModel>();
                x.CreateMap<Relation, AssociationViewModel>()
                    .ForMember(d => d.Name, c => c.MapFrom(s => s.Start.Name))
                    .ForMember(d => d.InitialAssociationType, c => c.MapFrom(s => s.Type))
                    .ForMember(d => d.InitialTargetClassiferName, c => c.MapFrom(s => s.End.Classifier.Name));
            });

            _mapper = _mapperConfiguration.CreateMapper();
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
    }
}
