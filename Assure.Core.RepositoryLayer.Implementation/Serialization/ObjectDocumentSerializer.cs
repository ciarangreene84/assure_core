using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;
using AutoMapper;
using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Assure.Core.RepositoryLayer.Implementation.Serialization
{
    [AddSingleton(typeof(IObjectDocumentSerializer))]
    public sealed class ObjectDocumentSerializer  : IObjectDocumentSerializer
    {
        private readonly ILogger<ObjectDocumentSerializer> _logger;
        private readonly IMapper _mapper;

        private readonly JsonSerializerSettings _settings;

        public ObjectDocumentSerializer(ILogger<ObjectDocumentSerializer> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _settings = new JsonSerializerSettings()
            {
                ContractResolver = new IgnoreNamespacePropertiesResolver("Assure.Core.RepositoryLayer.Interfaces.Models")
            };
        }

        public T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer 
        {
            var result = JsonConvert.DeserializeObject<T2>(objectDocumentContainer.ObjectDocument, _settings);
            return _mapper.Map(objectDocumentContainer, result);
        }

        public IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers) where T1 : ObjectDocumentContainer
        {
            return objectDocumentContainers.Select(Deserialize<T1, T2>);
        }

        public PageResponse<T2> Deserialize<T1, T2>(PageResponse<T1> pageResponse) where T1 : ObjectDocumentContainer
        {
            return new PageResponse<T2>()
            {
                PageIndex = pageResponse.PageIndex,
                PageSize = pageResponse.PageSize,
                SortBy = pageResponse.SortBy,
                SortOrder = pageResponse.SortOrder,
                TotalItemCount = pageResponse.TotalItemCount,
                Items = Deserialize<T1, T2>(pageResponse.Items)
            };
        }

        public T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer
        {
            var objectDocumentContainer = _mapper.Map<T2>(repositoryObject);
            objectDocumentContainer.ObjectDocument = JsonConvert.SerializeObject(repositoryObject, _settings);
            objectDocumentContainer.ObjectHash = objectDocumentContainer.ObjectDocument.GetHashCode();
            return objectDocumentContainer;
        }
    }
}
