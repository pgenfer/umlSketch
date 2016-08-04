using NUnit.Framework;
using Yuml.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Serializer.Dto;

namespace Yuml.Test
{
    public class JsonSerializerTest : TestBase
    {
        [TestDescription(@"Ensures that the classifier dictionary is stored correcctly")]
        public void SaveLoad_Test()
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

        [TestDescription(@"Checks that method Dtos are stored correctly")]
        public void SaveLoad_MethodDtos()
        {
            var classifierDtos = new List<ClassifierDto>
            {
                StringDto,IntegerDto,ServiceDto
            };

            var serializer = new JsonSerializer();
            var content = serializer.SaveDto(classifierDtos);
            var restoredDtos = serializer.LoadDto(content).ToList();

            var service = restoredDtos.Single(x => x.Name == "Service");
            // ensure that classifiers references are reused
            Assert.AreSame(restoredDtos[0], service.Methods[0].Parameters[0].ParameterType);
            // ensure that all method data was read correctly
            Assert.AreEqual(service.Methods[0],ServiceDto.Methods[0]);
        }
    }
}