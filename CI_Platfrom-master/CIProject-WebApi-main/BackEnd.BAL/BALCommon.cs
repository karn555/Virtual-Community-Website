using BackEnd.DAL;
using BackEnd.Entity;
using BackEnd.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL
{
    public class BALCommon
    {
        private readonly DALCommon _dalCommon;
        public BALCommon(DALCommon dalCommon)
        {
            _dalCommon = dalCommon;
        }

        public List<DropDown> CountryList()
        {
            return _dalCommon.CountryList();
        } 
               
        public List<DropDown> CityList(int countryId)
        {
            return _dalCommon.CityList(countryId);
        }

        public List<DropDown> MissionCountryList()
        {
            return _dalCommon.MissionCountryList();
        }  
        public List<DropDown> MissionCityList()
        {
            return _dalCommon.MissionCityList();
        } 
        public List<DropDown> MissionThemeList()
        {
            return _dalCommon.MissionThemeList();
        } 
        public List<DropDown> MissionSkillList()
        {
            return _dalCommon.MissionSkillList();
        } 
        public List<DropDown> MissionTitleList()
        {
            return _dalCommon.MissionTitleList();
        }       
        public string ContactUs(ContactUs contactUs)
        {
            return _dalCommon.ContactUs(contactUs);
        }
        public string AddUserSkill(UserSkills userSkills)
        {
            return _dalCommon.AddUserSkill(userSkills);
        }
        public List<DropDown> GetUserSkill(int userId)
        {
            return _dalCommon.GetUserSkill(userId);
        }
    }
}
