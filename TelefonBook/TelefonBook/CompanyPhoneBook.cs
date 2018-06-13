using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TelefonBook {
    public class CompanyPhoneBook : DbContext {
        public CompanyPhoneBook() : base("CompanyPhoneBook") {
           // this.Configuration.LazyLoadingEnabled = false;
        }

        static CompanyPhoneBook() {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CompanyPhoneBook>());
        }
        public DbSet<Employee>Employees{ get; set;}
        public DbSet<ExtensionPhone> ExtensionPhones { get; set; }
        public DbSet<Divison> Divisons { get; set; }
        public DbSet<Subdivison> Subdivisons { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}