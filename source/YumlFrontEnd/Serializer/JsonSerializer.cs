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
        public JsonContent Save(Diagram diagram)
        {
            var toDtoConverter = new DomainDtoConverter();
            var diagramDto = toDtoConverter.ToDto(diagram);
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
        public void Load(JsonContent jsonContent,Diagram diagram)
        {
            var toDomainConverter = new DomainDtoConverter();
            var diagramDto = LoadDto(jsonContent);
            toDomainConverter.ToDomain(diagram, diagramDto);            
        }

        internal DiagramDataDto LoadDto(JsonContent jsonContent)
        {
            var diagramDto = JsonConvert.DeserializeObject<DiagramDataDto>(jsonContent.Value);
            return diagramDto;
        }
    }
}
