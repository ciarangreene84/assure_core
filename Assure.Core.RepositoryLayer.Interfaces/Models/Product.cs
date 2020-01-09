using System.Collections.Generic;

namespace Assure.Core.RepositoryLayer.Interfaces.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Benefit> Benefits { get; set; }
    }
}
