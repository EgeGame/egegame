using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.ApiViewModels;
using WebApiTest4.ApiViewModels.BindingModels;

namespace WebApiTest4.Services
{
    public interface ISolvedTasksService
    {
        IEnumerable<AttemptViewModel> GetUncheckedAttemptsOfMyStudents(int teacher_id, int offset, int limit);
        void CheckAttemptsByTeacher(int teacherId, IEnumerable<CheckedAttemptBindigModel> checkedAttempts);
    }
}
