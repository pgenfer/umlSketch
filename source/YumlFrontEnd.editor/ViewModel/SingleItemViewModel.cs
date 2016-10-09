using System;
using System.Collections.Generic;
using System.Linq;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Base class for view models that represent a single domain object.
    /// The type information of the available commands is a generic parameter,
    /// in that way the CustomInit method can always use the correct type of command parameters
    /// </summary>
    /// <typeparam name="TDomain">type of the domain entitiy</typeparam>
    /// <typeparam name="TSingleCommandContext">type of command context which is
    /// available for this single domain object</typeparam>
    internal abstract class SingleItemViewModel<TDomain,TSingleCommandContext> : SingleItemViewModelBase<TDomain> 
        where TSingleCommandContext : ISingleCommandContext
    {
        protected readonly TSingleCommandContext _commands;
     
        protected SingleItemViewModel(TSingleCommandContext commands) :base(commands)
        {
            _commands = commands;
        }
    }
}