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

        /// <summary>
        /// loads the given data in json format and stores it in the given classifier dictionary
        /// </summary>
        /// <param name="jsonContent">content to load</param>
        /// <param name="classifierDictionary">classifier dictionary where the loaded classifiers should be stored</param>
        public void Load(JsonContent jsonContent,ClassifierDictionary classifierDictionary)
        {
            var toDomainConverter = new DomainDtoConverter();

            var diagramDto = LoadDto(jsonContent);
            // TODO: currently only the classifiers are handled
            toDomainConverter.ToDomain(diagramDto.Classifiers,classifierDictionary);            
        }

        internal DiagramDataDto LoadDto(JsonContent jsonContent)
        {
            var diagramDto = JsonConvert.DeserializeObject<DiagramDataDto>(jsonContent.Value);
            return diagramDto;
        }
    }
}
