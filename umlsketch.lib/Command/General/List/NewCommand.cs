using Common;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class NewCommand<T> : INewCommand where T : class, IVisible
    {
        private readonly BaseList<T> _memberList;
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public NewCommand(
            BaseList<T> memberList,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _memberList = memberList;
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var newMember = _memberList.CreateNew(_classifiers);
            if(newMember != null)
               _messageSystem.PublishCreated(_memberList, newMember);
        }
    }
}
