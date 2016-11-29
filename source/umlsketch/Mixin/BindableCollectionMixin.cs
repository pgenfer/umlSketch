using Caliburn.Micro;

namespace UmlSketch.Editor
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
