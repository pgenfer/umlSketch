using Common;
using UmlSketch.Mixin;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// base list with additional Name constraint. Provides functionality for named-based searching
    /// in the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NamedBaseList<T> : BaseList<T> where T : class, INamed, IVisible
    {
        private readonly FindBestNameMixin _findBestName;
        protected NamedBaseList()
        {
            _findBestName = new FindBestNameMixin(_list);
        }
        protected string FindBestName(string defaultName) => _findBestName.FindBestName(defaultName);
    }
}