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
    public class MissionSkillController : ControllerBase
    {
        private readonly BALMissionSkill _balMissionSkill;
        ResponseResult result = new ResponseResult();
        public MissionSkillController(BALMissionSkill balMissionSkill)
        {
            _balMissionSkill = balMissionSkill;
        }

        [HttpGet]
        [Route("GetMissionSkillList")]
        [Authorize]
        public ResponseResult GetMissionSkillList()
        {
            try
            {
                result.Data = _balMissionSkill.GetMissionSkillList();
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
        [Route("GetMissionSkillById/{id}")]
        [Authorize]
        public ResponseResult GetMissionSkillById(int id)
        {
            try
            {
                result.Data = _balMissionSkill.GetMissionSkillById(id);
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
        [Route("AddMissionSkill")]
        [Authorize]
        public ResponseResult AddMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                result.Data = _balMissionSkill.AddMissionSkill(missionSkill);
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
        [Route("UpdateMissionSkill")]
        [Authorize]
        public ResponseResult UpdateMissionSkill(MissionSkill missionSkill)
        {
            try
            {
                result.Data = _balMissionSkill.UpdateMissionSkill(missionSkill);
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
        [Route("DeleteMissionSkill/{id}")]
        [Authorize]
        public ResponseResult DeleteMissionSkill(int id)
        {
            try
            {
                result.Data = _balMissionSkill.DeleteMissionSkill(id);
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
