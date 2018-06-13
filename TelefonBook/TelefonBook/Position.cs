using System;
using System.ComponentModel.DataAnnotations;

namespace TelefonBook {
    /// <summary>
    /// Должность сотрудника
    /// </summary>
    public class Position {
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Название должности
        /// </summary>
        public string Name { get; set; }
        [Required]
        public virtual Subdivison Subdivison { get; set; }
    }
}