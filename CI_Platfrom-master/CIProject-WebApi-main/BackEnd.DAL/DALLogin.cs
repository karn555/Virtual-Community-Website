using BackEnd.DAL.Helper;
using BackEnd.Entity;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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
using System.Xml.Linq;

namespace BackEnd.DAL
{
    public class DALLogin
    {
        private readonly CIDbContext _cIDbContext;
        public DALLogin(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public User GetUserById(int userId)
        {
            try
            {      User user = new User();
                    // Retrieve the user by ID
                    user = _cIDbContext.User.FirstOrDefault(u => u.Id == userId && !u.IsDeleted);

                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RegisterUserDetail(int id)
        {
            User user = new User();
            user = _cIDbContext.User.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
            if (user != null)
            {
                string maxEmployeeIdStr = _cIDbContext.UserDetail.Max(ud => ud.EmployeeId);
                int maxEmployeeId = 0;

                // Convert the maximum EmployeeId to an integer
                if (!string.IsNullOrEmpty(maxEmployeeIdStr))
                {
                    if (int.TryParse(maxEmployeeIdStr, out int parsedEmployeeId))
                    {
                        maxEmployeeId = parsedEmployeeId;
                    }
                    else
                    {
                        // Handle conversion error
                        throw new Exception("Error converting EmployeeId to integer.");
                    }
                }

                // Increment the maximum EmployeeId by 1 for the new user
                int newEmployeeId = maxEmployeeId + 1;
                var newUserDetail = new UserDetail
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress,
                    UserType = user.UserType,
                    Name = user.FirstName,
                    Surname = user.LastName,
                    EmployeeId = newEmployeeId.ToString(),
                    Department = "IT",
                    Status = true
                };

                _cIDbContext.UserDetail.Add(newUserDetail);
                _cIDbContext.SaveChanges();
            }
            return "User register successfully.";
        }

        public string Register(User user)
        {
            string result = "";
            try
            {
                // Check if the email address already exists
                bool emailExists = _cIDbContext.User.Any(u => u.EmailAddress == user.EmailAddress && !u.IsDeleted);

                if (!emailExists)
                {
                    int maxIdStr = _cIDbContext.User.Max(ud => ud.Id);
                    int maxId = 0;
                    if (maxIdStr > maxId)
                    {
                        maxId = maxIdStr;
                    }
                    int newmaxId = maxId + 1;



                    // Create a new user entity
                    var newUser = new User
                    {

                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };
                   
                    // Add the new user to the database
                    _cIDbContext.User.Add(newUser);
                    _cIDbContext.SaveChanges();
                    user = _cIDbContext.User.FirstOrDefault(u => u.EmailAddress == user.EmailAddress && !u.IsDeleted);
                    RegisterUserDetail(user.Id);


                    result = "User register successfully.";
                }
                else
                {
                    throw new Exception("Email Address Already Exist.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        public string UpdateUser(User updatedUser)
        {
            string result = "";
            try
            {
                // Check if the user with the provided email address exists and is not deleted
                var existingUser = _cIDbContext.User.FirstOrDefault(u => u.EmailAddress == updatedUser.EmailAddress && !u.IsDeleted);
                var existingUserDetail = _cIDbContext.UserDetail.FirstOrDefault(u=> u.UserId == updatedUser.Id && !u.IsDeleted);

                if (existingUser != null && existingUserDetail != null)
                {
                    // Update user details
                    existingUser.FirstName = updatedUser.FirstName;
                    existingUser.LastName = updatedUser.LastName;
                    existingUser.PhoneNumber = updatedUser.PhoneNumber;
                    existingUser.UserType = updatedUser.UserType;
                    existingUser.ModifiedDate = DateTime.Now;

                    existingUserDetail.FirstName = updatedUser.FirstName;
                    existingUserDetail.LastName = updatedUser.LastName;
                    existingUserDetail.PhoneNumber = updatedUser.PhoneNumber;
                    existingUserDetail.EmailAddress = updatedUser.EmailAddress;
                    existingUserDetail.UserType = updatedUser.UserType;
                    existingUserDetail.Name = updatedUser.FirstName;
                    existingUserDetail.Surname = updatedUser.LastName;
                    existingUserDetail.ModifiedDate = DateTime.Now;

                    // Save changes to the database
                    _cIDbContext.SaveChanges();

                    result = "User updated successfully.";
                }
                else
                {
                    throw new Exception("User not found or already deleted.");
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
            User userObj = new User();
            try
            {
                    var query = from u in _cIDbContext.User
                                where u.EmailAddress == user.EmailAddress && u.IsDeleted == false
                                select new
                                {
                                    u.Id,
                                    u.FirstName,
                                    u.LastName,
                                    u.PhoneNumber,
                                    u.EmailAddress,
                                    u.UserType,
                                    u.Password,
                                    UserImage = u.UserImage
                                };

                    var userData = query.FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.Password == user.Password)
                        {
                            userObj.Id = userData.Id;
                            userObj.FirstName = userData.FirstName;
                            userObj.LastName = userData.LastName;
                            userObj.PhoneNumber = userData.PhoneNumber;
                            userObj.EmailAddress = userData.EmailAddress;
                            userObj.UserType = userData.UserType;
                            userObj.UserImage = userData.UserImage;
                            userObj.Message = "Login Successfully";
                        }
                        else
                        {
                            userObj.Message = "Incorrect Password.";
                        }
                    }
                    else
                    {
                        userObj.Message = "Email Address Not Found.";
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userObj;
        }
        public async Task<Boolean> ForgotPassword(ForgotPassword forgotPassword)
        {
            var userAvilable = _cIDbContext.User.Where(c => c.EmailAddress == forgotPassword.EmailAddress).FirstOrDefault();

            if (userAvilable != null)
            {
                string newGUID = Guid.NewGuid().ToString();
                string callbackurl = forgotPassword.baseUrl + "/resetPassword?Uid=" + newGUID;                
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
                    param.Add("@Avilability", userDetail.Avilability);
                    param.Add("@LinkdInUrl", userDetail.LinkdInUrl);
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
