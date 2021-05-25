using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DbEntities
{
    public class AdvertItem
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Descirption { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public long CategoryId { get; set; }


        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        [ForeignKey("CategoryId")]
        public AdvertCategory Category { get; set; }

        public List<AdvertImage> Images { get; set; }
    }
}
