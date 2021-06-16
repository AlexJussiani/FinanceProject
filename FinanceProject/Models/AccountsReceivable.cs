using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceProject.Models
{
    public class AccountsReceivable
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }
        public decimal TotalValue { get; set; }
        public string Status { get; set; }
        [Required]
        public int Removed { get; set; }
    }
}
