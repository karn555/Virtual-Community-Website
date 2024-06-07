using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DAL;
using BackEnd.Entity;
namespace BackEnd.BAL
{
    public class BALMissionTheme
    {
        private readonly DALMissionTheme _dalMissionTheme;
        public BALMissionTheme(DALMissionTheme dalMissionTheme)
        {
            _dalMissionTheme = dalMissionTheme;
        }

        public List<MissionTheme> GetMissionThemeList()
        {
            return _dalMissionTheme.GetMissionThemeList();
        }
        public MissionTheme GetMissionThemeById(int id)
        {
            return _dalMissionTheme.GetMissionThemeById(id);
        }

        public string AddMissionTheme(MissionTheme missionTheme)
        {
            return _dalMissionTheme.AddMissionTheme(missionTheme);
        }
        public string UpdateMissionTheme(MissionTheme missionTheme)
        {
            return _dalMissionTheme.UpdateMissionTheme(missionTheme);
        }
        public string DeleteMissionTheme(int id)
        {
            return _dalMissionTheme.DeleteMissionTheme(id);
        }
    }
}
