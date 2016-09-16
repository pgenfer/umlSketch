using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    class RenameAssociationCommand : IRenameCommand
    {
        public void Rename(string newName)
        {
            
        }

        public ValidationResult CanRenameWith(string newName)
        {
            return new Error("not implemented yet");
        }
    }
}
