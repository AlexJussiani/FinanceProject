using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceProject.Models
{
    public class AccountsMovimentations
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public virtual AccountsPayable accountsPayable { get; set; }
        [Required]
        public virtual AccountsReceivable accountsReceivable { get; set; }
        public string Description { get; set; }
        public int type { get; set; }
        public decimal Value { get; set; }
    }
}
