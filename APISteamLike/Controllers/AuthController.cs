using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamLike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }
        
        
        //POST : API/Auth
        [AllowAnonymous]
        [HttpPost("/auth")]
        public ActionResult<string> Login(LoginForm form)
        {
            User? user = _authService.Login(form);
            
            if (user is null)
            {
                return BadRequest();
            }

            return Ok(_jwtService.GenerateToken(user));
        }
    }
}
