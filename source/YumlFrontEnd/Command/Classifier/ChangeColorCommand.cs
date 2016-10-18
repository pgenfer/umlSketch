using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class ChangeColorCommand : DomainObjectBaseCommand<Classifier>
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
