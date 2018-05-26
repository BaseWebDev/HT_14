using System;

namespace TelefonBook {
    /// <summary>
    /// Отдел компании
    /// </summary>
    public class Subdivison {
        public int Id { get; set; }
        /// <summary>
        /// Название отдела
        /// </summary>
        public string Name { get; set; }
        public Divison Divison { get; set; }
    }
}