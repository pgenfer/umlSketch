namespace UmlSketch.Validation
{
    /// <summary>
    /// validation result object in case the
    /// validation was not successful
    /// </summary>
    internal class Error : ValidationResult
    {
        public Error(string message) : base(message, Validation.Error)
        {
        }
    }
}
