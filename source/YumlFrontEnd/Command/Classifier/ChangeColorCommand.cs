using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class ChangeColorCommand : DomainObjectBaseCommand<Classifier>, IChangeColorCommand
    {
        public ChangeColorCommand(
            Classifier domainObject,
            MessageSystem messageSystem) : base(domainObject, messageSystem)
        {
        }

        public void ChangeColor(string newColor)
        {
            _domainObject.Color = newColor;
            _messageSystem.Publish(_domainObject,new ChangeClassifierColorEvent(newColor));
        }
    }
}
