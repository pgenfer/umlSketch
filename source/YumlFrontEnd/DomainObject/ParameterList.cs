using System.Collections;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
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
    }
}