using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// Starting point for rendering a relation between two classes.
    /// A relation is constructed of the following parts:
    /// [StartNode][RelationEnd][EndNode]
    /// </summary>
    public class RelationWriter
    {
        private readonly DiagramContentMixin _content;

        /// <summary>
        /// creates a new relation writer 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="checkForPreviousContent">
        /// if true, check if an additional separator must be added before creating
        /// the next relation. This is the case if there are already other elements before
        /// this entry (e.g. classifiers)</param>
        public RelationWriter(DiagramContentMixin content, bool checkForPreviousContent = false)
        {
            _content = content;
            // if there are entries before this relation,
            // add an additional separator
            if (checkForPreviousContent && !_content.IsEmpty)
                _content.AppendToken(",");
        }

        public StartNodeWriter WithStartNode(string classifier) => new StartNodeWriter(classifier,_content);
        public DiagramWriter Finish(bool lastEntry = true)
        {
            if(!lastEntry)
                _content.AppendToken(",");
            return new DiagramWriter(_content);
        }
    }
}
