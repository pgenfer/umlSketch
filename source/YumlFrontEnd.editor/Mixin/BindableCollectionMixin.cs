using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// mixin that stores a collection of sub items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BindableCollectionMixin<T>
    {
        public BindableCollectionMixin()
        {
            Items = new BindableCollection<T>();
        }

        public BindableCollection<T> Items { get; }
        public void RemoveItem(T item) => Items.Remove(item);
    }
}
