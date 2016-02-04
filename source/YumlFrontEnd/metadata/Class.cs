using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.mixins;

namespace yuml.metadata
{
    /// <summary>
    /// class represents a type within the uml diagram
    /// </summary>
    public class Class
    {
        public static Class CreateSystemType(string systemTypeName) => new Class(systemTypeName);

        public Class() { }

        /// <summary>
        /// ctor is only used by factory methods (e.g. when a system type is created)
        /// </summary>
        /// <param name="systemTypeName"></param>
        /// <param name="systemType"></param>
        private Class(string systemTypeName, bool systemType = true)
        {
            Name = systemTypeName;
            IsSystemType = systemType;
        }
        
        /// <summary>
        /// name of this type
        /// </summary>
        private readonly NameMixin _name = new NameMixin();
        private PropertyList _properties = new PropertyList();
        private MethodList _methods = new MethodList();
        private AttributeList _attributes = new AttributeList();


        /// <summary>
        /// flag is used to specify a system type
        /// </summary>
        public bool IsSystemType { get; } = false;

        public void AddMember(Property member) => _properties.AddMember(member);
        public void AddMember(Method member) => _methods.AddMember(member);
        public void AddMember(Attribute member) => _attributes.AddMember(member);

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        public override string ToString() => _name.ToString();
        public override int GetHashCode() => _name.GetHashCode();
        public override bool Equals(object obj) => _name.Equals(obj);
    }
}
