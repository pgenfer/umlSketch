using System.Text;

namespace Yuml
{
    /// <summary>
    /// mixin that is used to hold the internal content of
    /// a diagram writer. The mixin will be shared between several
    /// diagram writer types.
    /// </summary>
    public class DiagramContentMixin
    {
        protected readonly StringBuilder _string = new StringBuilder();
        /// <summary>
        /// there must always be a space between two identifiers,
        /// so we try to remember what item was added the last time
        /// </summary>
        private bool _lastItemWasIdentifier = false;

        public void AppendIdentifier(string identifier)
        {
            if (_lastItemWasIdentifier)
                _string.Append(" "); // always add a space between two identifiers
            _string.Append($"{identifier}");
            _lastItemWasIdentifier = true;
        }
        public void AppendToken(string token)
        {
            _string.Append($"{token}");
            _lastItemWasIdentifier = false;
        }

        override public string ToString() => _string.ToString();
    }
}