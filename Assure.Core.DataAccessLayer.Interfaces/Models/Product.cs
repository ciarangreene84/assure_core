namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Product : ObjectDocumentContainer
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{ProductId}; {Name}";
        }
    }
}
