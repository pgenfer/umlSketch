using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;
using UmlSketch.Validation;

namespace UmlSketch.Command
{
    public class SingleParameterCommandContext : SingleCommandContextBase<Parameter>
    {
        public SingleParameterCommandContext(
            BaseList<Parameter> memberList,
            Parameter member,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem,
            IValidateNameService validateParameterNameService,
            IAskUserBeforeDeletionService askUserService) : 
            base(memberList, member, messageSystem,askUserService)
        {
            ChangeType = new ChangeParameterTypeCommand(classifiers, member, messageSystem);
            Rename = new RenameMemberCommand(member, validateParameterNameService, messageSystem);
        }

        public IChangeTypeCommand ChangeType { get; }
    }
}
