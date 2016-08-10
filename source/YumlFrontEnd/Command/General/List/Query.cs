using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// A query is a command which only returns items
    /// and does not change them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Query<T> : IQuery<T>
    {
        /// <summary>
        /// function which returns the query result
        /// </summary>
        private readonly Func<IEnumerable<T>> _queryFunction;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryFunction">Function which returns
        /// the resulting query. The function will be executed
        /// when the command is executed.</param>
        internal Query(Func<IEnumerable<T>> queryFunction)
        {
            _queryFunction = queryFunction;
        }

        public IEnumerable<T> Get() => _queryFunction();
    }
}
