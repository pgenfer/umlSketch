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
            var classifierList = toDtoConverter.ToDto(classifiers);
            return SaveDto(classifierList);
            
        }

        internal JsonContent SaveDto(IEnumerable<ClassifierDto> classifierDtos)
        {
            var jsonString = JsonConvert.SerializeObject(
                classifierDtos,
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
            var classifierDtos = LoadDto(jsonContent);
            var toDomainConverter = new DomainDtoConverter();
            return toDomainConverter.ToDomain(classifierDtos);            
        }

        internal IEnumerable<ClassifierDto> LoadDto(JsonContent jsonContent)
        {
            var classifierDtos = JsonConvert.DeserializeObject<List<ClassifierDto>>(jsonContent.Value);
            return classifierDtos;
        }
    }
}
