using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WpfApp_BD
{
    [Table("Сотрудник")]
    public class Employee
    {
        [Key]
        public int id { get; set; }
        
        [MaxLength(200)]
        [Required]
        [Column("фио")]
        public string fullName { get; set; }

        [Column("дата_рождения")]
        public DateTime dateOfBirth { get; set; }

        [MaxLength(30)]
        [Column("телефон")]
        public string phone { get; set; }

        [MaxLength(30)]
        [Required]
        [Column("паспорт")]
        public string passport { get; set; }

        [Column("должность_id")]
        public int postId { get; set; }

        [ForeignKey("postId")]
        public Job postEmployee { get; set; }
    }
}
