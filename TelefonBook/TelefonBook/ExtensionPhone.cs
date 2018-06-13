using System;
using System.ComponentModel.DataAnnotations;

namespace TelefonBook {
    /// <summary>
    /// Внутренние номера телефонов
    /// </summary>
    public class ExtensionPhone {
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Внутренний номер
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Место установки
        /// </summary>
        public string InstallationSite { get; set; }
    }
}