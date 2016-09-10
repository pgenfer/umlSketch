﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// event fired when a domain object was removed from the system.
    /// </summary>
    public class DomainObjectDeletedEvent<T> : IDomainEvent
    {
        /// <summary>
        /// the domain object that was removed
        /// </summary>
        public T DomainObject { get; }

        /// <summary>
        /// creates the event
        /// </summary>
        /// <param name="domainObject">domain object that was deleted</param>
        public DomainObjectDeletedEvent(T domainObject)
        {
            DomainObject = domainObject;
        }
    }
}
