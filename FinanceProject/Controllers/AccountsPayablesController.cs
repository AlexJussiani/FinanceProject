using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceProject.Data;
using FinanceProject.Models;

namespace FinanceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsPayablesController : ControllerBase
    {
        private readonly CustomerContext _context;

        public AccountsPayablesController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/AccountsPayables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountsPayable>>> GetAccountsPayables()
        {
            var result = (await _context.AccountsPayables.ToListAsync()).Where(e => e.Removed == 0).ToList();
            return Enumerable.Range(0, result.Count).Select(index => new AccountsPayable
            {
                Id = result[index].Id,
                Date = result[index].Date,
                ExpirationDate = result[index].ExpirationDate,
                Status = result[index].Status,
                TotalValue = result[index].TotalValue,
                Supplier = _context.Customers.ToList().Where(e => e.Id == result[index].SupplierID).First()
            })
            .ToArray();
        }

        // GET: api/AccountsPayables/
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountsPayable>> GetAccountsPayable(int id)
        {
            var accountsPayable = await _context.AccountsPayables.FindAsync(id);

            if (accountsPayable == null)
            {
                return NotFound();
            }

            return (new AccountsPayable
            {
                Id = accountsPayable.Id,
                Date = accountsPayable.Date,
                ExpirationDate = accountsPayable.ExpirationDate,
                Status = accountsPayable.Status,
                TotalValue = accountsPayable.TotalValue,
                Supplier = _context.Customers.ToList().Where(e => e.Id == accountsPayable.SupplierID).First()
            });
        }

        [HttpGet("periodo/")]
        public async Task<ActionResult<IEnumerable<AccountsPayable>>> GetAccountsPayable(DateTime startDate, DateTime stopDate)
        {
            var result = (await _context.AccountsPayables.ToListAsync()).Where(e => e.Removed == 0 && e.Date >= startDate && e.Date <= stopDate).ToArray();

            if (result == null)
            {
                return NotFound();
            }

            return Enumerable.Range(0, result.Length).Select(index => new AccountsPayable
            {
                Id = result[index].Id,
                Date = result[index].Date,
                ExpirationDate = result[index].ExpirationDate,
                Status = result[index].Status,
                TotalValue = result[index].TotalValue,
                Supplier = _context.Customers.ToList().Where(e => e.Id == result[index].SupplierID).First()
            })
           .ToArray();
        }

        // PUT: api/AccountsPayables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountsPayable>> PutAccountsPayable(int id, AccountsPayable accountsPayable)
        {
            if (id != accountsPayable.Id)
            {
                return BadRequest();
            }

            var resultAccountsPayable = await _context.AccountsPayables.FindAsync(id);

            //if (accountsPayable.Supplier.Id != resultAccountsPayable.Supplier.Id)
            //{
            //   return Content("Não é possivel alterar o fornecedor!");
            //}           
            _context.Entry(accountsPayable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsPayableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return accountsPayable;
        }

        // POST: api/AccountsPayables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountsPayable>> PostAccountsPayable(AccountsPayable accountsPayable)
        {
            var supplier = (await _context.Customers.ToListAsync()).Where(e => e.Id == accountsPayable.SupplierID).First();
            accountsPayable.Supplier = supplier;
            _context.AccountsPayables.Add(accountsPayable);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccountsPayable), new { id = accountsPayable.Id }, new AccountsPayable
            {
                Id = accountsPayable.Id,
                Date = accountsPayable.Date,
                ExpirationDate = accountsPayable.ExpirationDate,
                Supplier = accountsPayable.Supplier,
                Status = accountsPayable.Status,
                TotalValue = accountsPayable.TotalValue              
            });
        }

        // DELETE: api/AccountsPayables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountsPayable(int id)
        {
            var accountsPayable = await _context.AccountsPayables.FindAsync(id);
            if (accountsPayable == null)
            {
                return NotFound();
            }

            _context.AccountsPayables.Remove(accountsPayable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountsPayableExists(int id)
        {
            return _context.AccountsPayables.Any(e => e.Id == id);
        }
    }
}
