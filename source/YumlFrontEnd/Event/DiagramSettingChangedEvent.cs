using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Event
{
    /// <summary>
    /// this event is fired
    /// whenever an application setting changes that affects 
    /// the diagram.
    /// </summary>
    public class DiagramSettingChangedEvent : IDomainEvent
    {
        private readonly string _settingName;

        public DiagramSettingChangedEvent(string settingName)
        {
            _settingName = settingName;
        }
    }
}
