using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InclusCommunication.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}