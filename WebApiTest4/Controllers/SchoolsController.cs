using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    public class SchoolsController : ApiController
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService service)
        {
            _schoolService = service;
        }
        // GET: api/Schools
        [HttpGet]
        [Route("api/v1/Schools")]
        public IEnumerable<SchoolViewModel> Get()
        {
            return _schoolService.GetSchools();
        }

        // GET: api/Schools/5
        [HttpGet]
        [Route("api/v1/Schools/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (_schoolService.SchoolExists(id))
            {
                return Ok(_schoolService.GetSchoolVMById(id));
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
