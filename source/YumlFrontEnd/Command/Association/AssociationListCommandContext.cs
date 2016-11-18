using Yuml.DomainObject;

namespace Yuml.Command
{
    /// <summary>
    /// commands that can be executed on the association list of a classifier
    /// </summary>
    public class AssociationListCommandContext : ListCommandContextBase<Relation>
    {
        public AssociationListCommandContext(
         ClassifierAssociationList associations,
         ClassifierDictionary classifiers,
         MessageSystem messageSystem)
            :base(associations,classifiers,messageSystem)
        {
        }
    }
}