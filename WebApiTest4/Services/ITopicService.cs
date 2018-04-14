using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.ApiViewModels;

namespace WebApiTest4.Services
{
    public interface ITopicService
    {
        IEnumerable<ExamTopicViewModel> GetTopics();
        IEnumerable<ExamTopicViewModel> GetTopicsForUser(int userId);
    }
}
