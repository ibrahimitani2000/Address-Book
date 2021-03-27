using Address_Book.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book.Data
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Organization> Organization { get; set; }

    }
}
