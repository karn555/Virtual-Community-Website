using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entity;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BackEnd.DAL
{
    public class DALMissionTheme
    {
        private readonly CIDbContext _cIDbContext;
        public DALMissionTheme(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<MissionTheme> GetMissionThemeList()
        {
            try
            {
                List<MissionTheme> missionTheme = new List<MissionTheme>();
                try
                {
                    using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                    {
                        missionTheme = cnn.Query<MissionTheme>(StoreProcedure.MissionThemeList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return missionTheme;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MissionTheme GetMissionThemeById(int id)
        {
            try
            {
                MissionTheme missionThemeDetail = new MissionTheme();
                try
                {
                    using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@Id", id);
                        missionThemeDetail = cnn.Query<MissionTheme>(StoreProcedure.MissionThemeDetailById_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return missionThemeDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@ThemeName", missionTheme.ThemeName);                                        
                    param.Add("@Status", missionTheme.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddMissionTheme_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", missionTheme.Id);
                    param.Add("@ThemeName", missionTheme.ThemeName);
                    param.Add("@Status", missionTheme.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UpdateMissionTheme_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteMissionTheme(int id)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);                  
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.DeleteMissionTheme_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
