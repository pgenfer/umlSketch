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

        public RelationWriter(DiagramContentMixin content)
        {
            _content = content;
        }

        public StartNodeWriter WithStartNode(string classifier) => new StartNodeWriter(classifier,_content);
        public DiagramWriter Finish()
        {
            _content.AppendToken(",");
            return new DiagramWriter(_content);
        }
    }
}
