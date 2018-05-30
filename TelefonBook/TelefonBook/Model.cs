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
                tempEmp= GetOrFindExtensionPhone(db, tempEmp, emp.ExtensionPhone.Number, emp.ExtensionPhone.InstallationSite);
                tempEmp= GetOrFindPosition(db, tempEmp, emp.Position.Name);
                tempEmp= GetOrFindSubdivison(db, tempEmp, emp.Position.Subdivison.Name);
                tempEmp= GetOrFindDivison(db, tempEmp, emp.Position.Subdivison.Divison.Name);
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
                var emp = db.Employees.ToList();
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
                Ivanov= GetOrFindExtensionPhone(db, Ivanov, "432", "Здание Управления, 3 этаж, каб. 25");
                Ivanov= GetOrFindPosition(db, Ivanov, "Главный экономист");
                Ivanov= GetOrFindSubdivison(db, Ivanov, "Плановый отдел");
                Ivanov= GetOrFindDivison(db, Ivanov, "Упраление");
                db.Employees.Add(Ivanov);

                Employee Petrov = new Employee() {
                    FirstName = "Петр",
                    MiddleName = "Петрович",
                    LastName = "Петров",
                    NumberHomePhone = "8-985-834-23-76"
                };
                Petrov= GetOrFindExtensionPhone(db, Petrov, "156", "Здание Управления, 2 этаж, каб. 11");
                Petrov= GetOrFindPosition(db, Petrov, "Оператор ЭВМ");
                Petrov= GetOrFindSubdivison(db, Petrov, "ОМТС");
                Petrov= GetOrFindDivison(db, Petrov, "Упраление");
                db.Employees.Add(Petrov);

                Employee Sidorov = new Employee() {
                    FirstName = "Егор",
                    MiddleName = "Станиславович",
                    LastName = "Сидоров",
                    NumberHomePhone = "8-910-123-38-07",
                };
                Sidorov= GetOrFindExtensionPhone(db, Sidorov, "232", "Здание Управления, 3 этаж, каб. 25");
                Sidorov= GetOrFindPosition(db, Sidorov, "Начальник отдела");
                Sidorov= GetOrFindSubdivison(db, Sidorov, "Плановый отдел");
                Sidorov= GetOrFindDivison(db, Sidorov, "Упраление");
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
        Employee GetOrFindDivison(CompanyPhoneBook db, Employee emp, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Select(t => t.Subdivison)
                               .Select(y => y.Divison)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            if (find != null) {
                emp.Position.Subdivison.Divison = find;
            } else {
                emp.Position.Subdivison.Divison = new Divison() { Name = name };
            }
            return emp;
        }
        Employee GetOrFindSubdivison(CompanyPhoneBook db, Employee emp, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Select(t => t.Subdivison)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            if (find != null) {
                emp.Position.Subdivison = find;
            } else {
                emp.Position.Subdivison = new Subdivison() { Name = name };
            }
            return emp;
        }
        Employee GetOrFindPosition(CompanyPhoneBook db, Employee emp, string name) {
            var find = db.Employees
                               .Select(e => e.Position)
                               .Where(o => o.Name == name)
                               .FirstOrDefault();
            if (find != null) {
                emp.Position = find;
            } else {
                emp.Position = new Position() { Name = name };
            }
            return emp;          
        }

        Employee GetOrFindExtensionPhone(CompanyPhoneBook db, Employee emp,  string number, string site) {
            var find = db.Employees
                               .Select(e => e.ExtensionPhone)
                                .Where(o => o.Number == number)
                                .FirstOrDefault();
            if (find != null) {
                emp.ExtensionPhone = find;
            } else {
                emp.ExtensionPhone = new ExtensionPhone() { Number = number, InstallationSite = site };
            }
            return emp;
        }
       
    }
}
