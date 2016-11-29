using Common;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;

namespace UmlSketch.Command
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
            MessageSystem messageSystem,
            IAskUserBeforeDeletionService askUserService)
        {
            Delete = new DeleteCommand<T>(memberList, member, messageSystem,askUserService);
            Visibility = new ShowOrHideSingleObjectCommand(member, messageSystem);
        }
    }
}
