using System.Collections.Generic;
using System.Linq;

namespace TelefonBook {
    public class Model {
        CompanyPhoneBook db = null;
        public Model() {
            db = new CompanyPhoneBook();
        }
        /// <summary>
        /// Создать новый контакт
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        public void New(Employee emp) {
            Employee tempEmp = GetOrFindEmployee(emp.FirstName, emp.MiddleName, emp.LastName, emp.NumberHomePhone);
            tempEmp = GetOrFindExtensionPhone(tempEmp, emp.ExtensionPhone.Number, emp.ExtensionPhone.InstallationSite);
            tempEmp = GetOrFindPosition(tempEmp, emp.Position.Name);
            tempEmp = GetOrFindSubdivison(tempEmp, emp.Position.Subdivison.Name);
            tempEmp = GetOrFindDivison(tempEmp, emp.Position.Subdivison.Divison.Name);
            db.Employees.Add(tempEmp);
            // db.Employees.Add(emp);
            db.SaveChanges();
        }
        /// <summary>
        /// Удалить 
        /// </summary>
        /// <param name="nameOrExtensionPhone">Фамилия или внутренний номер абонента</param>
        public void Delete(string nameOrExtensionPhone) {   
             foreach (var emp in Find(nameOrExtensionPhone)) {
                        db.Employees.Remove(emp);
                    }
                    db.SaveChanges();
        }
        /// <summary>
        /// Поиск 
        /// </summary>
        /// <param name="nameOrExtensionPhone">Фамилия или внутренний номер абонента</param>
        public List<Employee> Find(string nameOrExtensionPhone) {
            return db.Employees
                .Where(o => o.LastName.Contains(nameOrExtensionPhone) || o.ExtensionPhone.Number.Contains(nameOrExtensionPhone))
                .OrderBy(x => x.ExtensionPhone.Number)
                .ToList();
        }
        /// <summary>
        /// Отобразить все контакты
        /// </summary>
        /// <returns></returns>
        public List<Employee> ShowAll() {
            var emp = db.Employees.ToList();
            return emp;
        }
        /// <summary>
        /// Инициализация БД
        /// </summary>
        public void InitTest() {
            Employee Ivanov = new Employee() {
                FirstName = "Иван",
                MiddleName = "Иванович",
                LastName = "Иванов",
                NumberHomePhone = "8-915-841-30-08",
            };
            Ivanov= GetOrFindExtensionPhone(Ivanov, "432", "Здание Управления, 3 этаж, каб. 25");
            Ivanov= GetOrFindPosition(Ivanov, "Главный экономист");
            Ivanov= GetOrFindSubdivison(Ivanov, "Плановый отдел");
            Ivanov= GetOrFindDivison(Ivanov, "Управление");
            db.Employees.Add(Ivanov);
            db.SaveChanges();

            Employee Petrov = new Employee() {
                FirstName = "Петр",
                MiddleName = "Петрович",
                LastName = "Петров",
                NumberHomePhone = "8-985-834-23-76"
            };
            Petrov= GetOrFindExtensionPhone(Petrov, "156", "Здание Управления, 2 этаж, каб. 11");
            Petrov= GetOrFindPosition(Petrov, "Оператор ЭВМ");
            Petrov= GetOrFindSubdivison(Petrov, "ОМТС");
            Petrov= GetOrFindDivison(Petrov, "Управление");
            db.Employees.Add(Petrov);
            db.SaveChanges();

            Employee Sidorov = new Employee() {
                FirstName = "Егор",
                MiddleName = "Станиславович",
                LastName = "Сидоров",
                NumberHomePhone = "8-910-123-38-07",
            };
            Sidorov= GetOrFindExtensionPhone(Sidorov, "232", "Здание Управления, 3 этаж, каб. 25");
            Sidorov= GetOrFindPosition(Sidorov, "Начальник отдела");
            Sidorov= GetOrFindSubdivison(Sidorov, "Плановый отдел");
            Sidorov= GetOrFindDivison(Sidorov, "Управление");
            db.Employees.Add(Sidorov);
            db.SaveChanges();
            
        }

        Employee GetOrFindEmployee(string firstName, string lastName, string middleName, string numberHomePhone) {
            Employee emp;
            var find = db.Employees
                               .Where(o => o.FirstName == firstName & o.LastName== lastName & o.MiddleName==middleName)
                               .FirstOrDefault();
            if (find != null) {
                emp = find;
            } else {
                emp =  new Employee() { FirstName = firstName, LastName = lastName, MiddleName = middleName, NumberHomePhone =numberHomePhone };
            }
            return emp;
        }
        Employee GetOrFindDivison(Employee emp, string name) {
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
        Employee GetOrFindSubdivison(Employee emp, string name) {
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
        Employee GetOrFindPosition(Employee emp, string name) {
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

        Employee GetOrFindExtensionPhone(Employee emp,  string number, string site) {
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
