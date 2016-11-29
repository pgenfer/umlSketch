using Common;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    /// <summary>
    /// base class for command list context.
    /// The individual commands must be implemented by derived types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ListCommandContextBase<T> : IListCommandContext<T> where T : class, IVisible
    {
        protected ListCommandContextBase(
            BaseList<T> memberList,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            // default implementation, derived types
            // can define special implementation for "All" query
            All = new Query<T>(() => memberList);
            New = new NewCommand<T>(memberList, classifiers, messageSystem);
            Visibility = new ShowOrHideAllObjectsInListCommand(memberList, messageSystem);
        }

        public INewCommand New { get; }
        public ShowOrHideAllObjectsInListCommand Visibility { get; }
        public IQuery<T> All { get; protected set; }
    }
}
