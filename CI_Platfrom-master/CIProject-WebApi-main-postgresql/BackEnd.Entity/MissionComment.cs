using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("MissionComment")] // Specify the table name
    public class MissionComment : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("comment_description")]
        public string CommentDescription { get; set; }

        [Column("comment_date")]
        public DateTime? CommentDate { get; set; }

        [NotMapped] // Exclude these properties from being mapped to the database
        public string UserFullName { get; set; }

        [NotMapped] // Exclude these properties from being mapped to the database
        public string UserImage { get; set; }
    }
}
