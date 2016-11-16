using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml.Command
{
    /// <summary>
    /// generic delete command that can be used to remove
    /// an item from a domain list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteCommand<T> : IDeleteCommand where T : class,IVisible
    {
        private readonly BaseList<T> _members;
        private readonly T _member;
        private readonly MessageSystem _messageSystem;

        public DeleteCommand(
            BaseList<T> members,
            T member,
            MessageSystem messageSystem)
        {
            _members = members;
            _member = member;
            _messageSystem = messageSystem;
        }

        public void DeleteItem() => _members.DeleteMember(_member, _messageSystem);
    }
}
