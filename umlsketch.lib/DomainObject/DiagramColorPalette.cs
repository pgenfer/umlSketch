using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// returns all colors that are currently
    /// available in the diagram
    /// </summary>
    public class DiagramColorPalette
    {
        private readonly Diagram _diagram;

        public DiagramColorPalette(Diagram diagram)
        {
            _diagram = diagram;
        }

        public IEnumerable<string> CollectColorsUsedInDiagram()
        {
            var colors = _diagram.Classifiers
                .Select(x => x.Color)
                .Concat(_diagram.Classifiers.Select(x => x.Note.Color))
                .Concat(new[] {_diagram.Note.Color})
                .Distinct()
                .Where(x => !string.IsNullOrEmpty(x)); // skip transparent colors
            return colors;
        }
    }
}
