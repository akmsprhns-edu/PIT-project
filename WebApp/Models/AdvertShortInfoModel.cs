using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AdvertShortInfoModel
    {
        public long AdvertId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
