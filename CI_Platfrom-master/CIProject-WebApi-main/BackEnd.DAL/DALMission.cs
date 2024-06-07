﻿using BackEnd.Entity;
using BackEnd.Entity.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class DALMission
    {
        private readonly CIDbContext _cIDbContext;
        
        public DALMission(CIDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<DropDown> GetMissionThemeList()
        {
            List<DropDown> missionthemeList = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    missionthemeList = cnn.Query<DropDown>(StoreProcedure.ActiveThemeList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missionthemeList;
        }
        public List<DropDown> GetMissionSkillList()
        {
            List<DropDown> missionskillList = new List<DropDown>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    missionskillList = cnn.Query<DropDown>(StoreProcedure.ActiveSkillList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missionskillList;
        }
        public List<Missions> MissionList()
        {
            List<Missions> missions = new List<Missions>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {                    
                    missions = cnn.Query<Missions>(StoreProcedure.MissionList_Usp, null, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missions;
        }
        public string AddMission(Missions mission)
        {
            string result = "";
            try
            {
                _cIDbContext.Missions.Add(mission);
                _cIDbContext.SaveChanges();
                result = "Mission added successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Missions MissionDetailById(int id)
        {
            Missions mission = new Missions();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    mission = cnn.Query<Missions>(StoreProcedure.MissionDetailById_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mission;
        }
        public string UpdateMission(Missions mission)
        {
            string result = "";
            try
            {
                // Check if the mission with the same title, city, start date, and end date already exists
                bool missionExists = _cIDbContext.Missions.Any(m => m.MissionTitle == mission.MissionTitle
                                                                    && m.CityId == mission.CityId
                                                                    && m.StartDate == mission.StartDate
                                                                    && m.EndDate == mission.EndDate
                                                                    && m.Id != mission.Id
                                                                    && !m.IsDeleted);

                if (!missionExists)
                {
                    // Find the mission in the database to update
                    var missionToUpdate = _cIDbContext.Missions.FirstOrDefault(m => m.Id == mission.Id && !m.IsDeleted);

                    if (missionToUpdate != null)
                    {
                        // Update the mission details
                        missionToUpdate.MissionTitle = mission.MissionTitle;
                        missionToUpdate.MissionDescription = mission.MissionDescription;
                        missionToUpdate.MissionOrganisationName = mission.MissionOrganisationName;
                        missionToUpdate.MissionOrganisationDetail = mission.MissionOrganisationDetail;
                        missionToUpdate.CountryId = mission.CountryId;
                        missionToUpdate.CityId = mission.CityId;
                        missionToUpdate.StartDate = mission.StartDate;
                        missionToUpdate.EndDate = mission.EndDate;
                        missionToUpdate.MissionType = mission.MissionType;
                        missionToUpdate.TotalSheets = mission.TotalSheets;
                        missionToUpdate.RegistrationDeadLine = mission.RegistrationDeadLine;
                        missionToUpdate.MissionThemeId = mission.MissionThemeId;
                        missionToUpdate.MissionSkillId = mission.MissionSkillId;
                        missionToUpdate.MissionImages = mission.MissionImages;
                        missionToUpdate.MissionDocuments = mission.MissionDocuments;
                        missionToUpdate.MissionAvilability = mission.MissionAvilability;
                        missionToUpdate.MissionVideoUrl = mission.MissionVideoUrl;
                        missionToUpdate.ModifiedDate = DateTime.Now;

                        _cIDbContext.SaveChanges();

                        result = "Update Mission Detail Successfully.";
                    }
                    else
                    {
                        throw new Exception("Mission not found.");
                    }
                }
                else
                {
                    throw new Exception("Mission with the same title, city, start date, and end date already exists.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public string DeleteMission(int id)
        {
            try
            {
                string result = "";
                    var mission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == id);
                    if (mission != null)
                    {
                        mission.IsDeleted = true;
                        _cIDbContext.SaveChanges();
                        result = "Delete Mission Detail Successfully.";
                    }
                    else
                    {
                        result = "Mission not found."; // Or any other appropriate message
                    }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MissionApplication> MissionApplicationList()
        {
            List<MissionApplication> missionApplicationList = new List<MissionApplication>();
            try
            {
                    missionApplicationList = _cIDbContext.MissionApplication
                        .Where(ma => !ma.IsDeleted) // Assuming IsDeleted is a property on MissionApplication indicating deletion status
                        .Join(_cIDbContext.Missions.Where(m => !m.IsDeleted),
                              ma => ma.MissionId,
                              m => m.Id,
                              (ma, m) => new { ma, m })
                        .Join(_cIDbContext.User.Where(u => !u.IsDeleted),
                              mm => mm.ma.UserId,
                              u => u.Id,
                              (mm, u) => new MissionApplication
                              {
                                  Id = mm.ma.Id,
                                  MissionId = mm.ma.MissionId,
                                  MissionTitle = mm.m.MissionTitle,
                                  UserId = u.Id,
                                  UserName = u.FirstName + " " + u.LastName,
                                  AppliedDate = mm.ma.AppliedDate,
                                  Status = mm.ma.Status
                              })
                        .ToList();
                }
            catch (Exception)
            {
                throw;
            }
            return missionApplicationList;
        }

        public string MissionApplicationDelete(int id)
        {
            try
            {
                var missionApplication = _cIDbContext.MissionApplication.FirstOrDefault(m => m.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.IsDeleted = true;
                    _cIDbContext.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Record not found";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string MissionApplicationApprove(int id)
        {
            try
            {
                var missionApplication = _cIDbContext.MissionApplication.FirstOrDefault(m => m.Id == id);
                if (missionApplication != null)
                {
                    missionApplication.Status = true;
                    _cIDbContext.SaveChanges();
                    return "Mission is approved";
                }
                else
                {
                    return "Mission is not approved";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<Missions> ClientSideMissionList(int userId)
        {
            List<Missions> clientSideMissionlist = new List<Missions>();
            try
            {
                clientSideMissionlist = _cIDbContext.Missions
                    .Where(m => !m.IsDeleted)
                    .OrderBy(m => m.CreatedDate)
                    .Select(m => new Missions
                    {
                        Id = m.Id,
                        CountryId = m.CountryId,
                        CountryName = m.CountryName,
                        CityId = m.CityId,
                        CityName = m.CityName,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationName = m.MissionOrganisationName,
                        MissionOrganisationDetail = m.MissionOrganisationDetail,
                        TotalSheets = m.TotalSheets,
                        RegistrationDeadLine = m.RegistrationDeadLine,
                        MissionThemeId = m.MissionThemeId,
                        MissionImages = m.MissionImages,
                        MissionDocuments = m.MissionDocuments,
                        MissionSkillId = m.MissionSkillId,
                        MissionSkillName = string.Join(",", m.MissionSkillName),
                        MissionAvilability = m.MissionAvilability,
                        MissionVideoUrl = m.MissionVideoUrl,
                        MissionType = m.MissionType,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        MissionThemeName = m.MissionThemeName,
                        MissionStatus = m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                        MissionApproveStatus = _cIDbContext.MissionApplication.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status == true) ? "Approved" : "Applied",
                        MissionDateStatus = m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus = m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus = _cIDbContext.MissionFavourites.Any(mf => mf.MissionId == m.Id && mf.UserId == userId) ? "1" : "0",
                        Rating = _cIDbContext.MissionRating.FirstOrDefault(mr => mr.MissionId == m.Id && mr.UserId == userId).Rating ?? 0
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return clientSideMissionlist;
        }

        public List<Missions> MissionClientList(SortestData data)
        {
            List<Missions> clientSideMissionlist = new List<Missions>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@UserId", data.UserId);
                    param.Add("@SortingValue", data.SortestValue);
                    clientSideMissionlist = cnn.Query<Missions>(StoreProcedure.TestingMissionListClientSide_Usp, param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientSideMissionlist;
        }

        public string ApplyMission(MissionApplication missionApplication)
        {
            string result = "";
            try
            {
                    // Begin transaction
                    using (var transaction = _cIDbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Get the mission and check if it's available
                            var mission = _cIDbContext.Missions
                                .FirstOrDefault(m => m.Id == missionApplication.MissionId && m.IsDeleted == false);

                            if (mission != null)
                            {
                                // Check if sheets are available
                                if (mission.TotalSheets >= missionApplication.Sheet)
                                {
                                    // Create a new MissionApplication entity
                                    var newApplication = new MissionApplication
                                    {
                                        MissionId = missionApplication.MissionId,
                                        UserId = missionApplication.UserId,
                                        AppliedDate = missionApplication.AppliedDate,
                                        Status = missionApplication.Status,
                                        Sheet = missionApplication.Sheet,

                                        CreatedDate = DateTime.Now,
                                        ModifiedDate = null,
                                        IsDeleted = false
                                    };

                                // Add the new application to the context
                                _cIDbContext.MissionApplication.Add(newApplication);
                                _cIDbContext.SaveChanges();

                                    // Update total sheets in the mission
                                    mission.TotalSheets -= missionApplication.Sheet;
                                _cIDbContext.SaveChanges();

                                    result = "Mission Apply Successfully.";
                                }
                                else
                                {
                                    result = "Mission Housefull";
                                }
                            }
                            else
                            {
                                result = "Mission Not Found.";
                            }

                            // Commit transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if an exception occurs
                            transaction.Rollback();
                            throw ex;
                        }
                    }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Missions MissionDetailByMissionId(SortestData data)
        {
            Missions missionDetail = new Missions();
            try
            {
                using(SqlConnection cnn =new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", data.MissionId);
                    param.Add("@UserId", data.UserId);
                    missionDetail = cnn.Query<Missions>(StoreProcedure.MissionDetailByMissionId_Usp, param, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missionDetail;
        }
        public string AddMissionComment(MissionComment missionComment)
        {
            string result = "";
            try
            {   
                using(SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionComment.MissionId);
                    param.Add("@UserId", missionComment.UserId);
                    param.Add("@CommentDescription", missionComment.CommentDescription);
                    param.Add("@CommentDate", missionComment.CommentDate);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddMissionComment_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MissionComment> MissionCommentListByMissionId(int missionId)
        {
            List<MissionComment> missionCommentList = new List<MissionComment>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionId);
                    missionCommentList = cnn.Query<MissionComment>(StoreProcedure.MissionCommentListByMissionId_Usp, param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return missionCommentList;
        }
        public string AddMissionFavourite(MissionFavourites missionFavourites)
        {
            string result = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionFavourites.MissionId);
                    param.Add("@UserId", missionFavourites.UserId);                
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddMissionFavourite_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RemoveMissionFavourite(MissionFavourites missionFavourites)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionFavourites.MissionId);
                    param.Add("@UserId", missionFavourites.UserId);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.RemoveMissionFavourite_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string MissionRating(MissionRating missionRating)
        {
            try
            {
                string result = "";
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionRating.MissionId);
                    param.Add("@UserId", missionRating.UserId);
                    param.Add("@Rating", missionRating.Rating);
                    result = Convert.ToString(cnn.ExecuteScalar(StoreProcedure.AddUpdateMissionRating_Usp, param, null, 0, CommandType.StoredProcedure));
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<MissionApplication> RecentVolunteerList(MissionApplication missionApplication)
        {
            List<MissionApplication> recentList = new List<MissionApplication>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@MissionId", missionApplication.MissionId);
                    param.Add("@UserId", missionApplication.UserId);
                    recentList = cnn.Query<MissionApplication>(StoreProcedure.RecentVolunteersList_Usp, param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recentList;
        } 
        public List<User> GetUserList(int userId)
        {
            List<User> userList = new List<User>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_cIDbContext.CreateConnection().ConnectionString))
                {
                    cnn.Open();
                    var param = new DynamicParameters();
                    param.Add("@UserId", userId);
                    userList = cnn.Query<User>(StoreProcedure.UserListShareOrInvite_Usp, param, null, true, 0, CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userList;
        }
        public string SendInviteMissionMail(List<MissionShareOrInvite> user)
        {
            string result = "";
            try
            {                              
                foreach(var item in user)
                {                
                    string callbackurl =  item.baseUrl + "/volunteeringMission/"+item.MissionId;
                    string mailTo = item.EmailAddress;
                    string userName = item.UserFullName;
                    string emailBody= "Hi " + userName + ",<br/><br/> Click the link below to suggest mission link <br/><br/> " + callbackurl;
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient();
                    mail.From = new MailAddress(item.MissionShareUserEmailAddress);
                    mail.To.Add(mailTo);
                    mail.Subject = "Invite Mission Link";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    SmtpServer.UseDefaultCredentials = false;
                    NetworkCredential NetworkCred = new NetworkCredential(item.MissionShareUserEmailAddress, "yourpassword");
                    SmtpServer.Credentials = NetworkCred;
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Send(mail);                    
                }
                result = "Mission Invite Successfully";
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
