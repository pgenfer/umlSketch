namespace UmlSketch.Validation
{

    /// <summary>
    /// base class for the results returned by a validation operation
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// in case an error occured, the error message
        /// is stored here
        /// </summary>
        public string Message { get; }        
        /// <summary>
        /// result of the validation, can either be
        /// Success or Error
        /// </summary>
        public Validation Result { get;}
        /// <summary>
        /// shortcut for checking if this validation was successful or not
        /// </summary>
        public bool HasError => Result == Validation.Error;

        protected ValidationResult(string message, Validation result)
        {
            Message = message;
            Result = result;
        }
    }
}
