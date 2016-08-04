using NUnit.Framework;
using Yuml.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    public class JsonSerializerTest : TestBase
    {
        [TestDescription(@"Ensures that the classifier dictionary is stored correcctly")]
        public void SaveTest()
        {
            var fromClassifiers = new ClassifierDictionary(String);
           
            var serializer = new JsonSerializer();

            // store the classifiers as json
            var json = serializer.Save(fromClassifiers);
            // read them from json to another dictionary
            var toClassifiers = serializer.Load(json);

            // Ensure that both have the same classifiers
            Assert.AreEqual(1, toClassifiers.Count);
            Assert.AreEqual(String.Name, toClassifiers.ElementAt(0).Name);
            Assert.AreEqual(1, toClassifiers.ElementAt(0).Properties.Count());
        }
    }
}