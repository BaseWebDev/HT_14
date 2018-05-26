using System;

namespace TelefonBook {
    /// <summary>
    /// Внутренние номера телефонов
    /// </summary>
    class ExtensionPhone {
        public int Id { get; set; }
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