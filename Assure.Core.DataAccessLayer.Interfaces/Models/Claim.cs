namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Claim : ObjectDocumentContainer
    {
        public int ClaimId { get; set; }
        public int PolicyId { get; set; }

        public override string ToString()
        {
            return $"{ClaimId}; {PolicyId}";
        }
    }
}
