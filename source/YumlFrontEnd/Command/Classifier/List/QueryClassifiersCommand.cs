using System;
using System.Collections.Generic;

namespace Yuml.Commands
{
    /// <summary>
    /// a generic command that can be used to query the list of available classifiers.
    /// </summary>
    public class QueryClassifiersCommand
    {
        /// <summary>
        /// function that describes the query which will be executed by this command
        /// </summary>
        private readonly Func<IEnumerable<Classifier>> _queryClassifiers;

        internal QueryClassifiersCommand(Func<IEnumerable<Classifier>> queryClassifiers)
        {
            _queryClassifiers = queryClassifiers;
        }

        /// <summary>
        /// executes the query and returns the resulting classifiers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Classifier> Do() => _queryClassifiers();
    }
}