using BackEnd.BAL;
using BackEnd.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly BALStory _balStory;
        ResponseResult result = new ResponseResult();
        public StoryController(BALStory balStory)
        {
            _balStory = balStory;
        }
        #region ClientSide

        
        [HttpGet]
        [Route("GetMissionTitle")]
        public ResponseResult GetMissionTitle()
        {
            try
            {
                result.Data = _balStory.GetMissionTitle();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("AddStory")]
        public ResponseResult AddStory(Story story)
        {
            try
            {
                result.Data = _balStory.AddStory(story);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("ClientSideStoryList")]
        public ResponseResult ClientSideStoryList()
        {
            try
            {
                result.Data = _balStory.ClientSideStoryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("StoryDetailById/{id}")]
        public ResponseResult StoryDetailById(int id)
        {
            try
            {
                result.Data = _balStory.StoryDetailById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region AdminSide


        [HttpGet]
        [Route("AdminSideStoryList")]
        [Authorize]
        public ResponseResult AdminSideStoryList()
        {
            try
            {
                result.Data = _balStory.AdminSideStoryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }   
       
        [HttpPost]
        [Route("StoryStatusActive")]
        [Authorize]
        public ResponseResult StoryStatusActive(Story story)
        {
            try
            {
                result.Data = _balStory.StoryStatusActive(story);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteStory/{id}")]
        [Authorize]
        public ResponseResult DeleteStory(int id)
        {
            try
            {
                result.Data = _balStory.DeleteStory(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("StoryDetailByIdAdmin/{id}")]
        [Authorize]
        public ResponseResult StoryDetailByIdAdmin(int id)
        {
            try
            {
                result.Data = _balStory.StoryDetailByIdAdmin(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
