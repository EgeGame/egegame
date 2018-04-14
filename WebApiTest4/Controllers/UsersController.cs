using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private ApplicationUserManager _userManager;
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
        private readonly IUserService _userService;
        public UsersController(IUserService service)
        {
            _userService = service;
        }
        //// GET: api/Users
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Users/5

        [HttpGet]
        [Route("api/v1/Users/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id == 0)
            {
                return Ok(_userService.GetUser(User.Identity.GetUserId<int>()));
            }
            if (_userService.UserExists(id))
            {
                if (UserManager.IsInClaimRole(id, "student"))
                {
                    return Ok(_userService.GetUser(id));
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();


        }

        //// POST: api/Users
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Users/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Users/5
        //public void Delete(int id)
        //{
        //}
        
    }
}
