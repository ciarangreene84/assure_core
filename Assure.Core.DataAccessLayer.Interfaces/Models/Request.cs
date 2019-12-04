namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Request : ObjectDocumentContainer
    {
        public int RequestId { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{RequestId}; {Type}";
        }
    }
}
