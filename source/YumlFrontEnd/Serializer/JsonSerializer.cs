using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Serializer.Dto;

namespace Yuml.Serializer
{
    public class JsonSerializer
    {
        public JsonContent Save(ClassifierDictionary classifiers)
        {
            var toDtoConverter = new DomainDtoConverter();
            var classifierList = toDtoConverter
                .ToDto(classifiers)
                .ToList();
            // TODO: fix this when relations are implemented
            // in the domain model, currently we only store the
            // classifiers
            var diagramDto = new DiagramDataDto() { Classifiers = classifierList };
            return SaveDto(diagramDto);
            
        }

        internal JsonContent SaveDto(DiagramDataDto completeDiagramData)
        {
            var jsonString = JsonConvert.SerializeObject(
                completeDiagramData,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            return new JsonContent(jsonString);
        }

        public ClassifierDictionary Load(JsonContent jsonContent)
        {
            var toDomainConverter = new DomainDtoConverter();

            var diagramDto = LoadDto(jsonContent);
            // TODO: currently only the classifiers are handled
            // but we also have to handle relations here
            return toDomainConverter.ToDomain(diagramDto.Classifiers);            
        }

        internal DiagramDataDto LoadDto(JsonContent jsonContent)
        {
            var diagramDto = JsonConvert.DeserializeObject<DiagramDataDto>(jsonContent.Value);
            return diagramDto;
        }
    }
}
