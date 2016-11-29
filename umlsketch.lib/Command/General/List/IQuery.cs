using System.Collections.Generic;

namespace UmlSketch.Command
{
    /// <summary>
    /// query used to retrieve all domain objects
    /// within a context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<out T>
    {
        IEnumerable<T> Get();
    }
}