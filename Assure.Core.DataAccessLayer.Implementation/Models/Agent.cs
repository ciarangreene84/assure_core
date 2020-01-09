using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Agent), ReverseMap = true)]
    [Table("CoreFacade.Agents")]
    [CountScalarValuedFunction("CoreFacade.GetAgentCount")]
    public class Agent : ObjectDocumentContainer
    {
        [Key]
        public int AgentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
