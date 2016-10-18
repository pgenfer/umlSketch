using NUnit.Framework;
using YumlFrontEnd.editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YumlFrontEnd.editor.Test
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