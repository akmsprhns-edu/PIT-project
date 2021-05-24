using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class NewAdvertModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
