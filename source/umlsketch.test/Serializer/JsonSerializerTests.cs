using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UmlSketch.DomainObject;
using UmlSketch.Serializer;
using UmlSketch.Serializer.Dto;

namespace UmlSketch.Test
{
    public class JsonSerializerTest : TestBase
    {
        [TestDescription("Ensures that the classifier dictionary is stored correctly")]
        public void SaveLoad_Test()
        {
            var fromClassifiers = new ClassifierDictionary(String);
            var toClassifiers = new ClassifierDictionary();
            var fromDiagram = new Diagram(fromClassifiers);
            var toDiagram = new Diagram(toClassifiers);
           
            var serializer = new JsonSerializer();

            // store the classifiers as json
            var json = serializer.Save(fromDiagram);
            // read them from json to another dictionary
            serializer.Load(json, toDiagram);

            // Ensure that both have the same classifiers
            Assert.IsTrue(toClassifiers.Count(x => x.Name == String.Name) == 1);
        }

        [TestDescription("Checks that method Dtos are stored correctly")]
        public void SaveLoad_MethodDtos()
        {
            var diagramDto = new DiagramDataDto
            {
                Classifiers = new List<ClassifierDto>
                {
                    StringDto,IntegerDto,ServiceDto
                }
            };

            var serializer = new JsonSerializer();
            var content = serializer.SaveDto(diagramDto);
            var restoredDtos = serializer
                .LoadDto(content)
                .Classifiers
                .ToList();

            var service = restoredDtos.Single(x => x.Name == "Service");
            // ensure that classifiers references are reused
            Assert.AreSame(restoredDtos[0], service.Methods[0].Parameters[0].Type);
            // ensure that all method data was read correctly
            Assert.AreEqual(service.Methods[0],ServiceDto.Methods[0]);
        }

        [TestDescription("Checks that relations are stored correctly")]
        public void SaveLoad_ReleationsDto()
        {
            var diagramDto = new DiagramDataDto
            {
                Classifiers = new List<ClassifierDto> { CarDto, TireDto },
            };

            var serializer = new JsonSerializer();
            var json = serializer.SaveDto(diagramDto);

            var newDiagramDto = serializer.LoadDto(json);
            var loadedCar = newDiagramDto.Classifiers[0];
            var loadedTire = newDiagramDto.Classifiers[1];
            var relation = loadedCar.Associations.Single();

            Assert.AreSame(loadedCar, relation.Start);
            Assert.AreSame(loadedTire, relation.End);
            Assert.AreEqual(RelationType.Aggregation, relation.RelationType);
            Assert.AreEqual(Multiplicity.ZeroToMany, relation.EndMultiplicity);
        }
    }
}