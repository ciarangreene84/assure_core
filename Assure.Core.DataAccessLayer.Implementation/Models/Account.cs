using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Account), ReverseMap = true)]
    [Table("CoreFacade.Accounts")]
    [CountScalarValuedFunction("CoreFacade.GetAccountCount")]
    public class Account : ObjectDocumentContainer
    {
        [Key]
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
