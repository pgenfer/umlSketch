using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// contains all available system types that are currently supported by the system.
    /// </summary>
    public class SystemTypes : IEnumerable<Classifier>
    {
        private readonly Dictionary<string,Classifier> _systemTypes = new Dictionary<string, Classifier>();
        /// <summary>
        /// required to get the correct C# specific type names
        /// (int instead of int32, string instead of String etc...)
        /// See here for description:
        /// http://stackoverflow.com/questions/4369737/how-can-i-get-the-primitive-name-of-a-type-in-c
        /// </summary>
        private readonly CSharpCodeProvider _compiler = new CSharpCodeProvider();
        
        /// <summary>
        /// currently, system types are created hard coded,
        /// but later they could also be read from a data storage
        /// </summary>
        public SystemTypes()
        {
            var netTypes = new []
            {
                typeof(int),
                typeof(bool),
                typeof(string),
                typeof(double),
                typeof(void)
            };

            foreach (var type in netTypes)
            {
                var typeName = _compiler.GetTypeOutput(new CodeTypeReference(type));
                _systemTypes.Add(typeName, new Classifier(typeName, true));
            }
        }

        public IEnumerator<Classifier> GetEnumerator() => _systemTypes.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _systemTypes.Values.GetEnumerator();
        public Classifier this[string name] => _systemTypes[name];
    }
}
