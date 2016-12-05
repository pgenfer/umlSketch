using NUnit.Framework;
using System.Windows.Media;
using UmlSketch.Editor;

namespace UmlSketch.Test
{
    [TestFixture]
    public class ColorExtensionTest
    {
        [Test]
        public void ToColorTest()
        {
            const string colorString = "Red";
            var color = colorString.ToColorFromFriendlyName();
            Assert.AreEqual(color, Colors.Red);
        }
    }
}