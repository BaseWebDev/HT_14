using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonBook {
    class Program {
        static void Main(string[] args) {
            var db = new CompanyPhoneBook();

            //foreach (var person in db.People.Where(p => p.Age % 2 == 0))
            //{
            //    Console.WriteLine($"{person.Name} - {person.Office.Name}");
            //}

            var office = db.Offices.FirstOrDefault();
            if (office == null) {
                office = new Office { Name = "Palo Alto" };
                db.Offices.Add(office);
            }

            office.Address += "1";

            for (int i = 0; i < 10; i++) {
                db.People.Add(new Person {
                    Name = Path.GetRandomFileName(),
                    Age = 20 + i,
                    Office = office
                });
            }

            db.SaveChanges();


        }
    }
}
