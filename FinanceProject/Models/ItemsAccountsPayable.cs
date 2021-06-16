using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceProject.Models
{
    public class ItemsAccountsPayable
    {
        [Key]
        public int Id { get; set; }
        public virtual AccountsPayable accountsPayable { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal UnitareValue { get; set; }
        public decimal Value { get; set; }
        [Required]
        public int Removed { get; set; }
    }
}
