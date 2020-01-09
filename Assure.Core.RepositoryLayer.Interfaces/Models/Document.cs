using System;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset LastWrite { get; set; }
        public byte[] Data { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
