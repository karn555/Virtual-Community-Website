using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entity
{
    [Table("Country")] // Explicitly specify the table name
    public class Country
    {
        [Key]
        [Column("id")] // Define the column name and make it lowercase
        public int Id { get; set; }

        [Column("country_name")] // Define the column name and make it lowercase
        public string CountryName { get; set; }
    }

    [Table("City")] // Explicitly specify the table name
    public class City
    {
        [Key]
        [Column("id")] // Define the column name and make it lowercase
        public int Id { get; set; }

        [Column("country_id")] // Define the column name and make it lowercase
        public int CountryId { get; set; }

        [Column("city_name")] // Define the column name and make it lowercase
        public string CityName { get; set; }
    }
}
