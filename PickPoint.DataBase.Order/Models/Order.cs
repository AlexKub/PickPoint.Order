using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickPoint.DataBase.Order.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        /// <summary>
        /// Номер заказа
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Id постамата <see cref="ParcelLocker.Id"/>
        /// </summary>
        public Guid? ParcelLockerId { get; set; }
        /// <summary>
        /// Телефон получателя
        /// </summary>
        [StringLength(15)]
        public string RecipientPhone { get; set; }
        /// <summary>
        /// ФИО получателя 
        /// </summary>
        [StringLength(300)]
        public string RecipientFullName { get; set; }

        public ParcelLocker ParcelLocker { get; set; }

        public ICollection<OrderArticle> Articles { get; set; }

        string DebugDisplay() => $"{Number} | {Status} | {Price} | {RecipientPhone} | {RecipientFullName}";
    }
}
