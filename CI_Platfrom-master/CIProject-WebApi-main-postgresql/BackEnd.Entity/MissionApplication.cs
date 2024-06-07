using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("MissionApplication")] // Specify the table name
    public class MissionApplication : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }

        [NotMapped] // Exclude this property from mapping to the database
        public string MissionTitle { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [NotMapped] // Exclude this property from mapping to the database
        public string UserName { get; set; }

        [NotMapped] // Exclude this property from mapping to the database
        public string UserImage { get; set; }

        [Column("applied_date")] // Specify column name and data type
        public DateTime AppliedDate { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("sheet")]
        public int Sheet { get; set; }
    }
}
