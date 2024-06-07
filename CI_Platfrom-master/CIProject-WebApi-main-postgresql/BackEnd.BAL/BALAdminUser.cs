using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DAL;
using BackEnd.Entity;

namespace BackEnd.BAL
{
    public class BALAdminUser
    {
        private readonly DALAdminUser _dalAdminUser;
        public BALAdminUser(DALAdminUser dalAdminUser)
        {
            _dalAdminUser = dalAdminUser;
        }

        public List<UserDetail> UserDetailList()
        {
            return _dalAdminUser.UserDetailList();
        }

        public string DeleteUserAndUserDetail(int userId)
        {
            return _dalAdminUser.DeleteUserAndUserDetail(userId);
        }
    }
}
