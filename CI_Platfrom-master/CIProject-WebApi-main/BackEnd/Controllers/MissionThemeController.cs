using BackEnd.BAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Entity;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionThemeController : ControllerBase
    {
        private readonly BALMissionTheme _balMissionTheme;
        ResponseResult result = new ResponseResult();
        public MissionThemeController(BALMissionTheme balMissionTheme)
        {
            _balMissionTheme = balMissionTheme;
        }

        [HttpGet]
        [Route("GetMissionThemeList")]
        [Authorize]
        public ResponseResult GetMissionThemeList()
        {
            try
            {
                result.Data = _balMissionTheme.GetMissionThemeList();
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
        [Route("GetMissionThemeById/{id}")]
        [Authorize]
        public ResponseResult GetMissionThemeById(int id)
        {
            try
            {
                result.Data = _balMissionTheme.GetMissionThemeById(id);
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
        [Route("AddMissionTheme")]
        [Authorize]
        public ResponseResult AddMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                result.Data = _balMissionTheme.AddMissionTheme(missionTheme);
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
        [Route("UpdateMissionTheme")]
        [Authorize]
        public ResponseResult UpdateMissionTheme(MissionTheme missionTheme)
        {
            try
            {
                result.Data = _balMissionTheme.UpdateMissionTheme(missionTheme);
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
        [Route("DeleteMissionTheme/{id}")]
        [Authorize]
        public ResponseResult DeleteMissionTheme(int id)
        {
            try
            {
                result.Data = _balMissionTheme.DeleteMissionTheme(id);
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
