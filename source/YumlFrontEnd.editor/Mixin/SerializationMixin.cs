using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Yuml;
using Yuml.Serializer;
using static System.Environment;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Mixin that handles that loads and stores data
    /// from and to a persistant storage. 
    /// OS specific dialogs like MessageBox or FileRequester
    /// are currently hardcoded but could be replaced by a service later.
    /// The serialization works on the given ClassifierDictionary and 
    /// a Reset event will be fired on the ClassifierDictionary after
    /// deserialization was completed.
    /// </summary>
    public class SerializationMixin
    {
        private FileName _fileName;
        private readonly Diagram _diagram;
        private readonly MessageSystem _messageSystem;
        private readonly ApplicationSettings _applicationSettings;

        public SerializationMixin(
            Diagram diagram,
            MessageSystem messageSystem,
            ApplicationSettings applicationSettings)
        {
            _diagram = diagram;
            _messageSystem = messageSystem;
            _applicationSettings = applicationSettings;
            _fileName = applicationSettings.LastFile;
        }

        private void StoreProjectFileName()
        {
            // update the application settings
            _applicationSettings.LastFile = _fileName;
            _applicationSettings.Save();
        }

        public void New()
        {
            _fileName = null;
            _diagram.Reset();
            _messageSystem.Publish(_diagram.Classifiers, new ClassifiersResetEvent());
        }

        public void Save()
        {
            if (_fileName == null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = EditorStrings.UmlJsonFileFilter,
                    InitialDirectory = GetFolderPath(SpecialFolder.MyDocuments)
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    _fileName = new FileName(saveFileDialog.FileName);
                    StoreProjectFileName();
                }
            }

            // user has not choosen any file, so skip here
            if (_fileName == null)
                return;

            var jsonContent = new JsonSerializer().Save(_diagram);
            try
            {
                System.IO.File.WriteAllText(
                    _fileName.ToString(),
                    jsonContent.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(EditorStrings.SaveFileError, ex.Message),
                    EditorStrings.Save,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void LoadLastFile()
        {
            if (_fileName?.IsValid == true)
            {
                try
                {
                    LoadFile();
                }
                catch (Exception)
                {
                    // error while trying to load last data,
                    // so reset everything
                    New();
                }
            }
            else
            {
                // no file to load, so start with a new file
                New();
            }

        }

        private void LoadFile()
        {
            // load content from json source
            var content = System.IO.File.ReadAllText(_fileName.Value);
            var jsonContent = new JsonContent(content);
            new JsonSerializer().Load(jsonContent, _diagram);
            // fire event that view models have updated
            _messageSystem.Publish(_diagram.Classifiers, new ClassifiersResetEvent());
        }

        public void Open()
        {
            var path = _fileName?.Path;
            // use the path of the last file or use default path
            path = System.IO.Directory.Exists(path) ? path : GetFolderPath(SpecialFolder.MyDocuments);

            var openFileDialog = new OpenFileDialog
            {
                Filter = EditorStrings.UmlJsonFileFilter,
                InitialDirectory = path
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _fileName = new FileName(openFileDialog.FileName);
                try
                {
                    LoadFile();
                    // update the application settings
                    StoreProjectFileName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format(EditorStrings.OpenFileError, ex.Message),
                        EditorStrings.Open,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}
