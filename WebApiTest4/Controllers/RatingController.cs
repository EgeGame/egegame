using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Services;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class RatingController : ApiController
    {
        private readonly IUserService _userService;
        public RatingController(IUserService service)
        {
            _userService = service;
        }

        [Route("api/v1/Rating")]
        public IEnumerable<UserViewModel> Get()
        {

            return _userService.GetRatingForUser(User
                .Identity
                .GetUserId<int>());
        }
    }
}
