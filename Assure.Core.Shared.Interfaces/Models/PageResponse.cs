using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assure.Core.Shared.Interfaces.Models
{
    public sealed class PageResponse<T>
    {
        public uint PageIndex { get; set; }

        public uint PageSize { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SortBy { get; set; }

        public SortOrders SortOrder { get; set; }

        public IEnumerable<T> Items { get; set; }
        public int TotalItemCount { get; set; }

        public PageResponse()
        {

        }

        public PageResponse(PageRequest pageRequest)
        {
            PageIndex = pageRequest.PageIndex;
            PageSize = pageRequest.PageSize;
            SortBy = pageRequest.SortBy;
            SortOrder = pageRequest.SortOrder;
        }
    }
}
