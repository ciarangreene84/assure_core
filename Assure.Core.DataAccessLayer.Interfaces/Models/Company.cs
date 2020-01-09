namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Company : ObjectDocumentContainer
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{CompanyId}; {Name}";
        }
    }
}
