using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerInfo.Models
{
    public class CustomersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=customers.db");
        }
    }
    
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
        public string GivenName { get; set; }
        public string MiddleInitial { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
    }

}
