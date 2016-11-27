namespace Yuml
{
    /// <summary>
    /// this is the application data that is serialized.
    /// </summary>
    internal class ApplicationSettingsDataMixin
    {
        private const string BaseUri = @"http://yuml.me/";
    
        public ApplicationSettingsDataMixin(
            string lastFile = null,
            bool askBeforeDelete = true,
            string baseUri = BaseUri,
            DiagramSize size = DiagramSize.Big,
            DiagramDirection direction = DiagramDirection.TopDown)
        {
            LastFile = lastFile;
            AskBeforeDelete = askBeforeDelete;
            YumlBaseUrl = baseUri;
            DiagramSize = size;
            DiagramDirection = direction;
        }

        /// <summary>
        /// last file that was loaded by the application
        /// </summary>
        public string LastFile { get; set; }
        /// <summary>
        /// if flag is set, the application will always
        /// ask before any domain entities (like classes, properties etc...)
        /// will be deleted
        /// </summary>
        public bool AskBeforeDelete { get; set; }
        /// <summary>
        /// the base address used to access all Yumle services.
        /// </summary>
        public string YumlBaseUrl { get; set; }
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