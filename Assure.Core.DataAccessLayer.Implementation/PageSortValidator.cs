using Boot4ServiceCollection.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Assure.Core.DataAccessLayer.Interfaces;

namespace Assure.Core.DataAccessLayer.Implementation
{
    [AddScoped(typeof(IPageSortValidator))]
    public sealed class PageSortValidator : IPageSortValidator
    {
        private readonly ILogger<PageSortValidator> _logger;

        public PageSortValidator(ILogger<PageSortValidator> logger)
        {
            _logger = logger;
        }

        public void ValidateSortByProperty<T>(string sortBy)
        {
            _logger.LogDebug($"Validating sort by property '{sortBy}' for type '{typeof(T).FullName}'...");
            if (!typeof(T).GetProperties().Any(property => string.Equals(sortBy, property.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ValidationException($"Unknown property '{sortBy}'.");
            }
        }
    }
}
