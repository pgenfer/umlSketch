using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// groups all validation name services together.
    /// Simplifies the injection of specific services
    /// </summary>
    public class ValidationServices
    {
        /// <summary>
        /// services used to validate the name of a classifier
        /// </summary>
        public IValidateNameService Classifier { get; }
        /// <summary>
        /// service used to validate the name of a property
        /// </summary>
        public IValidateNameService Property { get; }

        public ValidationServices(
            IValidateNameService classifier = null,
            IValidateNameService property = null)
        {
            Classifier = classifier;
            Property = property;
        }
    }
}
