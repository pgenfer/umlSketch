using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yuml;
using Yuml.Service;

namespace YumlFrontEnd.editor
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
