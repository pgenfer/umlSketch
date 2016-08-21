using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// command interface is used when an assigned classifier 
    /// is changed (e.g. the return type of a method, the type of a property etc...)
    /// </summary>
    public interface IChangeTypeCommand
    {
        void ChangeType(string nameOfOldType, string nameOfNewType);
    }
}
