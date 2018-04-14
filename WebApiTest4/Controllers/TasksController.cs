using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApiTest4.ApiViewModels;
using WebApiTest4.ApiViewModels.BindingModels;
using WebApiTest4.Services;
using WebApiTest4.Util;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        const int defaultLimit = 20;


        [HttpGet]
        [Route("api/v1/Tasks/{task_id}")]
        public IHttpActionResult Get([FromUri]int task_id)
        {

            if (_taskService.TaskExists(task_id))
            {
                return Ok(_taskService.GetTask(task_id, User.Identity.GetUserId<int>()));
            }
            return NotFound();
        }

        ////GET: api/Tasks?
        //[Route("api/v1/Tasks")]
        //public IEnumerable<ExamTaskViewModel> Get([FromUri]int? offset, [FromUri]int? limit)
        //{
        //    return _taskService.GetSortedTasks(null, offset ?? 0, limit ?? defaultLimit, User.Identity.GetUserId<int>());
        //}

        //GET: api/Tasks?
        [Route("api/v1/Tasks")]
        public IEnumerable<ExamTaskViewModel> GetByTopic([FromUri]int? topic_id, [FromUri]int? offset, [FromUri]int? limit)
        {
            return _taskService.GetSortedTasks(topic_id, offset ?? 0, limit ?? defaultLimit, User.Identity.GetUserId<int>());
        }
        //GET: api/Tasks?
        [Route("api/v1/Tasks/GetByType")]
        public IEnumerable<ExamTaskViewModel> GetByType([FromUri]int? type, [FromUri]int? offset, [FromUri]int? limit)
        {

            return _taskService.GetTasksByType(type ?? 0, offset ?? 0, limit ?? defaultLimit, User.Identity.GetUserId<int>());
        }

        
        [HttpPost]
        [Authorize]
        [ClaimsAuthorize(ClaimTypes.Role, "student")]
        [Route("api/v1/Tasks/PostCheck")]
        public IHttpActionResult PostCheck(TaskAnswersSetBindingModel answers)
        {
            if (answers.list.Any() && ModelState.IsValid)
            {
                var result = _taskService
                    .CheckAnswers(
                        answers.train_type,
                        answers.list,
                        User
                            .Identity
                            .GetUserId<int>()
                    );
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [ActionName("Train")]
        [ClaimsAuthorize(ClaimTypes.Role, "student")]
        [Route("api/v1/Tasks/GetTrain")]
        public IEnumerable<ExamTaskViewModel> GetTrain()
        {
           return _taskService.GenerateNewExamTrain(User
                .Identity
                .GetUserId<int>());
        }

        //// GET: api/Tasks/5
        //public ExamTaskViewModel Get(int id)
        //{
        //    return _taskService.GetTask(id);
        //}
        //// POST: api/Tasks
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Tasks/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Tasks/5
        //public void Delete(int id)
        //{
        //}
    }
}
