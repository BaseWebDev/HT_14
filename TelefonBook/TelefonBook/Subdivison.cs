using System;
using System.ComponentModel.DataAnnotations;

namespace TelefonBook {
    /// <summary>
    /// Отдел компании
    /// </summary>
    public class Subdivison {
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Название отдела
        /// </summary>
        public string Name { get; set; }
        [Required]
        public virtual Divison Divison { get; set; }
    }
}