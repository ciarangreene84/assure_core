using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [Table("StaticFacade.AgentTypes")]
    public class AgentType
    {
        [Key]
        public int AgentTypeId { get; set; }
        public string Name { get; set; }
    }
}
