using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml.Command
{
    /// <summary>
    /// base class for command context which hosts all available commands
    /// for a single domain object.
    /// </summary>
    public abstract class SingleCommandContextBase<T> : 
        ISingleCommandContext<T> where T : class, IVisible
    {
        public IRenameCommand Rename { get; protected set; }
        public IDeleteCommand Delete { get; protected set; }
        public IShowOrHideCommand Visibility { get; protected set; }

        protected SingleCommandContextBase(
            BaseList<T> memberList, 
            T member, 
            MessageSystem messageSystem)
        {
            Delete = new DeleteCommand<T>(memberList, member, messageSystem);
            Visibility = new ShowOrHideSingleObjectCommand(member, messageSystem);
        }
    }
}
