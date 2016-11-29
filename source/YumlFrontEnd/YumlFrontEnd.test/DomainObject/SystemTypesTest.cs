using NUnit.Framework;
using UmlSketch.DomainObject;

namespace UmlSketch.Test
{
    public class SystemTypesTest : TestBase
    {
        readonly SystemTypes _systemTypes = new SystemTypes();

        [TestDescription("Check if system types contain standard system types like int")]
        public void SystemTypes_CheckHasInt()
        {
            var @int = _systemTypes["int"];
            Assert.NotNull(@int);
        }

        [TestDescription("Check that string is in system types")]
        public void SystemTypes_CheckHasString()
        {
            var @string = _systemTypes["string"];
            Assert.NotNull(@string);
        }
    }
}