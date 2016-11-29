using UmlSketch.Event;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Command
{
    /// <summary>
    /// base class for commands that operate on a single domain object
    /// </summary>
    /// <typeparam name="T">type of domain object
    /// this command operates on</typeparam>
    public abstract class DomainObjectBaseCommand<T> where T : class
    {
        /// <summary>
        /// domain object which is the target of the command.
        /// </summary>
        protected readonly T _domainObject;

        protected readonly MessageSystem _messageSystem;

        protected DomainObjectBaseCommand(T domainObject,MessageSystem messageSystem)
            :this(domainObject)
        {   
            Requires(messageSystem != null);
            _messageSystem = messageSystem;
        }

        protected DomainObjectBaseCommand(T domainObject)
        {
            Requires(domainObject != null);
            _domainObject = domainObject;
        }
    }
}
