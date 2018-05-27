using System;
using System.ComponentModel.DataAnnotations;

namespace TelefonBook {
    /// <summary>
    /// Справочник сотрудников
    /// </summary>
    public class Employee {
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        [Required]
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Домашний телефон, обычно личный номер, по которому сотрудника возможно найти в нерабочее время
        /// </summary>
        public string NumberHomePhone { get; set; }
        [Required]
        public virtual Position Position { get; set; }
        [Required]
        public virtual ExtensionPhone ExtensionPhone { get; set; }
    }
}