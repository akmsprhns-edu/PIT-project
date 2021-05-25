using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public enum SortOrder
    {
        DateAsc,
        DateDesc,
        PriceAsc,
        PriceDesc
    }
    public class GetAdvertsRequestModel
    {
        public long? CategoryId { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}
