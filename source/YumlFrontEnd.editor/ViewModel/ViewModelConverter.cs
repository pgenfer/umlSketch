using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    .ForMember( d => d.Properties, c => c.Ignore())
                    .ForMember( d => d.Methods, c => c.Ignore());
                x.CreateMap<Property, PropertyViewModel>();
                x.CreateMap<Method, MethodViewModel>();
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
