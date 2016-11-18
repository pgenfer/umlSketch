using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// method of a type.
    /// </summary>
    public class Method : IVisible, INamed
    {
        private readonly IVisible _visible = new VisibleMixin();
        private readonly NameMixin _name = new NameMixin();
        private ParameterList _parameters = new ParameterList();

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

        public override string ToString()
        {
            return $"{ReturnType} {Name} ({string.Join(",",_parameters.Select(x => x.ToString()))})";
        }

        /// <summary>
        /// internal setter is only used for serialization.
        /// </summary>
        public ParameterList Parameters { get { return _parameters;} internal set { _parameters = value; } }

        // TODO: return type can be changed by user
        public Classifier ReturnType { get; set; }

        public Method(string name, Classifier returnType, bool isVisible = true)
        {
            Name = name;
            ReturnType = returnType;
            IsVisible = isVisible;
        }
        public IEnumerator<Parameter> GetEnumerator() => _parameters.GetEnumerator();
       
        public MethodWriter WriteTo(MethodWriter methodWriter)
        {
            Requires(methodWriter != null);

            return methodWriter
                .WithReturnType(ReturnType.Name)
                .WithName(Name);
            // TODO: write parameters
        }

        public void CreateParameter(Classifier classifier, string name)
            => _parameters.CreateParameter(classifier, name);
    }
}
