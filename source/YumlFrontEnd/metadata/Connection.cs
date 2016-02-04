using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.mixins;

namespace yuml.metadata
{
    public class Connection
    {
        private readonly NameMixin _name = new NameMixin();

        public Connection(
            Cardinality cardinality = null,
            Class @class = null,
            bool hasDirection = false,
            string name = "")
        {
            Cardinality = cardinality ?? Cardinality.None;
            Class = @class;
            HasDirection = hasDirection;
            Name = name;
        }


        public Cardinality Cardinality { get; set; }
        public Class Class { get; set; }
        public bool HasDirection { get; set; }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public override string ToString() => _name.ToString();
    }
}
