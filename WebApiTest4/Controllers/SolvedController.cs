using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebApiTest4.ApiViewModels.BindingModels;
using WebApiTest4.Services;
using WebApiTest4.Util;

namespace WebApiTest4.Controllers
{
    [ClaimsAuthorize(ClaimTypes.Role, "teacher")]
    public class SolvedController : ApiController
    {
        private readonly ISolvedTasksService _solvedTasksService;
        public SolvedController(ISolvedTasksService solvedTasksService)
        {
            _solvedTasksService = solvedTasksService;
        }

        [HttpGet]
        [Route("api/v1/Solved/GetTask")]
        public IHttpActionResult GetTask(int taskId, int studentId)
        {
            return Ok(_solvedTasksService.GetTask(taskId, studentId));
        }

        [HttpGet]
        [Route("api/v1/Solved/GetForMy")]
        public IHttpActionResult GetForMy([FromUri]int offset, [FromUri]int limit, [FromUri] bool? is_checked = null)
        {
            if (offset >= 0 && limit >= 0)
            {
                return Ok(_solvedTasksService.GetUncheckedAttemptsOfMyStudents(User.Identity.GetUserId<int>(), is_checked, offset, limit));
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("api/v1/Solved/GetByTopic")]
        public IHttpActionResult GetByTopic([FromUri]int topic_id, [FromUri]int offset, [FromUri]int limit, [FromUri] bool? only_unchecked = null)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok(_solvedTasksService.GetUncheckedAttemptsByTopic(topic_id, only_unchecked, User.Identity.GetUserId<int>(), offset, limit));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/v1/Solved/GetByType")]
        public IHttpActionResult GetByType([FromUri]int type, [FromUri]int offset, [FromUri]int? limit, [FromUri] bool? only_unchecked = null)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok(_solvedTasksService.GetUncheckedAttemptsByType(type, only_unchecked, User.Identity.GetUserId<int>(), offset, limit ?? 0));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/v1/Solved/GetByStudent")]
        public IHttpActionResult GetByStudent([FromUri]int student_id, [FromUri]int offset, [FromUri]int limit, [FromUri] bool? only_unchecked = null)
        {

            if (offset >= 0 && limit >= 0)
            {
                return Ok(_solvedTasksService.GetUncheckedAttemptsByStudent(student_id, only_unchecked, User.Identity.GetUserId<int>(), offset, limit));
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Solved
        [HttpPost]
        [Route("api/v1/Solved")]
        public IHttpActionResult Post([FromBody] IEnumerable<CheckedAttemptBindigModel> checkedAttempts)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).ToString());
            }
            _solvedTasksService.CheckAttemptsByTeacher(User.Identity.GetUserId<int>(), checkedAttempts);
            return Ok();

        }
        
    }
}
