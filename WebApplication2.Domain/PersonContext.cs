using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;


namespace WebApplication2.Domain
{
    public class PersonContext: DbContext
    {
        
            public PersonContext(DbContextOptions<PersonContext> options)
                  : base(options)
            {

            }

            public DbSet<Person> Persons { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Person>().OwnsOne(p => p.PersonAddress);
            }

    }
}
