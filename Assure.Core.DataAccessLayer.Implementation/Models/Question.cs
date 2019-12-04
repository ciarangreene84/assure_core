using Assure.Core.DataAccessLayer.Implementation.Attributes;
using Assure.Core.DataAccessLayer.Interfaces.Models;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Question), ReverseMap = true)]
    [Table("StaticFacade.Questions")]
    [CountScalarValuedFunction("CoreFacade.GetQuestionCount")]
    public class Question : ObjectDocumentContainer
    {
        [Key]
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
