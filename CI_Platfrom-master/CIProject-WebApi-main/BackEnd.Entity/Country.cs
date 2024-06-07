using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Entity
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
    }

    public class City
    {
        [Key]   
        public int Id { get; set; }

        public int CountryId { get; set; }
        public string CityName { get; set; }
    }
}
