using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assure.Core.RepositoryLayer.Interfaces.Repositories
{
    public interface IReferenceRepository<TReferenceType>
    {
        Task<IEnumerable<TReferenceType>> GetAsync();
    }
}