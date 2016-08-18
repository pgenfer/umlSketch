namespace Yuml
{
    /// <summary>
    /// method validation service needs the method itself
    /// to check if there is another method with the same signature
    /// </summary>
    public interface IMethodNameValidationService
    {
        ValidationResult ValidateNameChange(Method method, string newName);
    }
}