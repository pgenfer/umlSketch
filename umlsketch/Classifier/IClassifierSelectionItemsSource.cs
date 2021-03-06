using System.Collections.Generic;

namespace UmlSketch.Editor
{
    /// <summary>
    /// interface is used to access the list of available classifiers whenever
    /// a classifier needs to be referenced (e.g. as method return type or as property)
    /// </summary>
    public interface IClassifierSelectionItemsSource : IEnumerable<ClassifierItemViewModel>
    {
        /// <summary>
        /// returns the classifier with the given name.
        /// </summary>
        /// <param name="name">name of the classifier. A classifier
        /// with this name must exist in the list and there may only be one 
        /// classifier with this name</param>
        /// <returns></returns>
        ClassifierItemViewModel ByName(string name);


    }
}