using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerInfo.Models;

namespace CustomerInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {        
        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            var context = new CustomersContext();
            return context.Customers;
            //return new Customer {CustomerID=1, Gender ="asdf", Title ="sdf", Occupation ="asdf", Company ="asf", GivenName ="asf", MiddleInitial ="sf", EmailAddress="asdf@asf", Surname="sdf" };
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var context = new CustomersContext();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            var context = new CustomersContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            context.Entry(customer).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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

            return NoContent();
        }


        // POST: api/Customers
        [HttpPost("{id}")]
        public async Task<IActionResult> PostCustomer(int id, [FromBody] Customer customer)
        {
            var context = new CustomersContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Customers
        [HttpPost]        
        public async Task<IActionResult> PostCustomer([FromBody] List<Customer> customers)
        {
            var context = new CustomersContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (Customer customer in customers)
                {
                    context.Customers.Add(customer);
                }
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(); 
            }

            return Ok(customers.Count.ToString());
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var context = new CustomersContext();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            var context = new CustomersContext();
            return context.Customers.Any(e => e.CustomerID == id);
        }
    }
}