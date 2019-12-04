using Assure.Core.DataAccessLayer.Interfaces.Models;
using Dapper.Contrib.Extensions;
using System;
using Assure.Core.DataAccessLayer.Implementation.Attributes;
using AutoMapper;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Quote), ReverseMap = true)]
    [Table("CoreFacade.Quotes")]
    [CountScalarValuedFunction("CoreFacade.GetQuoteCount")]
    public class Quote : ObjectDocumentContainer
    {
        [Key]
        public int QuoteId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
