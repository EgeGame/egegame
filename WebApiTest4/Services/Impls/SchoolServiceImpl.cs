using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class SchoolServiceImpl : ISchoolService
    {
        private readonly ExamAppDbContext _context;
        public SchoolServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }
        public School GetSchoolById(int schoolId)
        {
            return _context.Schools.FirstOrDefault(x => x.Id == schoolId);
        }

        public SchoolViewModel GetSchoolVMById(int id)
        {
            return new SchoolViewModel(GetSchoolById(id));
        }

        public bool SchoolExists(int id)
        {
            return _context.Schools.Any(x => x.Id == id);
        }

        public IEnumerable<SchoolViewModel> GetSchools()
        {
            return _context.Schools.ToList().Select(x => new SchoolViewModel(x));
        }
    }
}