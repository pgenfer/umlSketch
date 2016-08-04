using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    internal class PropertyViewModel : PropertyChangedBase
    {
        private NameMixin _name = new NameMixin();

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value;NotifyOfPropertyChange(() => Name); }
        }

        public override string ToString() => _name.ToString();
    }
}
