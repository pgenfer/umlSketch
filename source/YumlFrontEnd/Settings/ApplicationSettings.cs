using System;

namespace Yuml
{
    /// <summary>
    /// User specific settings that are used by the application.
    /// The settings are defined as value type because
    /// when using the settings we will never need object identiy,
    /// only the current values of the settings will be important.
    /// </summary>
    public struct ApplicationSettings
    {
        /// <summary>
        /// default path where the Yuml.me service should be located.
        /// </summary>
        private const string DefaultUrl = @"http://yuml.me/diagram/scruffy/class/";

        public ApplicationSettings(
            string filePath = null,
            bool askBeforeDelete = true,
            string yumlUrl = DefaultUrl,
            DiagramSize size = DiagramSize.Big,
            DiagramDirection direction = DiagramDirection.TopDown)
        {
            FilePath = filePath ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AskBeforeDelete = askBeforeDelete;
            YumlUrl = yumlUrl;
            DiagramSize = size;
            DiagramDirection = direction;
        }

        /// <summary>
        /// the path where the application stores
        /// its project files
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// if flag is set, the application will always
        /// ask before any domain entities (like classes, properties etc...)
        /// will be deleted
        /// </summary>
        public bool AskBeforeDelete { get; set; }
        /// <summary>
        /// the url where the Yuml.me web service is located
        /// </summary>
        public string YumlUrl { get; set; }
        /// <summary>
        /// predefined size of the diagram
        /// </summary>
        public DiagramSize DiagramSize { get; set; }
        /// <summary>
        /// predefined direction of the diagram
        /// </summary>
        public DiagramDirection DiagramDirection { get; set; }
    }
}
