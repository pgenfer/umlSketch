using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// base class for view models that represent a list of items.
    /// </summary>
    /// <typeparam name="TDomain">Type of domain objects within the list</typeparam>
    /// <typeparam name="TCommand">Commands that can be used for this view model</typeparam>
    public class ListViewModelBase<TDomain,TCommand> : ListViewModelBaseSimple<TDomain> 
        where TDomain: IVisible // required for BaseList<T>
        where TCommand : IListCommandContext<TDomain>
    {
        private ChangeVisibilityMixin _visibility;
        
        /// <summary>
        /// sub items in this list
        /// </summary>
        private readonly BindableCollectionMixin<SingleItemViewModelBaseSimple<TDomain>> _items =
           new BindableCollectionMixin<SingleItemViewModelBaseSimple<TDomain>>();

        /// <summary>
        /// commands that can be executed by this view model.
        /// The commands represent the application layer
        /// </summary>
        protected TCommand _commands;

        public void New() => _commands.New.CreateNew();

        /// <summary>
        /// updates the list of available items by calling the query which is part of the
        /// command context
        /// </summary>
        protected override void UpdateItemList()
        {
            Items.Clear();
           
            // create single view models for every domain object
            foreach (var domainObject in _commands.All.Get())
                CreateAndAddViewModel(domainObject);
           
            // update the expand flag every time the list changes
            NotifyOfPropertyChange(nameof(CanExpand));
        }

        /// <summary>
        /// initialization method will be called when view model is created by the factory.
        /// Additional command initialization code can be added here.
        /// Don't forget to call base method when overriding.
        /// </summary>
        /// <param name="commands"></param>
        internal virtual void InitCommands(TCommand commands)
        {
            _commands = commands;
            if (commands.Visibility != null)
                _visibility = new ChangeVisibilityMixin(commands.Visibility);
        }

        public bool IsVisible => _visibility.IsVisible;

        public void ShowOrHide()
        {
            _visibility.ShowOrHide();
            NotifyOfPropertyChange(nameof(IsVisible));
        }
    }
}