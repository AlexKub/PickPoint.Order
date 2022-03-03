using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickPoint.DataBase.Order.Models
{
    /// <summary>
    /// Постамат
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ParcelLocker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        /// <summary>
        /// Номер постамата
        /// </summary>
        [StringLength(8)]
        public string Number { get; set; }
        /// <summary>
        /// АДрес установки
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Статус постамата
        /// </summary>
        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; set; }

        string DebugDisplay() => $"{Number} | {(IsActive ? "Active" : "Closed")} | {Address}";
    }
}
