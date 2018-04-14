using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.ApiViewModels;
using WebApiTest4.ApiViewModels.BindingModels;

namespace WebApiTest4.Services
{
    public interface ITaskService
    {
        ExamTaskViewModel GetTask(int id, int userId);
        IEnumerable<ExamTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit, int userId);
        IEnumerable<AnswerViewModel> CheckAnswers(string trainType, IEnumerable<TaskAnswerBindingModel> answers, int userId);
        IEnumerable<ExamTaskViewModel> GenerateNewExamTrain(int userId);
        IEnumerable<ExamTaskViewModel> GetTasksByType(int type, int offset, int limit, int userId);
        bool TaskExists(int id);
    }
}
