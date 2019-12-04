namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Card : ObjectDocumentContainer
    {
        public int CardId { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"{CardId}; {Number}";
        }
    }
}
