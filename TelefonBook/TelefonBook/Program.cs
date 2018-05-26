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
            Console.WriteLine("Инициализация БД");
            var db = new CompanyPhoneBook();
            Employee Ivanov = new Employee() {
                FirstName = "Иван",
                MiddleName = "Иванович",
                LastName = "Иванов",
                NumberHomePhone = "8-915-841-30-08",
                ExtensionPhone = new ExtensionPhone() {
                    Number = "432",
                    InstallationSite = "Здание Управления, 4 этаж, каб. 30"
                },
                Position = new Position() {
                    Name = "Главный экономист",
                    Subdivison = new Subdivison() {
                        Name = "Плановый отдел",
                        Divison = new Divison() {
                            Name = "Управление",
                        }
                    }
                }
            };
            db.Employees.Add(Ivanov);

            Employee Petrov = new Employee() {
                FirstName = "Петр",
                MiddleName = "Петрович",
                LastName = "Петров",
                NumberHomePhone = "8-985-834-23-76",
                ExtensionPhone = new ExtensionPhone() {
                    Number = "156",
                    InstallationSite = "Здание Управления, 2 этаж, каб. 11"
                },
                Position = new Position() {
                    Name = "Оператор ЭВМ",
                    Subdivison = new Subdivison() {
                        Name = "ОМТС",
                        Divison = new Divison() {
                            Name = "Управление",
                        }
                    }
                }
            };
            db.Employees.Add(Petrov);

            Employee Sidorov = new Employee() {
                FirstName = "Иван",
                MiddleName = "Станиславович",
                LastName = "Сидоров",
                NumberHomePhone = "8-910-123-38-07",
                ExtensionPhone = new ExtensionPhone() {
                    Number = "232",
                    InstallationSite = "Здание Управления, 3 этаж, каб. 25"
                },
                Position = new Position() {
                    Name = "Начальник отдела",
                    Subdivison = new Subdivison() {
                        Name = "Плановый отдел",
                        Divison = new Divison() {
                            Name = "Управление",
                        }
                    }
                }
            };
            db.Employees.Add(Sidorov);

            db.SaveChanges();
        }
        static void Exit() {
            Console.WriteLine("Завершение работы с приложением!");
        }
    }
}
