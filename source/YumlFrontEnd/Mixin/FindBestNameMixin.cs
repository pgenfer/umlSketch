using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace UmlSketch.Mixin
{
    /// <summary>
    /// used to find the best name of for an item within a given list.
    /// The proposed name can be provided and the mixin checks the for the correct 
    /// enumeration.
    /// Example: if the name is "New Classifier", the method would
    /// return "New Classifier 1" or "New Classifier 2" depending on how many
    /// items with the same name are in the list
    /// </summary>
    public class FindBestNameMixin
    {
        private readonly IEnumerable<INamed> _namedObjects;
        private readonly Regex _findLastNumber = new Regex(@"(\d+)(?!.*\d)", RegexOptions.Compiled);

        public FindBestNameMixin(IEnumerable<INamed> namedObjects )
        {
            _namedObjects = namedObjects;
        }

        public string FindBestName(string defaultName)
        {
            var newName = string.Empty;

            Contract.Requires(!string.IsNullOrEmpty(defaultName));
            Contract.Ensures(_namedObjects.All(x => x.Name != newName));

            var defaulMemberNames = _namedObjects
                .Where(x => x.Name.StartsWith(defaultName))
                .Select(x => x.Name);
            var highestNumber = 0;
            foreach (var name in defaulMemberNames)
            {
                var match = _findLastNumber.Match(name);
                if (match.Success)
                    highestNumber = int.Parse(match.Groups[1].ToString());
            }
            newName = $"{defaultName} {++highestNumber}";
            return newName;
        }
    }
}
