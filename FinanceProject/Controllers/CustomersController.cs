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
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var result = (await _context.Customers.ToListAsync()).Where(e => e.Removed == 0).ToList();
            return Enumerable.Range(0, result.Count).Select(index => new Customer
            {
                Id = result[index].Id,
                Name = result[index].Name,
                Cpf = result[index].Cpf,
                Phone = result[index].Phone
            })
            .ToArray();
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(int id)
        {
            var customer = (await _context.Customers.ToListAsync()).Where(e => e.Id == id).First();

            if (customer == null)
            {
                return NotFound();
            }

            return Enumerable.Range(0, 1).Select(index => new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Cpf = customer.Cpf,
                Phone = customer.Phone
            })
            .ToArray();
        }

        // GET: api/Customers/name
        [HttpGet("names/{search}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(string search)
        {
            var customer = (await _context.Customers.ToListAsync()).Where(e => e.Removed == 0  && (e.Cpf.Contains(search) || e.Name.ToLower().Contains(search.ToLower()))).ToArray();
          
            if (customer == null)
            {
                return NotFound();
            }

            return Enumerable.Range(0, customer.Length).Select(index => new Customer
            {
                Id = customer[index].Id,
                Name = customer[index].Name,
                Cpf = customer[index].Cpf,
                Phone = customer[index].Phone
            })
         .ToArray();
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return customer;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            string texto = customer.Cpf;
            if (!CustomerExistsCPF(customer.Cpf))
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);                             
            } else
                return Content("Este CPF Já Cadastrado na base");
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Removed = customer.Id;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExistsCPF(string cpf) {
            return _context.Customers.Any(e => e.Cpf == cpf);
        }
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
