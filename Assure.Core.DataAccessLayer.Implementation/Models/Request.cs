using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Request), ReverseMap = true)]
    [Table("CoreFacade.Requests")]
    [CountScalarValuedFunction("CoreFacade.GetRequestCount")]
    public class Request : ObjectDocumentContainer
    {
        [Key]
        public int RequestId { get; set; }
        public string Type { get; set; }
    }
}
