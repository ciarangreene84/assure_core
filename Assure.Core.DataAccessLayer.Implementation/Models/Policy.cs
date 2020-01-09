using Assure.Core.DataAccessLayer.Interfaces.Models;
using Dapper.Contrib.Extensions;
using System;
using Assure.Core.DataAccessLayer.Implementation.Attributes;
using AutoMapper;

namespace Assure.Core.DataAccessLayer.Implementation.Models
{
    [AutoMap(typeof(Interfaces.Models.Policy), ReverseMap = true)]
    [Table("CoreFacade.Policies")]
    [CountScalarValuedFunction("CoreFacade.GetPolicyCount")]
    public class Policy : ObjectDocumentContainer
    {
        [Key]
        public int PolicyId { get; set; }
        public string Product { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
