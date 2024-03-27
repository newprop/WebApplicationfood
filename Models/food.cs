using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string foodName { get; set; }
        public string foodDescription { get; set; }
        public string foodCode { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImagePath { get; internal set; }
        public List<foodItem> Items { get; set; } = new List<foodItem>();
        
    }
}
