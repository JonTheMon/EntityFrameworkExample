using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Models
{
    public class AppDbContext : DbContext
   {
      public DbSet<Contact> Contacts { get; set; }
      public DbSet<Address> Addresses { get; set; }


      public override int SaveChanges()
      {

         ChangeTracker.DetectChanges();

         foreach (EntityEntry entry in ChangeTracker.Entries())
         {

         }

         return base.SaveChanges();
      }
   }
}

