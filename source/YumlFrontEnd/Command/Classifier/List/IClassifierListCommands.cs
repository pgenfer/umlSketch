using System.Collections.Generic;

namespace Yuml.Commands
{
    /// <summary>
    /// all commands that operate on the complete list of
    /// classifiers.
    /// </summary>
    public interface IClassifierListCommands
    {
        /// <summary>
        /// returns a list of all available classifiers
        /// </summary>
        IEnumerable<Classifier> QueryAllClassifiers { get; }
        /// <summary>
        /// returns the commands that are available for a single classifier
        /// </summary>
        /// <param name="classifier"></param>
        /// <returns></returns>
        IClassiferCommands GetCommandsForClassifier(Classifier classifier);
    }
}