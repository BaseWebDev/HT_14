using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TelefonBook {
    class CompanyPhoneBook : DbContext {
        CompanyPhoneBook() : base("CompanyPhoneBook") { }
        public DbSet<Employee>Employees{ get; set;}
        public DbSet<ExtensionPhone> ExtensionPhones { get; set; }
        public DbSet<Divison> Divisons { get; set; }
        public DbSet<Subdivison> Subdivisons { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}