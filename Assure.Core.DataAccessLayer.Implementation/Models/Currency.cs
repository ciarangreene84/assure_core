using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Currency), ReverseMap = true)]
    [Table("[StaticFacade].[Currencies]")]
    public class Currency
    {
        [Key]
        public string Alpha3 { get; set; }
        public string Name { get; set; }
        
    }
}
