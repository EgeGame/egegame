using System.Collections.Generic;
using System.Linq;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class TeacherViewModel
    {
        public TeacherViewModel(User teacher)
        {
            id = teacher.Id;
            name = teacher.Name;
            avatar = teacher.Avatar;
            if (teacher.School != null)
                school_id = teacher.School.Id;
        }

        public int id { get; private set; }
        public string name { get; private set; }
        public string avatar { get; private set; }
        public int? school_id { get; set; }
        
    }
}