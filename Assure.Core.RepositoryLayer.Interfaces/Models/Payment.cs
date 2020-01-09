using System;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Payment
    {
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
