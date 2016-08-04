using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// validation result object in case the
    /// validation was not successful
    /// </summary>
    internal class Error : ValidationResult
    {
        public Error(string message) : base(message, Validation.Error)
        {
        }
    }
}
