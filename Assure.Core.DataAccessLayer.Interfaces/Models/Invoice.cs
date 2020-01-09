using System;

namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Invoice : ObjectDocumentContainer
    {
        public int InvoiceId { get; set; }
        public string Identifier { get; set; }
        public string Product { get; set; }
        public string CurrencyAlpha3 { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public override string ToString()
        {
            return $"{Identifier}; {Product}; {DateTime}";
        }
    }
}
