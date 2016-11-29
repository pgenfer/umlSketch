using NUnit.Framework;
using UmlSketch.Serializer;
using UmlSketch.Settings;
using UmlSketch.Test;

namespace Yuml.Serializer.Test
{
    [TestFixture]
    public class JsonApplicationSettingSerializerTest
    {
        [TestDescription("Save and load application settings")]
        public void SaveAndLoadApplicationSettings()
        {
            var settings = new ApplicationSettingsDataMixin(
                size: DiagramSize.Huge,
                direction: DiagramDirection.LeftToRight);

            var serializer = new JsonApplicationSettingSerializer();
            var json = serializer.Save(settings);
            var newSettings = serializer.Load(json);

            Assert.AreEqual(settings.DiagramSize, newSettings.DiagramSize);
            Assert.AreEqual(settings.DiagramDirection, newSettings.DiagramDirection);
        }
    }
}