using System.Windows;
using UmlSketch.Service;
using UmlSketch.Settings;

namespace UmlSketch.Editor
{
    public class AskUserBeforeDeletionService : IAskUserBeforeDeletionService
    {
        private readonly ApplicationSettings _settings;
        public AskUserBeforeDeletionService(ApplicationSettings settings)
        {
            _settings = settings;
        }

        public bool ShouldDomainObjectBeDeleted(string message)
        {
            // if flag is not set, user can always delete without restriction
            if (!_settings.AskBeforeDelete)
                return true;
            var messageBoxResult = MessageBox.Show(
                message,
                EditorStrings.ConfirmDelete,
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            return messageBoxResult == MessageBoxResult.Yes;
        }
    }
}
