using System.Collections.Generic;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using Assure.Core.Shared.Interfaces.Models;

namespace Assure.Core.RepositoryLayer.Implementation.Serialization
{
    public interface IObjectDocumentSerializer
    {
        T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer;
        IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers) where T1 : ObjectDocumentContainer;
        PageResponse<T2> Deserialize<T1, T2>(PageResponse<T1> pageResponse) where T1 : ObjectDocumentContainer;
        T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer;
    }
}