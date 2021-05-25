using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DbEntities
{
    public class AdvertImage
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long AdvertId { get; set; }

        [ForeignKey("AdvertId")]
        public AdvertItem Advert {get; set;}

        [Required]
        public string Path { get; set; }

        public bool Active { get; set; } = true;
    }
}
