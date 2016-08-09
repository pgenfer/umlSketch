using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// class which extends property change base class
    /// by automatically setting the property name 
    /// by using caller attribute
    /// </summary>
    internal class AutoPropertyChange : PropertyChangedBase
    {
        protected void RaisePropertyChanged([CallerMemberName] string memberName = "")
            => NotifyOfPropertyChange(memberName);
    }
}
