using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DAL;
using BackEnd.Entity;
using BackEnd.Entity.Common;
namespace BackEnd.BAL
{
    public class BALVolunteeringTimesheet
    {
        private readonly DALVolunteeringTimesheet _dalVolunteeringTimesheet;
        public BALVolunteeringTimesheet(DALVolunteeringTimesheet dalVolunteeringTimesheet)
        {
            _dalVolunteeringTimesheet = dalVolunteeringTimesheet;
        }
        #region Volunteering Hours
        public List<VolunteeringHours> GetVolunteeringHoursList(int userId)
        {
            return _dalVolunteeringTimesheet.GetVolunteeringHoursList(userId);
        }
        public VolunteeringHours GetVolunteeringHoursListById(int id)
        {
            return _dalVolunteeringTimesheet.GetVolunteeringHoursListById(id);
        }
        public string AddVolunteeringHours(VolunteeringHours volunteeringHours)
        {
            return _dalVolunteeringTimesheet.AddVolunteeringHours(volunteeringHours);
        }

        public string UpdateVolunteeringHours(VolunteeringHours volunteeringHours)
        {
            return _dalVolunteeringTimesheet.UpdateVolunteeringHours(volunteeringHours);
        }
        public List<DropDown> VolunteeringMissionList(int userId)
        {
            return _dalVolunteeringTimesheet.VolunteeringMissionList(userId);
        }
        public string DeleteVolunteeringHours(int id)
        {
            return _dalVolunteeringTimesheet.DeleteVolunteeringHours(id);
        }

        #endregion


        #region Volunteering Goals
        public List<VolunteeringGoals> GetVolunteeringGoalsList(int userId)
        {
            return _dalVolunteeringTimesheet.GetVolunteeringGoalsList(userId);
        }
        public VolunteeringGoals GetVolunteeringGoalsListById(int id)
        {
            return _dalVolunteeringTimesheet.GetVolunteeringGoalsListById(id);
        }
        public string AddVolunteeringGoals(VolunteeringGoals volunteeringGoals)
        {
            return _dalVolunteeringTimesheet.AddVolunteeringGoals(volunteeringGoals);
        }

        public string UpdateVolunteeringGoals(VolunteeringGoals volunteeringGoals)
        {
            return _dalVolunteeringTimesheet.UpdateVolunteeringGoals(volunteeringGoals);
        }
        public string DeleteVolunteeringGoals(int id)
        {
            return _dalVolunteeringTimesheet.DeleteVolunteeringGoals(id);
        }

        #endregion
    }
}
