using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.mixins;

namespace yuml.metadata
{
    /// <summary>
    /// base class for an association.
    /// </summary>
    public class Association
    {
        public Connection FirstConnection { get; set; }
        public Connection SecondConnection { get; set; }

        public Association From(Class first, bool withDirection=false,string name="")
        {
            FirstConnection = new Connection(@class:first,hasDirection:withDirection,name:name);
            return this;
        }

        public Association To(Class second, bool withDirection=false,string name="")
        {
            SecondConnection = new Connection(@class: second,hasDirection:withDirection, name:name);
            return this;
        }

        public Association Bidirectional()
        {
            if(FirstConnection != null) FirstConnection.HasDirection = true;
            if(SecondConnection != null) SecondConnection.HasDirection = true;
            return this;
        }        
    }    
}
