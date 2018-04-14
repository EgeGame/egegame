using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;
using WebApiTest4.Util;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class TeachersController : ApiController
    {
        public TeachersController() { }

        private ApplicationUserManager _userManager;
        private readonly IUserService _userService;

        public TeachersController(IUserService service)
        {
            _userService = service;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/Teachers
        [Route("api/v1/Teachers")]
        public IEnumerable<TeacherViewModel> Get()
        {
            return _userService.GetTeachers();
        }

        // GET: api/v1/Teachers/5
        [HttpGet]
        [Route("api/v1/Teachers/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (UserManager.IsInClaimRole(id, "teacher"))
            {
                return Ok(_userService.GetTeacherVMById(id));
            }
            else
            {
                return NotFound();
            }
        }
        [ClaimsAuthorize(ClaimTypes.Role, "student")]
        public IHttpActionResult PostBecome([FromBody] string key)
        {
            var rm = new ResourceManager("WebApiTest4.Controllers.SecretTeacherKey", Assembly.GetExecutingAssembly());
            if (rm.GetString("key").Equals(key))
            {
                var userId = User.Identity.GetUserId<int>();
                UserManager.RemoveClaim(userId, new Claim(ClaimTypes.Role, "student"));
                UserManager.AddClaim(userId, new Claim(ClaimTypes.Role, "teacher"));
                _userService.ClearDataForTeacher(userId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
                
        }
        
    }
}
