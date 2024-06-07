using BackEnd.DAL;
using BackEnd.DAL.JWTService;
using BackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL
{
    public class BALLogin
    {
        private readonly DALLogin _dalLogin;
        private readonly JwtService _jwtService;
        ResponseResult result = new ResponseResult();
        public BALLogin(DALLogin dalLogin, JwtService jwtService)
        {
            _dalLogin = dalLogin;
            _jwtService = jwtService;
        }
        //public async Task<string> Register(User user)
        //{
        //    return await _dalLogin.Register(user);
        //}

        //public async Task<string> LoginUser(User user)
        //{
        //    return await _dalLogin.LoginUser(user);
        //}
        public string Register(User user)
        {
            return _dalLogin.Register(user);
        }

        public ResponseResult LoginUser(User user)
        {
            try
            {
                User userObj= new User();
                userObj = UserLogin(user);

                if(userObj != null)
                {
                    if(userObj.Message.ToString() == "Login Successfully")
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Success;
                        result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.EmailAddress,userObj.UserType,userObj.UserImage);
                    }
                    else
                    {
                        result.Message = userObj.Message;
                        result.Result = ResponseStatus.Error;
                    }
                }
                else
                {
                    result.Message = "Error in Login";
                    result.Result = ResponseStatus.Error;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public User  UserLogin(User user)
        {
            User userOb = new User()
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };

            return _dalLogin.LoginUser(user);
        }

        public async Task<Boolean> ForgotPassword(ForgotPassword forgotPassword)
        {
            return await _dalLogin.ForgotPassword(forgotPassword);
        }    
        public async Task<string> ResetPassword(User user)
        {
            return await _dalLogin.ResetPassword(user);
        }

        public User LoginUserDetailById(int id)
        {
            return _dalLogin.LoginUserDetailById(id);
        }  
        public UserDetail GetUserProfileDetailById(int userId)
        {
            return _dalLogin.GetUserProfileDetailById(userId);
        }
        public string LoginUserProfileUpdate(UserDetail userDetail)
        {
            return _dalLogin.LoginUserProfileUpdate(userDetail);
        }
        public string ChangePassword(ChangePassword changePassword)
        {
            return _dalLogin.ChangePassword(changePassword);
        }
    }
}
