using System;

namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Quote : ObjectDocumentContainer
    {
        public int QuoteId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public override string ToString()
        {
            return $"{QuoteId}; {Product}; {StartDateTime}; {EndDateTime}";
        }
    }
}
