using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Lead), ReverseMap = true)]
    [Table("CoreFacade.Leads")]
    [CountScalarValuedFunction("CoreFacade.GetLeadCount")]
    public class Lead : ObjectDocumentContainer
    {
        [Key]
        public int LeadId { get; set; }
        public string Name { get; set; }
    }
}
