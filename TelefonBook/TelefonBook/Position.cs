using System;

namespace TelefonBook {
    /// <summary>
    /// Должность сотрудника
    /// </summary>
    public class Position {
        public int Id { get; set; }
        /// <summary>
        /// Название должности
        /// </summary>
        public string Name { get; set; }
        public Subdivison Subdivison { get; set; }
    }
}