namespace UmlSketch.Service
{
    /// <summary>
    /// Asks the user before a delete operation starts
    /// </summary>
    public interface IAskUserBeforeDeletionService
    {
        /// <summary>
        /// starts an interaction process with the user
        /// and asks whether the given domain object should be removed
        /// </summary>
        /// <param name="message">Message that should be presented to the user. Must be defined
        /// by the caller.</param>
        /// <returns>true if the user chooses to delete the object, otherwise false.</returns>
        bool ShouldDomainObjectBeDeleted(string message);
    }
}
