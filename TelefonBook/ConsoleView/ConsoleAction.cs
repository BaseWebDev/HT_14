﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelefonBook;


namespace ConsoleView {
    public class ConsoleAction: IView {
        delegate void method();
        delegate void methodFind();
        const string hMain = "\t Телефонный справочник предприятия";
        const string hContact = "Выбирите действие для контакта:";
        string[] items = { "Вывести все контакты", "Создать новый", "Найти",  "Удалить",   "Инициализация", "Выход"};
        
        public List<string> InputContact { get; private set; }
        public string InputFindContact { get; private set; }

        public event EventHandler<EventArgs> SetShowAllContacts;
        public event EventHandler<EventArgs> SetNewContact;
        public event EventHandler<EventArgs> SetFindContacts;
        public event EventHandler<EventArgs> SetDeleteContacts;
        public event EventHandler<EventArgs> SetInitTestContacts;
        
        public void Run() {        
            method[] methods = new method[] { Display, New, Find, Delete, Init, Exit };
            ConsoleMenu menu = new ConsoleMenu(items);
            menu.Header1 = hMain;
            menu.Header2 = hContact;
            int menuResult;
            do {
                menuResult = menu.PrintMenu();
                methods[menuResult]();
                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey();
            } while (menuResult != items.Length - 1);
        }
        private void Display() {
            Console.WriteLine("Вывести номера справочника");
            if (SetShowAllContacts != null) {
                SetShowAllContacts(this, EventArgs.Empty);
            }
        }
        private void New() {
            InputContact = new List<string>();
            Console.Write("Добавить контакт сотрудника."); Console.WriteLine("Введите:");
            Console.Write("Фамилия: "); InputContact.Add(Console.ReadLine());
            Console.Write("Имя: "); InputContact.Add(Console.ReadLine());
            Console.Write("Отчество: "); InputContact.Add(Console.ReadLine());
            Console.Write("Домашний телефон: "); InputContact.Add(Console.ReadLine());

            Console.Write("Внутренний телефон: "); InputContact.Add(Console.ReadLine());
            Console.Write("Место установки: "); InputContact.Add(Console.ReadLine());

            Console.Write("Должность: "); InputContact.Add(Console.ReadLine());
            Console.Write("Отдел: "); InputContact.Add(Console.ReadLine());
            Console.Write("Подразделение: "); InputContact.Add(Console.ReadLine());
            if (SetNewContact != null) {
                SetNewContact(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Поиск по сотруднику или внутреннему номеру
        /// </summary>
        private void Find() {
            Console.Write("Введите фамилию сотрудника или внутренний номер: ");
            InputFindContact = Console.ReadLine();
            if (SetFindContacts != null) {
                SetFindContacts(this, EventArgs.Empty);
            }
        }

        private void Delete() {
            Console.Write("Введите внутренний номер абонента или фамилию для удаления контакта: ");
            InputFindContact = Console.ReadLine();
            if (SetDeleteContacts != null) {
                SetDeleteContacts(this, EventArgs.Empty);
            }
        }        
        
        
        private void Init() {
            Console.WriteLine("Инициализация БД");
            if (SetInitTestContacts != null) {
                SetInitTestContacts(this, EventArgs.Empty);
            }
            Console.WriteLine("Инициализация БД завершена.");
        }
        private void Exit() {
            Console.WriteLine("Завершение работы с приложением!");
        }

        public void ShowListContact(IEnumerable<string[]> contacts) {
            foreach (var contact in contacts)
            Console.WriteLine("{0}. {1} {2} - {4} - {3}", contact[0], contact[1], contact[2], contact[3], contact[4]);
        }

        public void ShowListAnswer(IEnumerable<string> messages) {
            foreach (var message in messages)
                Console.WriteLine(message);
        }
    }
}
