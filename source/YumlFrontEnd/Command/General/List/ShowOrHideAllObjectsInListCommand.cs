using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml.Command
{
    public class ShowOrHideAllObjectsInListCommand : IShowOrHideCommand
    {
        private readonly IVisibleObjectList _visibleObjects;
        private readonly MessageSystem _messageSystem;

        public ShowOrHideAllObjectsInListCommand(IVisibleObjectList visibleObjects, MessageSystem messageSystem)
        {
            _visibleObjects = visibleObjects;
            _messageSystem = messageSystem;
        }

        public bool ChangeVisibility()
        {
            _visibleObjects.IsVisible = !_visibleObjects.IsVisible;
            // now fire a message for every object in the list
            foreach (var visibleObject in _visibleObjects.VisibleObjects)
                _messageSystem.Publish(visibleObject, new VisibilityChangedEvent(visibleObject));
            return _visibleObjects.IsVisible;

        }

        public bool IsVisible => _visibleObjects.IsVisible;
    }
}
