using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonBook {
    class Program {
        delegate void method();
        const string hMain = "\t Телефонный справочник предприятия";
        const string hContact = "Выбирите действие для контакта:";
        static void Main(string[] args) {
            string[] items = { "Создать новый", "Удалить", "Найти", "Вывести", "Инициализация", "Выход" };
            method[] methods = new method[] { New, Delete, Find, Display, Init, Exit };
            ConsoleMenu menu = new ConsoleMenu(items);
            menu.HeaderString1 = hMain;
            menu.HeaderString2 = hContact;
            int menuResult;
            do {
                menuResult = menu.PrintMenu();
                methods[menuResult]();
                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey();
            } while (menuResult != items.Length - 1);
        }

        static void New() {
            Console.WriteLine("Выбрано действие 1");
        }
        static void Delete() {
            Console.WriteLine("Выбрано действие 2");
        }
        static void Find() {
            Console.WriteLine("Выбрано действие 3");
        }
        static void Display() {
            Console.WriteLine("Выбрано действие 3");
        }
        static void Init() {
            Console.WriteLine("Выбрано действие 3");
        }
        static void Exit() {
            Console.WriteLine("Завершение работы с приложением!");
        }
    }
    /*
    class Program {
        static void Main(string[] args) {
            var db = new CompanyPhoneBook();

            //foreach (var person in db.People.Where(p => p.Age % 2 == 0))
            //{
            //    Console.WriteLine($"{person.Name} - {person.Office.Name}");
            //}

            //var office = db.Offices.FirstOrDefault();
            //if (office == null) {
            //    office = new Office { Name = "Palo Alto" };
            //    db.Offices.Add(office);
            //}

            //office.Address += "1";

            //for (int i = 0; i < 10; i++) {
            //    db.People.Add(new Person {
            //        Name = Path.GetRandomFileName(),
            //        Age = 20 + i,
            //        Office = office
            //    });
            //}

            //db.SaveChanges();


        }
    } */
}
