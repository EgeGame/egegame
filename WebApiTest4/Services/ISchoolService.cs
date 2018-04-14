using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services
{
    public interface ISchoolService
    {
        School GetSchoolById(int schoolId);
        SchoolViewModel GetSchoolVMById(int id);
        bool SchoolExists(int id);
        IEnumerable<SchoolViewModel> GetSchools();
    }
}
