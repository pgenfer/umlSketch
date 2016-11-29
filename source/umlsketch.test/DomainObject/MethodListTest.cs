using NUnit.Framework;
using NSubstitute;
using UmlSketch.DomainObject;
using static NSubstitute.Substitute;

namespace UmlSketch.Test
{
    public class MethodListTest : TestBase
    {
        private ParameterList _parameters;
        private Method _method;

        private MethodList _methods;

        protected override void Init()
        {
            _methods = new MethodList();
            _method = For<Method>();
            _method.Name = "method";
            _parameters = For<ParameterList>();
            _parameters.IsSame(_parameters).Returns(true);
            _method.Parameters.Returns(_parameters);

            _methods.AddExistingMember(_method);
        }


        [TestDescription("Checks that true is returned if methods have same signature")]
        public void ContainsMethodWithSameSignature_True()
        {
            var result = _methods.ContainsMethodWithSignature("method", _parameters);
            Assert.IsTrue(result);
        }

        [TestDescription("Checks that false is returned if methods have different names")]
        public void ContainsMethodWithDifferentNames_False()
        {
            var result = _methods.ContainsMethodWithSignature("method1", _parameters);
            Assert.IsFalse(result);
        }

        [TestDescription("Checks that false is returned if methods have different parameters")]
        public void ContainsMethodWithDifferentParameters_False()
        {
            var otherParameters = new ParameterList();
            _parameters.IsSame(otherParameters).Returns(false);

            var result = _methods.ContainsMethodWithSignature("method", otherParameters);
            Assert.IsFalse(result);
        }
    }
}