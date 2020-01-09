namespace Assure.Core.DataAccessLayer.Interfaces.Models
{
    public class Question : ObjectDocumentContainer
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
