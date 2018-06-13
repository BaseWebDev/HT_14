using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView {
    public interface IView {
        /// <summary>
        /// Список строк для создания нового контакта
        /// </summary>
        List<string> InputContact { get; }
        /// <summary>
        /// Строка для поиска по фамилии и внутреннему номеру
        /// </summary>
        string InputFindContact { get; }
        /// <summary>
        /// Отобразить контакт
        /// </summary>
        /// <param name="list">Список выводимых строк</param>
        void ShowListContact(IEnumerable<string[]> list);
        /// <summary>
        /// Отобразить сообщение/предупреждение об операции
        /// </summary>
        /// <param name="list">Список сообщений</param>
        void ShowListAnswer(IEnumerable<string> list);
        /// <summary>
        /// Событие при запросе на отображение всех контактов
        /// </summary>
        event EventHandler<EventArgs> SetShowAllContacts;
        /// <summary>
        /// Событие при запросе на создание нового контакта
        /// </summary>
        event EventHandler<EventArgs> SetNewContact;
        /// <summary>
        /// Событие при запросе на поиск контакта
        /// </summary>
        event EventHandler<EventArgs> SetFindContacts;      
        /// <summary>
        /// Событие при удалении контакта
        /// </summary>
        event EventHandler<EventArgs> SetDeleteContacts;
        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        event EventHandler<EventArgs> SetInitTestContacts;


    }
}
