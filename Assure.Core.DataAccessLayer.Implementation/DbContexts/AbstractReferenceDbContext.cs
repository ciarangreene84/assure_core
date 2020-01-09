using AutoMapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Assure.Core.DataAccessLayer.Implementation.DbContexts
{
    public abstract class AbstractReferenceDbContext<TInterface, TModel> where TInterface : class where TModel : class
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected AbstractReferenceDbContext(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TInterface>> GetAllAsync(IDbConnection dbConnection)
        {
            _logger.LogInformation($"Getting all {typeof(TInterface)}...");
            var items = await dbConnection.GetAllAsync<TModel>();
            return _mapper.Map<IEnumerable<TInterface>>(items);
        }
    }
}