using Assure.Core.DataAccessLayer.Interfaces.Models;
using Dapper.Contrib.Extensions;
using System;
using Assure.Core.DataAccessLayer.Implementation.Attributes;
using AutoMapper;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Invoice), ReverseMap = true)]
    [Table("CoreFacade.Invoices")]
    [CountScalarValuedFunction("CoreFacade.GetInvoiceCount")]
    public class Invoice : ObjectDocumentContainer
    {
        [Key]
        public int InvoiceId { get; set; }
        public string Identifier { get; set; }
        public string Product { get; set; }
        public string CurrencyAlpha3 { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
