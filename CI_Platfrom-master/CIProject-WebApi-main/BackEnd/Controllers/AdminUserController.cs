using BackEnd.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BAL;
namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        ResponseResult result = new ResponseResult();
        private readonly BALAdminUser _balAdminUser;
        public AdminUserController(BALAdminUser balAdminUser)
        {
            _balAdminUser = balAdminUser;
        }

        [HttpGet]
        [Route("UserDetailList")]
        public ResponseResult UserDetailList() 
        {
            try
            {
                result.Data = _balAdminUser.UserDetailList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Success;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteUserAndUserDetail/{userId}")]
        public ResponseResult DeleteUserAndUserDetail(int userId)
        {
            try
            {
                result.Data = _balAdminUser.DeleteUserAndUserDetail(userId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
