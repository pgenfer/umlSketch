namespace UmlSketch.Validation
{
    /// <summary>
    /// success object in case a validation was successful
    /// </summary>
    internal class Success : ValidationResult
    {
        public Success() : base(string.Empty, Validation.Success)
        {
        }
    }
}
