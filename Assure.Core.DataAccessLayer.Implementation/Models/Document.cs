using System;
using Assure.Core.DataAccessLayer.Implementation.Attributes;
using AutoMapper;
using Dapper.Contrib.Extensions;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Document), ReverseMap = true)]
    [Table("CoreFacade.Documents")]
    [CountScalarValuedFunction("CoreFacade.GetDocumentCount")]
    public class Document
    {
        [ExplicitKey]
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset LastWrite { get; set; }
        public byte[] Data { get; set; }
    }
}
