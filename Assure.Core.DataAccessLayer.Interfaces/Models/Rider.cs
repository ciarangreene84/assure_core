namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Benefit : ObjectDocumentContainer
    {
        public int BenefitId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{BenefitId}; {Name}";
        }
    }
}
