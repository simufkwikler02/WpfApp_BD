using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Рабочая_смена")]
    public class Responsibilites
    {
        [Key]
        public int id { get; set; }

        [Column("сотрудник_id")]
        public int employeeId { get; set; }

        [ForeignKey("employeeId")]
        public Employee employee { get; set; }

        [Column("помещение_id")]
        public int roomId { get; set; }

        [ForeignKey("roomId")]
        public Room room { get; set; }

        [Column("обязанности")]
        public string responsibility { get; set; }
    }
}
