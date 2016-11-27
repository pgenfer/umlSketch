using System;
using Yuml.Serializer;
using static System.Environment;

namespace Yuml
{
    /// <summary>
    /// User specific settings that are used by the application.
    /// </summary>
    public class ApplicationSettings
    {
        private FileName _settingFilePath;
        private ApplicationSettingsDataMixin _data;

        public ApplicationSettings()
        {
            _data = new ApplicationSettingsDataMixin();
        }
        
        public void Save()
        {
            var serializer = new JsonApplicationSettingSerializer();
            var content = serializer.Save(_data);
            try
            {
                System.IO.File.WriteAllText(_settingFilePath.Value, content.Value);
            }
            catch (Exception)
            {
                // if settings cannot be saved, we cannot do anything but reset the settings
                _data = new ApplicationSettingsDataMixin();
            }
        }

        public void Load(string applicationSettingsPath)
        {
            var settingsFile = new FileName(applicationSettingsPath);
            _settingFilePath = settingsFile;
            // return default settings in case
            // file could not be loaded
            if (!settingsFile.IsValid)
                return;
            try
            {
                var content = System.IO.File.ReadAllText(settingsFile.Value);
                var settings = new JsonApplicationSettingSerializer().Load(new JsonContent(content));
                // reset the newly loaded settings
                _data = settings;
            }
            catch (Exception)
            {
                // we could not load the settings, so return
                // default settings
                _data = new ApplicationSettingsDataMixin(_settingFilePath.Value);
            }
        }

        public bool AskBeforeDelete
        {
            get { return _data.AskBeforeDelete; }
            set { _data.AskBeforeDelete = value; }
        }

        public string YumlBaseUrl
        {
            get { return _data.YumlBaseUrl; }
            set { _data.YumlBaseUrl = value; }
        }

        public DiagramSize DiagramSize
        {
            get { return _data.DiagramSize; }
            set { _data.DiagramSize = value; }
        }

        public DiagramDirection DiagramDirection
        {
            get { return _data.DiagramDirection; }
            set { _data.DiagramDirection = value; }
        }

        /// <summary>
        /// last file that was loaded by the application
        /// </summary>
        public FileName LastFile
        {
            get { return new FileName(_data.LastFile); }
            set { _data.LastFile = value?.Value; }
        }

    }
}
