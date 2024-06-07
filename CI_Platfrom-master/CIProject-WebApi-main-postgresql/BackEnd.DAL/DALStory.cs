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
    public class DALStory
    {
        private readonly CIDbContext _ciDbContext;

        public DALStory(CIDbContext ciDbContext)
        {
            _ciDbContext = ciDbContext;
        }
        #region ClientSide
        
        public List<DropDown> GetMissionTitle()
        {
            List<DropDown> missionTitleList = new List<DropDown>();
            try
            {
                using(SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var query = "select MissionTitle As Text,Id As Value from Missions where IsDeleted=0";
                    missionTitleList = cnn.Query<DropDown>(query, null, null, true, 0, CommandType.Text).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missionTitleList;
        }       
        public string AddStory(Story story)
        {
            try
            {
                string result = "";
                story.IsActive = false;             
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", story.MissionId);
                    param.Add("@UserId", story.UserId);
                    param.Add("@StoryTitle", story.StoryTitle);
                    param.Add("@StoryDate", story.StoryDate);
                    param.Add("@StoryDescription", story.StoryDescription);
                    param.Add("@VideoUrl", story.VideoUrl);
                    param.Add("@StoryImage", story.StoryImage);
                    param.Add("@IsActive", story.IsActive);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddStory_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Story> ClientSideStoryList()
        {
            List<Story> storyList = new List<Story>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    storyList = cnn.Query<Story>(StoreProcedure.StoryListClientSide_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return storyList;
        }

        public Story StoryDetailById(int id)
        {
            Story storyById = new Story();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    storyById = cnn.Query<Story>(StoreProcedure.StoryDetailById_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return storyById;
        }
        #endregion

        #region AdminSide

        public List<Story> AdminSideStoryList()
        {
            List<Story> storyList = new List<Story>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();                    
                    storyList = cnn.Query<Story>(StoreProcedure.StoryList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return storyList;
        }  
       
        public string StoryStatusActive(Story story)
        {
            try
            {
                string result = "";                        
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", story.Id);
                    param.Add("@IsActive", story.IsActive);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UpdateStoryActiveStatus_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DeleteStory(int id)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.DeleteStory_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Story StoryDetailByIdAdmin(int id)
        {
            Story storyById = new Story();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_ciDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    storyById = cnn.Query<Story>(StoreProcedure.StoryDetailByIdAdmin_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return storyById;
        }

        #endregion
    }
}
