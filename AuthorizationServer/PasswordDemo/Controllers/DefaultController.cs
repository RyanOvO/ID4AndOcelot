using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PasswordDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [Authorize(Roles = "superadmin,admin")]
        [Route("getclaims")]
        [HttpGet]
        public IActionResult GetClaims()
        {
            return new JsonResult(from c in HttpContext.User.Claims select new { c.Type, c.Value });
        }
    }
}