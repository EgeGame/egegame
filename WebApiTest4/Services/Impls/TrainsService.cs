using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class TrainsService: ITrainsService
    {
        private ExamAppDbContext _dbContext;

        public TrainsService(ExamAppDbContext context)
        {
            _dbContext = context;
        }

        public List<TrainViewModel> GetTrains(int userId)
        {
            var user = _dbContext.Users.OfRole("student").FirstOrDefault(x => x.Id == userId);

            return user.Trains.OfType<ExamTrain>()
                .Select(TrainViewModel.ProjectionFunc)
                .ToList();
        }

        public List<ExamTaskViewModel> GetTrainTasks(int taskId, int userId)
        {
            var user = _dbContext.Users.OfRole("student").FirstOrDefault(x => x.Id == userId);

            return user.Trains.OfType<ExamTrain>()
                .Where(x => x.Id == taskId)
                .SelectMany(x => x.TaskAttempts
                    .Select(y => ExamTaskViewModel.ProjectionFunc(y.ExamTask)))
                .ToList();
        }
    }
}