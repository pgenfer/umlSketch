using System.Linq;
using Common;

namespace Yuml
{
    /// <summary>
    /// base list with additional Name constraint. Provides functionality for named-based searching
    /// in the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NamedBaseList<T> : BaseList<T> where T : INamed, IVisible
    {
        private readonly FindBestNameMixin _findBestName;
        protected NamedBaseList()
        {
            _findBestName = new FindBestNameMixin(_list.OfType<INamed>());
        }
        protected string FindBestName(string defaultName) => _findBestName.FindBestName(defaultName);
    }
}