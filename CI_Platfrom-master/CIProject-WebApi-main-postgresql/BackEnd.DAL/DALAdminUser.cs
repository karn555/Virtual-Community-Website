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
    public class DALAdminUser
    {
        private readonly CIDbContext _cIDbContext;
        public DALAdminUser(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public List<UserDetail> UserDetailList()
        {
            List<UserDetail> userDetails = new List<UserDetail>();
            try
            {
                using(SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    userDetails = cnn.Query<UserDetail>(StoreProcedure.UserDetailList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetails;
        }
        public string DeleteUserAndUserDetail(int userId)
        {          
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", userId);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.DeleteUserANDUserDetail_Usp, param, null, 0, CommandType.StoredProcedure));
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
