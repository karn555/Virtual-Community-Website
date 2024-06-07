using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("User")] // Specify the table name
    public class User : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }

        [Column("user_type")]
        public string UserType { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string Uid { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [Column("user_image")]
        public string UserImage { get; set; } = "";

        [NotMapped]
        public string UserFullName { get; set; }
    }

    [Table("UserDetail")] // Specify the table name
    public class UserDetail : BaseEntity // Assuming BaseEntity defines common properties
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("employee_id")]
        public string EmployeeId { get; set; }

        [Column("manager")]
        public string Manager { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("department")]
        public string Department { get; set; }

        [Column("my_profile")]
        public string MyProfile { get; set; }

        [Column("why_i_volunteer")]
        public string WhyIVolunteer { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("city_id")]
        public int CityId { get; set; }

        [Column("availability")]
        public string Availability { get; set; }

        [Column("linkd_in_url")]
        public string LinkedInUrl { get; set; }

        [Column("my_skills")]
        public string MySkills { get; set; }

        [Column("user_image")]
        public string UserImage { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [NotMapped]
        public int UId { get; set; }

        [NotMapped]
        public string FirstName { get; set; }

        [NotMapped]
        public string LastName { get; set; }

        [NotMapped]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public string EmailAddress { get; set; }

        [NotMapped]
        public string UserType { get; set; }
    }

    [Table("ForgotPassword")] // Specify the table name
    public class ForgotPassword
    {
        [Key]
        [Column("temp_id")]
        public int TempId { get; set; }

        [Column("id")]
        public string Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("request_date_time")]
        public DateTime RequestDateTime { get; set; }

        [NotMapped]
        public string EmailAddress { get; set; }

        [NotMapped]
        public string BaseUrl { get; set; }
    }

    [Table("ChangePassword")] // Specify the table name
    public class ChangePassword
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("old_password")]
        public string OldPassword { get; set; }

        [Column("new_password")]
        public string NewPassword { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
