using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Заказ")]
    public class Order
    {
        [Key]
        public int id { get; set; }

        [Column("сумма_заказа")]
        public double sumOfOrder { get; set; }

        [Column("номер_транзакции")]
        public int transactionNumber { get; set; }

        [Column("оператор_id")]
        public int operatorId { get; set; }

        [ForeignKey("operatorId")]
        public Employee operatorEmployee { get; set; }
    }
}
