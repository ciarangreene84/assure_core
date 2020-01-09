using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Company), ReverseMap = true)]
    [Table("CoreFacade.Companies")]
    [CountScalarValuedFunction("CoreFacade.GetCompanyCount")]
    public class Company : ObjectDocumentContainer
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
    }
}
