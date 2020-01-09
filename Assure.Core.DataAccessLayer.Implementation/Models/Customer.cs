using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Customer), ReverseMap = true)]
    [Table("CoreFacade.Customers")]
    [CountScalarValuedFunction("CoreFacade.GetCustomerCount")]
    public class Customer : ObjectDocumentContainer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}
