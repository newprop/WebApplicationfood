using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class foodItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public food? food { get; set; }
        public int? foodID { get; set; }

      
        public decimal ItemTotal => this.UnitPrice * this.Quantity;
    }
}