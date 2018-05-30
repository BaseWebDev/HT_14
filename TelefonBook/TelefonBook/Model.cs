using System.Collections.Generic;
using System.Linq;

namespace TelefonBook {
    public class Model {
        /// <summary>
        /// Создать новый контакт
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        public void New(Employee emp) {
            using (var db = new CompanyPhoneBook()) {
                Employee tempEmp = new Employee() {
                    FirstName = emp.FirstName,
                    MiddleName = emp.MiddleName,
                    LastName = emp.LastName,
                    NumberHomePhone = emp.NumberHomePhone,
                };
                tempEmp.ExtensionPhone = GetOrFindExtensionPhone(db, emp.ExtensionPhone.Number, emp.ExtensionPhone.InstallationSite);
                tempEmp.Position = GetOrFindPosition(db, emp.Position.Name);
                tempEmp.Position.Subdivison = GetOrFindSubdivison(db, emp.Position.Subdivison.Name);
                tempEmp.Position.Subdivison.Divison = GetOrFindDivison(db, emp.Position.Subdivison.Divison.Name);
                db.Employees.Add(tempEmp);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Удалить 
        /// </summary>
        /// <param name="nameOrExtensionPhone">Фамилия или внутренний номер абонента</param>
        public void Delete(string nameOrExtensionPhone) {   
                using (var db = new CompanyPhoneBook()) {
                    foreach (var emp in Find(nameOrExtensionPhone)) {
                        db.Employees.Remove(emp);
                    }
                    db.SaveChanges();
                }
        }
        /// <summary>
        /// Поиск 
        /// </summary>
        /// <param name="nameOrExtensionPhone">Фамилия или внутренний номер абонента</param>
        public List<Employee> Find(string nameOrExtensionPhone) {
            using (var db = new CompanyPhoneBook()) {
                return db.Employees
                                .Where(o => o.LastName.Contains(nameOrExtensionPhone) || o.ExtensionPhone.Number.Contains(nameOrExtensionPhone))
                                .OrderBy(x => x.ExtensionPhone.Number)
                                .ToList();
                //foreach (var emp in emps) {
                //    yield return emp;
                //    //     Console.WriteLine("{0}. {1} {2} - {4} - {3}", emp.Id, emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name);
                //}
            }
        }
        /// <summary>
        /// Отобразить все контакты
        /// </summary>
        /// <returns></returns>
        public List<Employee> ShowAll() {
            using (var db = new CompanyPhoneBook()) {
                var emp = db.Employees.Select(x => x).ToList();
                return emp;
                //foreach (var emp in emps) { 
                //    yield return emp;
                //}  // var emps = 
            }
            // Console.WriteLine("{0}. {1} {2} - {4} - {3}", emp.Id, emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name);
        }
        /// <summary>
        /// Инициализация БД
        /// </summary>
        public void InitTest() {
            using (var db = new CompanyPhoneBook()) {
                Employee Ivanov = new Employee() {
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    LastName = "Иванов",
                    NumberHomePhone = "8-915-841-30-08",
                };
                Ivanov.ExtensionPhone = GetOrFindExtensionPhone(db, "432", "Здание Управления, 3 этаж, каб. 25");
                Ivanov.Position = GetOrFindPosition(db, "Главный экономист");
                Ivanov.Position.Subdivison = GetOrFindSubdivison(db, "Плановый отдел");
                Ivanov.Position.Subdivison.Divison = GetOrFindDivison(db, "Упраление");
                db.Employees.Add(Ivanov);

                Employee Petrov = new Employee() {
                    FirstName = "Петр",
                    MiddleName = "Петрович",
                    LastName = "Петров",
                    NumberHomePhone = "8-985-834-23-76"
                };
                Petrov.ExtensionPhone = GetOrFindExtensionPhone(db, "156", "Здание Управления, 2 этаж, каб. 11");
                Petrov.Position = GetOrFindPosition(db, "Оператор ЭВМ");
                Petrov.Position.Subdivison = GetOrFindSubdivison(db, "ОМТС");
                Petrov.Position.Subdivison.Divison = GetOrFindDivison(db, "Упраление");
                db.Employees.Add(Petrov);

                Employee Sidorov = new Employee() {
                    FirstName = "Егор",
                    MiddleName = "Станиславович",
                    LastName = "Сидоров",
                    NumberHomePhone = "8-910-123-38-07",
                };
                Sidorov.ExtensionPhone = GetOrFindExtensionPhone(db, "232", "Здание Управления, 3 этаж, каб. 25");
                Sidorov.Position = GetOrFindPosition(db, "Начальник отдела");
                Sidorov.Position.Subdivison = GetOrFindSubdivison(db, "Плановый отдел");
                Sidorov.Position.Subdivison.Divison = GetOrFindDivison(db, "Упраление");
                db.Employees.Add(Sidorov);
                db.SaveChanges();
            }
        }

        Employee GetOrFindEmployee(CompanyPhoneBook db, string firstName, string lastName, string middleName) {
            var find = db.Employees
                               .Where(o => o.FirstName == firstName & o.LastName== lastName & o.MiddleName==middleName)
                               .FirstOrDefault();
            return find != null ? find : new Employee() { FirstName = firstName, LastName = lastName, MiddleName = middleName };
        }
        Divison GetOrFindDivison(CompanyPhoneBook db, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Select(t => t.Subdivison)
                               .Select(y => y.Divison)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            return find != null ? find : new Divison() {Name = name};
        }
        Subdivison GetOrFindSubdivison(CompanyPhoneBook db, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Select(t => t.Subdivison)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            return find != null ? find : new Subdivison() { Name = name };
        }
        Position GetOrFindPosition(CompanyPhoneBook db, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            return find != null ? find : new Position() { Name = name };
        }

        ExtensionPhone GetOrFindExtensionPhone(CompanyPhoneBook db, string number, string site) {
            var find = db.Employees
                               .Select(e => e.ExtensionPhone)
                                .Where(o => o.Number == number)
                                .FirstOrDefault();
            //if (find != null) {
            //    emp
            //}
            return find != null ? find : new ExtensionPhone() { Number = number, InstallationSite = site };
        }
       
    }
}
