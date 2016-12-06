using System.Windows.Media;
using Caliburn.Micro;
using UmlSketch.Command;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    /// <summary>
    /// mixin that is used to handle
    /// the interaction with the background color of a domain object
    /// (currently classifiers and notes)
    /// </summary>
    public class BackgroundColorMixin : PropertyChangedBase
    {
        public DiagramColorPalette ColorPalette { get;}
        private readonly IChangeColorCommand _command;
        private Color _backgroundColor;

        public BackgroundColorMixin(
            IChangeColorCommand command,
            DiagramColorPalette colorPalette)
        {
            ColorPalette = colorPalette;
            _command = command;
        }

        /// <summary>
        /// only used for initialization, will be used set initially during model => viewmodel mapping.
        /// If mapping would set Backgroundcolor directly, the command would be executed.
        /// </summary>
        public Color InitialColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// background color used for this domain object
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _command.ChangeColor(value.ToFriendlyName());
                NotifyOfPropertyChange();
            }
        }
    }
}
