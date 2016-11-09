using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;

namespace Yuml.Command
{
    /// <summary>
    /// base class for command list context.
    /// The individual commands must be implemented by derived types.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class ListCommandContextBase<TDomain> : IListCommandContext<TDomain>
    {
        public INewCommand New { get; protected set; }
        public ShowOrHideAllObjectsInListCommand Visibility { get; protected set; }
        public IQuery<TDomain> All { get; protected set; }
    }
}
