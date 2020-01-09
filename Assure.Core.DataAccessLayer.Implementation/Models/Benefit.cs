using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Benefit), ReverseMap = true)]
    [Table("StaticFacade.Benefits")]
    [CountScalarValuedFunction("CoreFacade.GetBenefitCount")]
    public class Benefit : ObjectDocumentContainer
    {
        [ExplicitKey]
        public int BenefitId { get; set; }
        public string Name { get; set; }
    }
}
