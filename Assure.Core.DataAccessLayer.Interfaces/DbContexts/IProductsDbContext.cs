using Assure.Core.DataAccessLayer.Interfaces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface IProductsDbContext : ICoreDbContext<Product>
    {
        Task<IEnumerable<Benefit>> GetProductBenefitsAsync(IDbConnection dbConnection, int productId, IDbTransaction dbTransaction = null);
        Task<int> AddProductBenefit(IDbTransaction dbTransaction, int productId, int benefitId);
        Task<int> RemoveProductBenefit(IDbTransaction dbTransaction, int productId, int benefitId);
    }
}