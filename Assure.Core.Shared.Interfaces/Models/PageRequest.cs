using System.ComponentModel.DataAnnotations;

namespace Assure.Core.Shared.Interfaces.Models
{
    public sealed class PageRequest
    {
        public string Filter { get; set; }

        public uint PageIndex { get; set; }

        public uint PageSize { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SortBy { get; set; }

        public SortOrders SortOrder { get; set; }

        public override string ToString()
        {
            return $"{PageIndex}; {PageSize}; {SortBy}; {SortOrder}";
        }
    }
}
