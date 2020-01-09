using System;

namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Policy : ObjectDocumentContainer
    {
        public int PolicyId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public override string ToString()
        {
            return $"{PolicyId}; {Product}; {StartDateTime}; {EndDateTime}";
        }
    }
}
