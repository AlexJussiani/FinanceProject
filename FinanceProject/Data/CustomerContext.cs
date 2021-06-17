using FinanceProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FinanceProject.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountsMovimentations> AccountsMovimentations { get; set; }
        public DbSet<AccountsPayable> AccountsPayables { get; set; }
        public DbSet<AccountsReceivable> accountsReceivables { get; set; }
        public DbSet<ItemsAccountsPayable> ItemsAccountsPayables { get; set; }
        public DbSet<ItemsAccountsReceivable> ItemsAccountsReceivables { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
