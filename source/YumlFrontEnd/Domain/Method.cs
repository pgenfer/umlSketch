using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml
{
    /// <summary>
    /// method of a type.
    /// </summary>
    public class Method : IVisible
    {
        private readonly IVisible _visible = new VisibleMixin();
        private readonly NameMixin _name = new NameMixin();

        public bool IsVisible
        {
            get { return _visible.IsVisible; }
            set { _visible.IsVisible = value; }
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public override string ToString() => _name.ToString();

        public Classifier ReturnType { get;}

        public Method(string name, Classifier returnType)
        {
            Name = name;
            ReturnType = returnType;
        }
    }
}
