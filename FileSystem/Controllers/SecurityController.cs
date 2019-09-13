﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InclusCommunication.Http.Requests;
using InclusCommunication.Http.Responses;
using InclusCommunication.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InclusCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly SecurityProvider Security;
        public SecurityController(SecurityProvider security)
        {
            Security = security;
        }

        [HttpPost]
        public AbstractResponse Post(LoginRequest request)
        {
            return Security.GetResponse(request);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}