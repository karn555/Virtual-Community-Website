using BackEnd.Entity;
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
    public class DALMissionSkill
    {
        private readonly CIDbContext _cIDbContext;
        public DALMissionSkill(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<MissionSkill> GetMissionSkillList()
        {
            try
            {
                List<MissionSkill> missionSkill = new List<MissionSkill>();
                try
                {
                    using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                    {
                        missionSkill = cnn.Query<MissionSkill>(StoreProcedure.MissionSkillList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return missionSkill;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MissionSkill GetMissionSkillById(int id)
        {
            try
            {
                MissionSkill missionSkillDetail = new MissionSkill();
                try
                {
                    using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                    {
                        var param = new DynamicParameters();
                        param.Add("@Id", id);
                        missionSkillDetail = cnn.Query<MissionSkill>(StoreProcedure.MissionSkillDetailById_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return missionSkillDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@SkillName", missionSkill.SkillName);
                    param.Add("@Status", missionSkill.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddMissionSkill_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", missionSkill.Id);
                    param.Add("@SkillName", missionSkill.SkillName);
                    param.Add("@Status", missionSkill.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UpdateMissionSkill_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteMissionSkill(int id)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.DeleteMissionSkill_Usp, param, null, 0, CommandType.StoredProcedure));
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
