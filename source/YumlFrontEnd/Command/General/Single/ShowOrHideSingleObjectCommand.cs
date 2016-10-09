using System;
using Common;


namespace Yuml.Command
{
    /// <summary>
    /// generic command used to show or hide a domain
    /// object in the resulting diagram
    /// </summary>
    public class ShowOrHideSingleObjectCommand : IShowOrHideCommand
    {
        private readonly IVisible _visibleObject;
        private readonly MessageSystem _messageSystem;

        public ShowOrHideSingleObjectCommand(IVisible visibleObject, MessageSystem messageSystem)
        {
            _visibleObject = visibleObject;
            _messageSystem = messageSystem;
        }

        public bool ChangeVisibility()
        {
            _visibleObject.IsVisible = !_visibleObject.IsVisible;
            _messageSystem.Publish(_visibleObject, new VisibilityChangedEvent(_visibleObject));
            return _visibleObject.IsVisible;
        }

        public bool IsVisible => _visibleObject.IsVisible;
    }
}