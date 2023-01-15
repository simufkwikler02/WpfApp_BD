using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Состав_заказа")]
    public class OrderCompound
    {
        [Key]
        public int id { get; set; }
        
        [Column("количество_порций")]
        public int amount { get; set; }

        [Column("заказ_id")]
        public int orderId { get; set; }

        [ForeignKey("orderId")]
        public Order order { get; set; }

        [Column("блюдо_id")]
        public int dishId { get; set; }

        [ForeignKey("dishId")]
        public Dish dish { get; set; }
    }
}
