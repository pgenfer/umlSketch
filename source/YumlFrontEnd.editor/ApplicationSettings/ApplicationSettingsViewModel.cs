using System.Runtime.CompilerServices;
using Caliburn.Micro;
using UmlSketch.Event;
using UmlSketch.Settings;

namespace UmlSketch.Editor
{
    /// <summary>
    /// Viewmodel that represents the settings
    /// </summary>
    public class ApplicationSettingsViewModel : Screen
    {
        /// <summary>
        /// settings are included as a mixin, so every change to the
        /// view model will be propagated to the internal settings object
        /// directly
        /// </summary>
        private readonly ApplicationSettings _settings;
        /// <summary>
        /// message system is used to fire events whenever a property
        /// has changed so that we can update the diagram
        /// </summary>
        private readonly MessageSystem _messageSystem;

        public ApplicationSettingsViewModel(
            ApplicationSettings settings,
            MessageSystem messageSystem)
        {
            _settings = settings;
            _messageSystem = messageSystem;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            DisplayName = EditorStrings.ApplicationSettings;
        }

        public bool AskBeforeDelete
        {
            get { return _settings.AskBeforeDelete; }
            set { _settings.AskBeforeDelete = value;Save(); }
        }

        public string YumlBaseUrl
        {
            get { return _settings.YumlBaseUrl; }
            set { _settings.YumlBaseUrl = value; Save(); UpdateDiagram(); }
        }

        public DiagramSize DiagramSize
        {
            get { return _settings.DiagramSize; }
            set { _settings.DiagramSize = value; Save(); UpdateDiagram(); }
        }

        public DiagramDirection DiagramDirection
        {
            get { return _settings.DiagramDirection; }
            set { _settings.DiagramDirection = value; Save();UpdateDiagram(); }
        }

        /// <summary>
        /// save will be called whenever a property is changed by the user
        /// </summary>
        private void Save() => _settings.Save();

        private void UpdateDiagram([CallerMemberName] string settingName = null) =>
            _messageSystem.Publish(this, new DiagramSettingChangedEvent(settingName));

        public void Ok() => TryClose();
    }
}
