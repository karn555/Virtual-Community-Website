using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("Missions")] // Specify the table name
    public class Missions : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("mission_title")]
        public string MissionTitle { get; set; }

        [Column("mission_description")]
        public string MissionDescription { get; set; }

        [Column("mission_organisation_name")]
        public string MissionOrganisationName { get; set; }

        [Column("mission_organisation_detail")]
        public string MissionOrganisationDetail { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("city_id")]
        public int CityId { get; set; }

        [Column("start_date", TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column("end_date", TypeName = "Date")]
        public DateTime EndDate { get; set; }

        [Column("mission_type")]
        public string MissionType { get; set; }

        [Column("total_sheets")]
        public int? TotalSheets { get; set; }

        [Column("registration_deadline", TypeName = "Date")]
        public DateTime? RegistrationDeadLine { get; set; }

        [Column("mission_theme_id")]
        public string MissionThemeId { get; set; }

        [Column("mission_skill_id")]
        public string MissionSkillId { get; set; }

        [Column("mission_images")]
        public string MissionImages { get; set; }

        [Column("mission_documents")]
        public string MissionDocuments { get; set; }

        [Column("mission_availability")]
        public string MissionAvailability { get; set; }

        [Column("mission_video_url")]
        public string MissionVideoUrl { get; set; }

        // Properties not mapped to the database
        [NotMapped]
        public string CountryName { get; set; }

        [NotMapped]
        public string CityName { get; set; }

        [NotMapped]
        public string MissionThemeName { get; set; }

        [NotMapped]
        public string MissionSkillName { get; set; }

        [NotMapped]
        public string MissionStatus { get; set; }

        [NotMapped]
        public string MissionApplyStatus { get; set; }

        [NotMapped]
        public string MissionApproveStatus { get; set; }

        [NotMapped]
        public string MissionDateStatus { get; set; }

        [NotMapped]
        public string MissionDeadLineStatus { get; set; }

        [NotMapped]
        public string MissionFavouriteStatus { get; set; }

        [NotMapped]
        public int Rating { get; set; }
    }

    public class SortestData
    {
        public int UserId { get; set; }
        public string SortestValue { get; set; }
        public int MissionId { get; set; }
    }
}
