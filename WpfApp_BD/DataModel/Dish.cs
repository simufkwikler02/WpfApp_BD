using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Блюдо")]
    public class Dish
    {
        [Key]
        public int id { get; set; }

        [MaxLength(100)]
        [Column("название_блюда")]
        public string name { get; set; }

        [Column("цена_блюда")]
        public double price { get; set; }

        [Column("вес_блюда")]
        public double weight { get; set; }

        [Column("состав_блюда")]
        public string compound { get; set; }

        [Column("автор_id")]
        public int authorId { get; set; }
        
        [ForeignKey("authorId")]
        public Employee authorEmployee { get; set; }
    }
}
