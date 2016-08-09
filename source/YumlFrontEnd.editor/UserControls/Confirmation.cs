namespace YumlFrontEnd.editor
{
    /// <summary>
    /// enum used to evaluate the result of a edit name confirmation operation.
    /// </summary>
    public enum Confirmation
    {
        /// <summary>
        /// no confirmation operation was
        /// done by the user
        /// </summary>
        None,
        /// <summary>
        /// the user canceled the current rename operation
        /// </summary>
        Canceled,
        /// <summary>
        /// the user confirmed the current rename operation
        /// </summary>
        Confirmed
    }
}