using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class TrainsController : ApiController
    {
        private readonly ITrainsService _trainService;
        public TrainsController(ITrainsService trainService)
        {
            _trainService = trainService;
        }


        [Route("api/v1/Trains")]
        public IEnumerable<TrainViewModel> GetTrains()
        {
            return _trainService.GetTrains(User.Identity.GetUserId<int>());
        }


        [Route("api/v1/Trains/GetTrainTasks")]
        public IEnumerable<ExamTaskViewModel> GetTrainTasks([FromUri]int taskId)
        {
            return _trainService.GetTrainTasks(taskId, User.Identity.GetUserId<int>());
        }
    }
}