using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Card), ReverseMap = true)]
    [Table("CoreFacade.Cards")]
    [CountScalarValuedFunction("CoreFacade.GetCardCount")]
    public class Card : ObjectDocumentContainer
    {
        [Key]
        public int CardId { get; set; }
        public int Number { get; set; }
    }
}
