using System;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
