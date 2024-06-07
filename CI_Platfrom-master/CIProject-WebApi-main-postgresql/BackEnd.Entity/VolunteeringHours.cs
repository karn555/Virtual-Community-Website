using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("VolunteeringHours")] // Specify the table name
    public class VolunteeringHours : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }

        [NotMapped]
        public string MissionName { get; set; }

        [Column("date_volunteered", TypeName = "Date")]
        public DateTime DateVolunteered { get; set; }

        [Column("hours")]
        public string Hours { get; set; }

        [Column("minutes")]
        public string Minutes { get; set; }

        [Column("message")]
        public string Message { get; set; }
    }

    [Table("VolunteeringGoals")] // Specify the table name
    public class VolunteeringGoals : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }

        [NotMapped]
        public string MissionName { get; set; }

        [Column("date", TypeName = "Date")]
        public DateTime Date { get; set; }

        [Column("action")]
        public int Action { get; set; }

        [Column("message")]
        public string Message { get; set; }
    }
}
