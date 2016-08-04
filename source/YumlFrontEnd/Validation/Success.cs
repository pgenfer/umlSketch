using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// success object in case a validation was successful
    /// </summary>
    internal class Success : ValidationResult
    {
        public Success() : base(string.Empty, Validation.Success)
        {
        }
    }
}
