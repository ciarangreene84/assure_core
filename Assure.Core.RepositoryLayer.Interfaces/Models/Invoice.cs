using System;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string Identifier { get; set; }
        public string Product { get; set; }
        public string CurrencyAlpha3 { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
