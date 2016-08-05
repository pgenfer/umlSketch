using NUnit.Framework;
using Yuml.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Test;

namespace Yuml.Serializer.Test
{
    [TestFixture]
    public class JsonApplicationSettingSerializerTest
    {
        [TestDescription("Save and load application settings")]
        public void SaveAndLoadApplicationSettings()
        {
            var settings = new ApplicationSettings(
                size: DiagramSize.Huge,
                direction: DiagramDirection.LefToRight);

            var serializer = new JsonApplicationSettingSerializer();
            var json = serializer.Save(settings);
            var newSettings = serializer.Load(json);

            Assert.AreEqual(settings, newSettings);
        }
    }
}