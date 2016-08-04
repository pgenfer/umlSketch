using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.TypeLoaders
{
    /// <summary>
    /// loads an architecture from reflection meta data.
    /// </summary>
    public class ReflectionTypeLoader
    {
        private readonly ClassifierDictionary _classifiers;

        public ReflectionTypeLoader(ClassifierDictionary classifiers)
        {
            Requires(classifiers != null);

            _classifiers = classifiers;
        }

        /// <summary>
        /// loads meta data from a type and stores them
        /// in a classifier object
        /// </summary>
        /// <param name="type">type from which a classifier should be created</param>
        public Classifier LoadType(Type type)
        {
            Requires(type != null);
            
            // type was not read before
            var classifier = _classifiers.FindByName(type.Name);
            if (classifier == null)
            {
                // create the new type and set its meta data
                classifier = _classifiers.CreateNewClass(type.Name);
                classifier.IsInterface = type.IsInterface;
                foreach(var reflectionProperty in type.GetProperties())
                {
                    var propertyType = LoadType(reflectionProperty.PropertyType);
                    classifier.CreateProperty(reflectionProperty.Name, propertyType);
                }
                // TODO: load methods

                // TODO: system types should already be predefined (map to correct names somehow)
                // maybe the classifier dictionary could contains another dictionary with system types?
                // TODO: system types should not be accessible through the classifier dictionary
            }
            return classifier;
        }
    }
}
