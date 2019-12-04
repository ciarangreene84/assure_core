namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Lead : ObjectDocumentContainer
    {
        public int LeadId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{LeadId}; {Name}";
        }
    }
}
