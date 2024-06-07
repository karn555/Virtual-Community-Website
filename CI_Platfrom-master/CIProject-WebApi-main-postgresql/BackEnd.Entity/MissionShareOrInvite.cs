using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("MissionShareOrInvite")] // Specify the table name
    public class MissionShareOrInvite // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_full_name")]
        public string UserFullName { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Column("mission_share_user_email_address")]
        public string MissionShareUserEmailAddress { get; set; }

        [Column("base_url")]
        public string BaseUrl { get; set; }

        [Column("mission_id")]
        public int MissionId { get; set; }
    }
}
