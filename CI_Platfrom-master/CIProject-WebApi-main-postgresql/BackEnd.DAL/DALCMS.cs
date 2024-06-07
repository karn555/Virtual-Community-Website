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
    public class DALCMS
    {
        private readonly CIDbContext _cIDbContext;
        public DALCMS(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public List<CMS> CMSList()
        {
            List<CMS> cmsList = new List<CMS>();
            try
            {
                using(SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cmsList = cnn.Query<CMS>(StoreProcedure.CMSList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }    
            }
            catch (Exception)
            {
                throw;
            }
            return cmsList;
        }
        public CMS CMSDetailById(int id)
        {
            CMS cmsDetail = new CMS();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    cmsDetail = cnn.Query<CMS>(StoreProcedure.CMSDetailById_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cmsDetail;
        }
        public string AddCMS(CMS cms)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Title", cms.Title);
                    param.Add("@Description", cms.Description);
                    param.Add("@Slug", cms.Slug);
                    param.Add("@Status", cms.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddCMS_Usp, param,null,0,CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateCMS(CMS cms)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", cms.Id);
                    param.Add("@Title", cms.Title);
                    param.Add("@Description", cms.Description);
                    param.Add("@Slug", cms.Slug);
                    param.Add("@Status", cms.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UpdateCMS_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        } 
        
        public string DeleteCMS(int id)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);                  
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.DeleteCMS_Usp, param, null, 0, CommandType.StoredProcedure));
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
