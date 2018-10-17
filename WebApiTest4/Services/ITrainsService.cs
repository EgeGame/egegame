using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models;

namespace WebApiTest4.Services
{
    public interface ITrainsService
    {
        List<TrainViewModel> GetTrains(int userId);

        List<ExamTaskViewModel> GetTrainTasks(int taskId, int userId);
    }
}
