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
    public class BALStory
    {
        private readonly DALStory _dalStory;
        public BALStory(DALStory dalStory)
        {
            _dalStory = dalStory;
        }
        #region ClientSide

        
        public List<DropDown> GetMissionTitle()
        {
            return _dalStory.GetMissionTitle();
        }
        public string AddStory(Story story)
        {
            return _dalStory.AddStory(story);
        }
        public List<Story> ClientSideStoryList()
        {
            return _dalStory.ClientSideStoryList();
        }
        public Story StoryDetailById(int id)
        {
            return _dalStory.StoryDetailById(id);
        } 
     
        #endregion

        #region AdminSide

        public List<Story> AdminSideStoryList()
        {
            return _dalStory.AdminSideStoryList();
        } 
    
        public string StoryStatusActive(Story story)
        {
            return _dalStory.StoryStatusActive(story);
        }
        public string DeleteStory(int id)
        {
            return _dalStory.DeleteStory(id);
        }
        public Story StoryDetailByIdAdmin(int id)
        {
            return _dalStory.StoryDetailByIdAdmin(id);
        }
        #endregion
    }
}
