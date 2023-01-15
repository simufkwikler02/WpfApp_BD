using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp_BD
{
    [Table("Столовый_инвентарь")]
    public class Inventory
    {
        [Key]
        public int id { get; set; }

        [MaxLength(200)]
        [Required]
        [Column("название_столового_инвентаря")]
        public string name { get; set; }

        [Column("дата_ввода_в_эксплуатацию")]
        public DateTime dateOfCommissioning { get; set; }

        [Column("правила_эксплуатации")]
        public string rulesExploitation { get; set; }

        [Column("помещение_id")]
        public int roomId { get; set; }

        [ForeignKey("roomId")]
        public Room room { get; set; }
    }
}
