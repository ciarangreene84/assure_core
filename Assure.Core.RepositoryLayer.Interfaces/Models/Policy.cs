using System;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
