using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;
using System;
using Assure.Core.DataAccessLayer.Implementation.Attributes;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Payment), ReverseMap = true)]
    [Table("CoreFacade.Payments")]
    [CountScalarValuedFunction("CoreFacade.GetPaymentCount")]
    public class Payment : ObjectDocumentContainer
    {
        [Key]
        public int PaymentId { get; set; }
        public string Identifier { get; set; }
        public string CurrencyAlpha3 { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public override string ToString()
        {
            return $"{Identifier}; {CurrencyAlpha3}; {Amount}";
        }
    }
}
