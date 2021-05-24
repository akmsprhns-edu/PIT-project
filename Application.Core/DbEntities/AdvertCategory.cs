using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DbEntities
{
    public class AdvertCategory
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public AdvertCategory ParentCategory { get; set; }

        public List<AdvertCategory> SubCategories { get; set; }
    }
}
