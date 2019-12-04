namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Customer : ObjectDocumentContainer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{CustomerId}; {Name}";
        }
    }
}
