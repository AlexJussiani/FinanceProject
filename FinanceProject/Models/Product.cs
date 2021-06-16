using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SaleValue { get; set; }
        public decimal PriceValue { get; set; }
        public string Type { get; set; }
        [Required]
        public int removed { get; set; }
    }
}
