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
    /// and their relations, both stored in
    /// separate lists
    /// </summary>
    internal class DiagramDataDto
    {
        /// <summary>
        /// list of classifiers which should
        /// be serialized
        /// </summary>
        public List<ClassifierDto> Classifiers { get; set; }
        /// <summary>
        /// list of relations. Every relation
        /// has also a dependency to a classifier,
        /// that's why they must be stored together.
        /// </summary>
        public List<RelationDto> Relations { get; set; }
    }
}
