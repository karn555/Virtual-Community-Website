using BackEnd.Entity;
using BackEnd.Entity.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class DALCommon
    {
        private readonly CIDbContext _cIDbContext;
        public DALCommon(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public List<DropDown> CountryList()
        {
            List<DropDown> countries = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select Id as Value,CountryName as Text from Country order by CountryName";
                    countries = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return countries;
        } 
        public List<DropDown> CityList(int countryId)
        {
            List<DropDown> cities = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@CountryId", countryId);
                    cities = cnn.Query<DropDown>(StoreProcedure.CountryWiseCityList_Usp,param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return cities;
        }

        public List<DropDown> MissionCountryList()
        {
            List<DropDown> countries = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select Distinct CI.CountryName As Text,M.CountryId As Value from Missions M join Country CI on CI.Id  = M.CountryId join City C on C.Id  = M.CityId";
                    countries = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return countries;
        }

        public List<DropDown> MissionCityList()
        {
            List<DropDown> citie = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select Distinct C.CityName As Text,M.CityId As Value from Missions M join Country CI on CI.Id  = M.CountryId join City C on C.Id  = M.CityId";
                    citie = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return citie;
        }
        public List<DropDown> MissionThemeList()
        {
            List<DropDown> missionTheme = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select Distinct MT.ThemeName As Text,MT.ID AS Value from Missions M join MissionTheme MT ON MT.Id = M.MissionThemeId AND MT.IsDeleted=0 where M.IsDeleted = 0";
                    missionTheme = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return missionTheme;
        } 
        public List<DropDown> MissionSkillList()
        {
            List<DropDown> missionSkill = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select Distinct MS.SkillName As Text,MS.ID AS Value from MissionSkill MS where MS.IsDeleted = 0";
                    missionSkill = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return missionSkill;
        } 
        public List<DropDown> MissionTitleList()
        {
            List<DropDown> missionSkill = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select MissionTitle AS Text,Id AS Value from [Missions] where IsDeleted=0";
                    missionSkill = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return missionSkill;
        }

        public string ContactUs(ContactUs contactUs)
        {
            try
            {
                using(SqlConnection cnn=new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    string result = "";
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", contactUs.UserId);                   
                    param.Add("@Name", contactUs.Name);                   
                    param.Add("@EmailAddress", contactUs.EmailAddress);                   
                    param.Add("@Subject", contactUs.Subject);                   
                    param.Add("@Message", contactUs.Message);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddContactUs_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string AddUserSkill(UserSkills userSkills)
        {
            try
            {
                using(SqlConnection cnn=new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    string result = "";
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Skill", userSkills.Skill);                   
                    param.Add("@UserId", userSkills.UserId);                                      
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddUserSkill_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DropDown> GetUserSkill(int userId)
        {
            List<DropDown> missionSkill = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", userId);
                    missionSkill = cnn.Query<DropDown>(StoreProcedure.UserIdWiseSkillList_Usp, param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return missionSkill;
        }
    }
}
