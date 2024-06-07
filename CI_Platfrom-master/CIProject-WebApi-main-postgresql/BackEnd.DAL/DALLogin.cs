using BackEnd.DAL.Helper;
using BackEnd.Entity;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class DALLogin
    {
        private readonly CIDbContext _cIDbContext;
        public DALLogin(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
     
        public string Register(User user)
        {
            string result = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@FirstName", user.FirstName);
                    param.Add("@LastName", user.LastName);
                    param.Add("@PhoneNumber", user.PhoneNumber);
                    param.Add("@EmailAddress", user.EmailAddress);
                    param.Add("@Password", user.Password);
                    param.Add("@UserType", user.UserType);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UserRegister_Usp, param, null, 0, CommandType.StoredProcedure));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public User LoginUser(User user)
        {
            User userObj = null;
            try
            {
                    // Check if the user exists in the database
                    var existingUser = _cIDbContext.User
                        .FirstOrDefault(u => u.EmailAddress == user.EmailAddress && u.IsDeleted == false);

                    if (existingUser != null)
                    {
                        // If the user exists, check the password
                        if (existingUser.Password == user.Password)
                        {
                            // User found and password matched, return user details
                            userObj = _cIDbContext.User
                                .Where(u => u.Id == existingUser.Id && u.IsDeleted == false)
                                .Select(u => new User
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    PhoneNumber = u.PhoneNumber,
                                    EmailAddress = u.EmailAddress,
                                    UserType = u.UserType,
                                })
                                .FirstOrDefault();
                            if (userObj != null)
                            {
                                userObj.Message = "Login Successfully";
                            }
                        }
                        else
                        {
                            // Incorrect Password
                            userObj = new User { Message = "Incorrect Password." };
                        }
                    }
                    else
                    {
                        // Email Address Not Found
                        userObj = new User { Message = "Email Address Not Found." };
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return userObj;
        }

        public async Task<Boolean> ForgotPassword(ForgotPassword forgotPassword)
        {
            var userAvilable = _cIDbContext.User.Where(c => c.EmailAddress == forgotPassword.EmailAddress).FirstOrDefault();

            if (userAvilable != null)
            {
                string newGUID = Guid.NewGuid().ToString();
                string callbackurl = forgotPassword.BaseUrl + "/resetPassword?Uid=" + newGUID;                
                int userId = userAvilable.Id;
                forgotPassword.Id = newGUID;
                forgotPassword.UserId = userId;
                forgotPassword.RequestDateTime = DateTime.Now;
                _cIDbContext.ForgotPassword.Add(forgotPassword);
                _cIDbContext.SaveChanges();
                try
                {
                    string mailTo = userAvilable.EmailAddress;
                    string userName = userAvilable.FirstName + " " + userAvilable.LastName;
                    string emailBody = "Hi " + userName + ",<br/><br/> Click the link below to reset your password <br/><br/> " + callbackurl;

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient();

                    mail.From = new MailAddress("sourabh.patidar@tatvasoft.com");
                    mail.To.Add(mailTo);
                    mail.Subject = "Reset Password";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;

                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new NetworkCredential("sourabh.patidar@tatvasoft.com", "Bow88327");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp-mail.outlook.com"; // Outlook SMTP server

                    SmtpServer.Send(mail);

                    return true;

                }
                catch (Exception)
                {
                    return false;
                }                
            }
            return false;
        }      
        public async Task<string> ResetPassword(User user)
        {
            var data = _cIDbContext.ForgotPassword.Where(c => c.Id == user.Uid).FirstOrDefault();
            if(data.UserId != 0)
            {
                var userId = data.UserId;
                var userData = _cIDbContext.User.Where(c => c.Id == userId).FirstOrDefault();
                userData.Password = user.Password;
                _cIDbContext.Entry(userData).State = EntityState.Modified;
                await _cIDbContext.SaveChangesAsync();

                _cIDbContext.Entry(data).State = EntityState.Deleted;
                await _cIDbContext.SaveChangesAsync();
                return "Success";
            }
            return "Failure";
        }

        public string ChangePassword(ChangePassword changePassword)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    string result = "";
                    var param = new DynamicParameters();
                    param.Add("@UsersId", changePassword.UserId);
                    param.Add("@OldPassword", changePassword.OldPassword);
                    param.Add("@NewPassword", changePassword.NewPassword);                  
                    param.Add("@ConfirmPassword", changePassword.ConfirmPassword);                  
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.ChangePassword_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User LoginUserDetailById(int id)
        {
            User userDetail = new User();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", id);             
                    userDetail = cnn.Query<User>(StoreProcedure.UserDetailByUserId_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }  
        public UserDetail GetUserProfileDetailById(int userId)
        {
            UserDetail userDetail = new UserDetail();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@userId", userId);             
                    userDetail = cnn.Query<UserDetail>(StoreProcedure.GetUserProfileDetailByUserId_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public string LoginUserProfileUpdate(UserDetail userDetail)
        {
            string result = "";
            try
            {
                using(SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", userDetail.UserId);
                    param.Add("@Name", userDetail.Name);
                    param.Add("@Surname", userDetail.Surname);
                    param.Add("@EmployeeId", userDetail.EmployeeId);
                    param.Add("@Manager", userDetail.Manager);
                    param.Add("@Title", userDetail.Title);
                    param.Add("@Department", userDetail.Department);
                    param.Add("@MyProfile", userDetail.MyProfile);
                    param.Add("@WhyIVolunteer", userDetail.WhyIVolunteer);
                    param.Add("@CountryId", userDetail.CountryId);
                    param.Add("@CityId", userDetail.CityId);
                    param.Add("@Avilability", userDetail.Availability);
                    param.Add("@LinkdInUrl", userDetail.LinkedInUrl);
                    param.Add("@MySkills", userDetail.MySkills);
                    param.Add("@UserImage", userDetail.UserImage);
                    param.Add("@Status", userDetail.Status);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.UserDetailRegister_Usp, param, null, 0, CommandType.StoredProcedure));

                    var query = "update [User] set FirstName='"+userDetail.Name+"',LastName='"+userDetail.Surname+"' where Id = "+userDetail.UserId+"";
                    cnn.ExecuteScalar(query, null, null, 0, CommandType.Text);                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
