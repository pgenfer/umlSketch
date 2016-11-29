using System.Diagnostics.Contracts;
using System.Linq;
using UmlSketch.DiagramWriter;

namespace UmlSketch.DomainObject
{
    public class ParameterList : NamedBaseList<Parameter>
    {
        public override Parameter CreateNew(ClassifierDictionary classifiers)
        {
            var newName = FindBestName(Strings.NewParameter);
            var stringType = classifiers.String;
            var parameter = new Parameter(stringType, newName);
            AddNewMember(parameter);
            return parameter;
        }

        public SubSet FindMethodsThatDependOnClassifier(Classifier classifier) => 
            Filter(x => x.Type == classifier);

        public void CreateParameter(Classifier classifier, string name) =>
            AddNewMember(new Parameter(classifier, name));

        public virtual bool IsSame(ParameterList others)
        {
            if (others.Count != Count)
                return false;
            for(var i=0;i<Count;i++)
                if (others._list[i].Name != _list[i].Name ||
                    others._list[i].Type != _list[i].Type)
                    return false;
            return true;
        }

        private int Count => _list.Count;


        public void WriteTo(MethodWriter methodWriter)
        {
            Contract.Requires(methodWriter != null);

            var parameters = this.Where(x => x.IsVisible && x.Type.IsVisible).ToArray();
            for(var i=0;i<parameters.Length;i++)
            {
                var parameter = parameters[i];
                var parameterWriter = methodWriter.WithParameter();
                parameterWriter = parameterWriter.AddParameter(parameter.Type.Name, parameter.Name);
                if (i < parameters.Length - 1)
                    parameterWriter.AppendToken(",");
            }
        }
    }
}