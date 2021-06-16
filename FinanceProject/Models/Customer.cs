using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        public string Phone { get; set; }
        [Required]
        public int Removed { get; set; }
    }
}
