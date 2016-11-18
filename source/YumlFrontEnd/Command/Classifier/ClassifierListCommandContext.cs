using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command;
using Yuml.Service;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    public class ClassifierListCommandContext : IListCommandContext<Classifier>
    {
        public ClassifierListCommandContext(
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            Requires(classifiers != null);
            Requires(messageSystem != null);   

            All = new Query<Classifier>(() => classifiers.NoSystemTypes);
            Visibility = new ShowOrHideAllObjectsInListCommand(classifiers, messageSystem);
            New = new NewClassifierCommand(classifiers, messageSystem);
        }

        public INewCommand New { get; }
        public ShowOrHideAllObjectsInListCommand Visibility { get; }
        public IQuery<Classifier> All { get; }
    }
}
