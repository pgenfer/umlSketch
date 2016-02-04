using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuml.metadata
{
    public class Cardinality
    {
        private string _CardinalityString = string.Empty;

        private Cardinality(string cardinality)
        {
            _CardinalityString = cardinality;
        }

        public override string ToString() => _CardinalityString;

        public static Cardinality One = new Cardinality("1");
        public static Cardinality OneToMany = new Cardinality("1..*");
        public static Cardinality ZeroToMany = new Cardinality("0..*");
        public static Cardinality None = new Cardinality(string.Empty);
    }
}
