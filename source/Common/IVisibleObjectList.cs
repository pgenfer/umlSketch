using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// a list of items with a visibility property.
    /// Changing the visible property will affect all items in the list
    /// </summary>
    public interface IVisibleObjectList : IVisible
    {
        /// <summary>
        /// returns the list of items in this list with a visibility property
        /// </summary>
        IEnumerable<IVisible> VisibleObjects { get; }
    }
}