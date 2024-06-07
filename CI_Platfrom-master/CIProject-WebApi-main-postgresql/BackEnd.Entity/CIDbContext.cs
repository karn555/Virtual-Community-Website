using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Entity
{
    public class CIDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public CIDbContext(DbContextOptions<CIDbContext> options,IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public DbSet<User> User { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<CMS> CMS { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<City> City { get; set; }
        public DbSet<Missions> Missions { get; set; }
        public DbSet<Story> Story { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<VolunteeringHours> VolunteeringHours { get; set; }
        public DbSet<VolunteeringGoals> VolunteeringGoals { get; set; }
        public DbSet<MissionApplication> MissionApplication { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }

        public DbSet<MissionTheme> MissionTheme { get; set; }
        public DbSet<MissionSkill> MissionSkill { get; set; }
        public DbSet<MissionComment> MissionComment { get; set; }
        public DbSet<MissionFavourites> MissionFavourites { get; set; }
        public DbSet<MissionRating> MissionRating { get; set; }

    }
}
