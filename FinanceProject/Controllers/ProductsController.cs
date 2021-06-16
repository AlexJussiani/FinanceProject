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
    public class ProductsController : ControllerBase
    {
        private readonly CustomerContext _context;

        public ProductsController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return (await _context.Products.ToListAsync()).Where(e => e.removed == 0).ToArray();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
        {
            var product = (await _context.Products.ToListAsync()).Where(e => e.Id == id).ToArray();

            if (product == null)
            {
                return NotFound();
            }

            return Enumerable.Range(0, product.Length).Select(index => new Product
            {
                Id = product[index].Id,
                Name = product[index].Name,
                Description = product[index].Description,
                SaleValue = product[index].SaleValue,
                PriceValue = product[index].PriceValue,
                Type = product[index].Type
            })
         .ToArray();
        }

        [HttpGet("produto/{search}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductSeacrh(string search)
        {
            var product = (await _context.Products.ToListAsync()).Where(e => e.Name.ToLower().Contains(search.ToLower())  && e.removed == 0).ToArray();


            return Enumerable.Range(0, product.Length).Select(index => new Product
            {
                Id = product[index].Id,
                Name = product[index].Name,
                Description = product[index].Description,
                SaleValue = product[index].SaleValue,
                PriceValue = product[index].PriceValue,
                Type = product[index].Type
            })
         .ToArray();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SaleValue = product.SaleValue,
                PriceValue = product.PriceValue,
                Type = product.Type
            };
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SaleValue = product.SaleValue,
                PriceValue = product.PriceValue,
                Type = product.Type
            });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("");
            }
            product.removed = product.Id;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
