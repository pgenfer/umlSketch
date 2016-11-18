using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Yuml.Service;

namespace Yuml.Command
{
    /// <summary>
    /// generic delete command that can be used to remove
    /// an item from a domain list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteCommand<T> : IDeleteCommand where T : class, IVisible
    {
        private readonly BaseList<T> _members;
        private readonly T _member;
        private readonly MessageSystem _messageSystem;
        private readonly IAskUserBeforeDeletionService _askUserService;

        public DeleteCommand(
            BaseList<T> members,
            T member,
            MessageSystem messageSystem,
            IAskUserBeforeDeletionService askUserService)
        {
            _members = members;
            _member = member;
            _messageSystem = messageSystem;
            _askUserService = askUserService;
        }

        public void DeleteItem()
        {
            var message = string.Format(
                Strings.AskUserBeforeDeletingDomainObject,
                Strings.ResourceManager.GetString(_member.GetType().Name)); // TODO : translation could be abstracted if necessary

            // check that we ask the user before we delete any domain objects
            if (_askUserService == null ||
                 _askUserService.ShouldDomainObjectBeDeleted(message))
                _members.DeleteMember(_member, _messageSystem);
        }
    }
}
