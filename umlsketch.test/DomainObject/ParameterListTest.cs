using NUnit.Framework;
using UmlSketch.DomainObject;

namespace UmlSketch.Test
{
    public class ParameterListTest : TestBase
    {
        private ParameterList _one;
        private ParameterList _two;

        protected override void Init()
        {
            _one = new ParameterList();
            _two = new ParameterList();
        }

        [Test]
        public void TwoParameters_IsSame_True()
        {
            _one.CreateParameter(String, "method");
            _two.CreateParameter(String, "method");

            Assert.IsTrue(_one.IsSame(_two));
        }

        [Test]
        public void TwoParametersDifferentName_IsSame_False()
        {
            _one.CreateParameter(Integer, "method1");
            _two.CreateParameter(String, "method2");

            Assert.IsFalse(_one.IsSame(_two));
        }

        [Test]
        public void TwoParametersDifferentType_IsSame_False()
        {
            _one.CreateParameter(Integer, "method");
            _two.CreateParameter(String, "method");

            Assert.IsFalse(_one.IsSame(_two));
        }

        [Test]
        public void TwoParametersDifferentNameAndType_IsSame_False()
        {
            _one.CreateParameter(Integer, "method1");
            _two.CreateParameter(String, "method2");

            Assert.IsFalse(_one.IsSame(_two));
        }

        [Test]
        public void DifferentParameterCount_IsSame_False()
        {
            _one.CreateParameter(Integer, "method1");
            _one.CreateParameter(String, "method2");

            Assert.IsFalse(_one.IsSame(_two));
        }
    }
}