namespace Yuml.Commands
{
    /// <summary>
    /// base class for all commands that operate on a single classifier
    /// </summary>
    public abstract class SingleClassifierCommandBase
    {
        protected readonly Classifier _classifier;

        protected SingleClassifierCommandBase(Classifier classifier)
        {
            _classifier = classifier;
        }
    }
}