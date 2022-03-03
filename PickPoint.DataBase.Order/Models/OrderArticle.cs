using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickPoint.DataBase.Order.Models
{
    /// <summary>
    /// Позиция в Заказе
    /// </summary>
    public class OrderArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Article { get; set; }

        public Order Order { get; set; }
    }
}
