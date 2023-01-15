using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Помещение")]
    public class Room
    {
        [Key]
        public int id { get; set; }

        [MaxLength(200)]
        [Required]
        [Column("название_помещения")]
        public string name { get; set; }

        [Column("дата_последней_уборки")]
        public DateTime dateOfLastCleaning { get; set; }
    }
}
