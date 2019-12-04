namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Agent : ObjectDocumentContainer
    {
        public int AgentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{AgentId}; {Type}; {Name}";
        }
    }
}
