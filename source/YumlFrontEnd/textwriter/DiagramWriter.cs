using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.metadata;

namespace yuml.textwriter
{
    public class DiagramOptions
    {
        public DiagramOptions(bool showAttributes = false,bool showProperties = true,bool showMethods = true)
        {
            ShowAttributes = showAttributes;
            ShowProperties = showProperties;
            ShowMethods = showMethods;
        }

        public static DiagramOptions OnlyClasses => new DiagramOptions(false, false, false);

        public bool ShowAttributes { get; set; }
        public bool ShowProperties { get; set; }
        public bool ShowMethods { get; set; }
    }

    public class DiagramWriter
    {
        private DiagramOptions _options = new DiagramOptions();

        public DiagramWriter(DiagramOptions options = null)
        {
            if (options != null) _options = options;
        }

        public const string Address = @"http://yuml.me/diagram/scruffy/class/";

        public string Write(Class @class)
        {
            return $"[{@class.Name}]";
        }

        public string Write(Association association)
        {
            var first = association.FirstConnection;
            var second = association.SecondConnection;

            var result =
                $"{Write(first.Class)}{(first.HasDirection ? "<" : string.Empty)}{first.Name}-{second.Name}{(second.HasDirection ? ">" : string.Empty)}{Write(second.Class)}";
            return result;
        }

        public string WriteDiagramText(Diagram diagram)
        {
            var classNames = string.Join(",", diagram.Classes.Select(Write));
            var associations = string.Join(",", diagram.Associations.Select(Write));
            var content = string.Join(",", new[] { classNames, associations });
            var result = $"{Address}{content}";
            return result;
        }
    }
}
