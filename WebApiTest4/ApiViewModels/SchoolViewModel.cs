using System.Collections.Generic;
using System.Linq;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class SchoolViewModel
    {
        public SchoolViewModel(School school)
        {
            id = school.Id;
            title = school.Title;
        }

        public int id { get; private set; }
        public string title { get; private set; }
    }
}