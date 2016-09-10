using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// additional commands available for a single property
    /// </summary>
    public interface ISinglePropertyCommands : ISingleCommandContext
    {
        IChangeTypeCommand ChangeTypeOfProperty { get; }
    }
}
