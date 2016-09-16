using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class DomainObjectCreatedEvent<T> : IDomainEvent
    {
        /// <summary>
        /// the domain object that was removed
        /// </summary>
        public T DomainObject { get; }

        /// <summary>
        /// creates the event
        /// </summary>
        /// <param name="domainObject">domain object that was deleted</param>
        public DomainObjectCreatedEvent(T domainObject)
        {
            DomainObject = domainObject;
        }
    }
}
