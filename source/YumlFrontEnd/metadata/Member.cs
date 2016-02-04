using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.mixins;

namespace yuml.metadata
{
    /// <summary>
    /// represents a member of a type,
    /// can either be an attribute, a property
    /// or a method
    /// </summary>
    public class Member
    {
        private NameMixin _name;
        public Class Type { get; set; }
    }

    public class MemberList<T>
    {
        private List<T> _members = new List<T>();
        public void AddMember(T member) => _members.Add(member);
    }

    public class PropertyList : MemberList<Property>
    { }


    public class Property : Member
    {
    }

    public class MethodList : MemberList<Method> { }

    public class Method : Member
    {
    }

    public class Attribute : Member
    {
    }

    public class AttributeList : MemberList<Attribute> { }
}
