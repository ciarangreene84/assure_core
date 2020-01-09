using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Claim), ReverseMap = true)]
    [Table("CoreFacade.Claims")]
    [CountScalarValuedFunction("CoreFacade.GetClaimCount")]
    public class Claim : ObjectDocumentContainer
    {
        [Key]
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }
    }
}
