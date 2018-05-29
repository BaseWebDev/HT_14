using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonBook;

namespace ConsoleView {
    
        class Program {
            delegate void method();
            delegate void methodFind();
            const string hMain = "\t Телефонный справочник предприятия";
            const string hContact = "Выбирите действие для контакта:";
            const string fContact = "Выбирите способ поиска контакта:";
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
                var db = new CompanyPhoneBook();
                Employee emp = new Employee();
                Console.Write("Добавить контакт сотрудника."); Console.WriteLine("Введите:");
                Console.Write("Фамилия: "); emp.LastName = Console.ReadLine();
                Console.Write("Имя: "); emp.FirstName = Console.ReadLine();
                Console.Write("Отчество: "); emp.MiddleName = Console.ReadLine();
                Console.Write("Домашний телефон: "); emp.NumberHomePhone = Console.ReadLine();

                Console.Write("Внутренний телефон: "); string empExtensionPhoneNumber = Console.ReadLine();
                ExtensionPhone findExtensionPhone = db.Employees
                                    .Select(e => e.ExtensionPhone)
                                    .Where(o => o.Number == empExtensionPhoneNumber)
                                    .FirstOrDefault();
                if (findExtensionPhone != null) {
                    emp.ExtensionPhone = findExtensionPhone;
                } else {
                    emp.ExtensionPhone = new ExtensionPhone();
                    emp.ExtensionPhone.Number = empExtensionPhoneNumber;
                    Console.Write("Место установки: "); emp.ExtensionPhone.InstallationSite = Console.ReadLine();
                }
                Console.Write("Должность: "); string empPositionName = Console.ReadLine();
                Position findEmpPosition = db.Employees
                                    .Select(e => e.Position)
                                    .Where(o => o.Name == empPositionName)
                                    .FirstOrDefault();
                if (findEmpPosition != null) {
                    emp.Position = findEmpPosition;
                } else {
                    emp.Position = new Position();
                    emp.Position.Name = empPositionName;
                }
                Console.Write("Отдел: "); string empPositionSubdivisonName = Console.ReadLine();
                Subdivison findEmpPositionSubdivison = db.Employees
                                    .Select(e => e.Position)
                                    .Select(t => t.Subdivison)
                                    .Where(o => o.Name == empPositionSubdivisonName)
                                    .FirstOrDefault();
                if (findEmpPositionSubdivison != null) {
                    emp.Position.Subdivison = findEmpPositionSubdivison;
                } else {
                    emp.Position.Subdivison = new Subdivison();
                    emp.Position.Subdivison.Name = empPositionSubdivisonName;
                }
                // emp.Position.Subdivison.Divison = new Divison();
                Console.Write("Подразделение: "); string empPositionSubdivisonDivisonName = Console.ReadLine();
                Divison findEmpPositionSubdivisonDivison = db.Employees
                                    .Select(e => e.Position)
                                    .Select(t => t.Subdivison)
                                    .Select(y => y.Divison)
                                    .Where(o => o.Name == empPositionSubdivisonDivisonName)
                                    .FirstOrDefault();
                if (findEmpPositionSubdivisonDivison != null) {
                    emp.Position.Subdivison.Divison = findEmpPositionSubdivisonDivison;
                } else {
                    emp.Position.Subdivison.Divison = new Divison();
                    emp.Position.Subdivison.Divison.Name = empPositionSubdivisonDivisonName;
                }
                db.Employees.Add(emp);
                db.SaveChanges();
            }
            static void Delete() {
                Display();
                Console.Write("Введите номер удаляемой строки: ");
                var db = new CompanyPhoneBook();
                int idDelete;
                if (Int32.TryParse(Console.ReadLine(), out idDelete)) {
                    Employee emp = db.Employees
                                    .Where(o => o.Id == idDelete)
                                    .FirstOrDefault();
                    if (emp != null) {
                        db.Employees.Remove(emp);
                        db.SaveChanges();
                        Console.WriteLine("Строка удачно удалена");
                    } else {
                        Console.WriteLine("Нет такой строки!");
                    }
                }
            }
            static void Find() {
                string[] items = { "Поиск по сотруднику", "Поиск по номеру", "Выход" };
                methodFind[] methods = new methodFind[] { FindEmployee, FindExtensionPhone, FindExit };
                ConsoleMenu menu = new ConsoleMenu(items);
                menu.HeaderString1 = hMain;
                menu.HeaderString2 = fContact;
                int menuResult;
                do {
                    menuResult = menu.PrintMenu();
                    methods[menuResult]();
                    Console.WriteLine("Для продолжения нажмите любую клавишу");
                    Console.ReadKey();
                } while (menuResult != items.Length - 1);
            }
            /// <summary>
            /// Поиск по сотруднику
            /// </summary>
            private static void FindEmployee() {
                Console.Write("Введите фамилию сотрудника: ");
                string lastName = Console.ReadLine();
                var db = new CompanyPhoneBook();
                var emps = db.Employees
                                    .Where(o => o.LastName == lastName)
                                    .OrderBy(x => x.ExtensionPhone.Number)
                                    .ToList();
                if (emps.Count > 0) {
                    foreach (var emp in emps) {
                        Console.WriteLine("Контакт найден");
                        Console.WriteLine("{0}. {1} {2} - {4} - {3}", emp.Id, emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name);
                    }
                } else {
                    Console.WriteLine("Нет такого контакта!");
                }
                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.WriteLine();
                return;
            }
            /// <summary>
            /// Поиск по внутреннему номеру телефона
            /// </summary>
            private static void FindExtensionPhone() {
                Console.Write("Введите внутренний номер телефона сотрудника: ");
                string number = Console.ReadLine();
                var db = new CompanyPhoneBook();
                var emps = db.Employees
                                    .Where(o => o.ExtensionPhone.Number == number)
                                    .OrderBy(x => x.ExtensionPhone.Number)
                                    .ToList();
                if (emps.Count > 0) {
                    foreach (var emp in emps) {
                        Console.WriteLine("Контакт найден");
                        Console.WriteLine("{0}. {1} {2} - {4} - {3}", emp.Id, emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name);
                    }
                } else {
                    Console.WriteLine("Нет такого внутреннего номера!");
                }
                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.WriteLine();
                return;
            }
            /// <summary>
            /// Выход
            /// </summary>
            private static void FindExit() {
                return;
            }

            static void Display() {
                Console.WriteLine("Вывести номера справочника");
                using (var db = new CompanyPhoneBook()) {
                    var emps = db.Employees.ToList();
                    foreach (var emp in emps) {
                        Console.WriteLine("{0}. {1} {2} - {4} - {3}", emp.Id, emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name);
                    }
                }
            }
            static void Init() {
                Console.WriteLine("Инициализация БД");
                var db = new CompanyPhoneBook();
                var division = new Divison() { Name = "Управление" };
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
                            Divison = division
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
                            Divison = division
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
                            Divison = division
                        }
                    }
                };
                db.Employees.Add(Sidorov);

                db.SaveChanges();
                Console.WriteLine("Инициализация БД завершена.");
            }
            static void Exit() {
                Console.WriteLine("Завершение работы с приложением!");
            }
        }
}
