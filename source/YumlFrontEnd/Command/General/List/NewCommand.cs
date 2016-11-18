using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml.Command
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
