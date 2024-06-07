using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("MissionTheme")] // Specify the table name
    public class MissionTheme // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("theme_name")]
        public string ThemeName { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
