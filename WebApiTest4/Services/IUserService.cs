using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services
{
    public interface IUserService
    {
        UserViewModel GetUser(int id);
        IEnumerable<UserViewModel> GetRatingForUser(int userId);
        void AddCurrentExam(User user, Type type);
        User GetUserById(int id);
        TeacherViewModel GetTeacherVMById(int id);
        IEnumerable<TeacherViewModel> GetTeachers();
        bool UserExists(int id);
        void ClearDataForTeacher(int userId);
    }
}
