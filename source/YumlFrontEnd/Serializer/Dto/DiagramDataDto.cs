using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// diagram data combines
    /// all information that are stored
    /// for a diagram.
    /// These are currently the classifiers
    /// later we will store additional diagram data here
    /// (like notes etc...)
    /// </summary>
    internal class DiagramDataDto
    {
        /// <summary>
        /// list of classifiers which should
        /// be serialized
        /// </summary>
        public List<ClassifierDto> Classifiers { get; set; }
    }
}
