namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Account : ObjectDocumentContainer
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{AccountId}; {Type}; {Name}";
        }
    }
}
