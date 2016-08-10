using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// base class for commands that operate on a single domain object
    /// </summary>
    /// <typeparam name="T">type of domain object
    /// this command operates on</typeparam>
    public abstract class DomainObjectBaseCommand<T>
    {
        /// <summary>
        /// domain object which is the target of the command.
        /// </summary>
        protected readonly T _domainObject;

        protected DomainObjectBaseCommand(T domainObject)
        {
            _domainObject = domainObject;
        }
    }
}
