using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// base class for command context which hosts all available commands
    /// for a single domain object.
    /// </summary>
    public abstract class SingleCommandContextBase<TDomain> : ISingleCommandContext<TDomain>
    {
        public IRenameCommand Rename { get; protected set; }
        public IDeleteCommand Delete { get; protected set; }
        public IShowOrHideCommand Visibility { get; protected set; }
    }
}
