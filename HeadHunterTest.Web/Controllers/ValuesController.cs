using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeadHunterTest.Web.Controllers
{
    [Route("Value")]
    public class ValuesController : Controller
    {
        private readonly IUserService _userService;
        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<object> GetUsers()
        {
            return await _userService.Add(null);
        }
    }
}
