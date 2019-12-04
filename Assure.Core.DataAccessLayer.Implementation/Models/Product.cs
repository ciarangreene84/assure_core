using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Product), ReverseMap = true)]
    [Table("StaticFacade.Products")]
    [CountScalarValuedFunction("CoreFacade.GetProductCount")]
    public class Product : ObjectDocumentContainer
    {
        [ExplicitKey]
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
}
