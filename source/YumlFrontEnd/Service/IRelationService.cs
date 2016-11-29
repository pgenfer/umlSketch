using UmlSketch.DomainObject;

namespace UmlSketch.Service
{
    /// <summary>
    /// the relation service handles the relation between classifiers
    /// if any of their relation changes.
    /// </summary>
    public interface IRelationService
    {
        void ChangeFromClassToInterface(Classifier @class);
        void ChangeFromInterfaceToClass(Classifier @class);
    }
}