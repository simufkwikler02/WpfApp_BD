using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Должность")]
    public class Job
    {
        [Key]
        public int id { get; set; }

        [MaxLength(200)]
        [Column("название_должности")]
        public string name { get; set; }

        [Column("зарплата")]
        public double salary { get; set; }
    }
}
