using System.Security.Claims;
using BLL.Interface;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamLike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        
        
        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            return Ok(_userService.GetAll());
        }

        // GET: api/User/5
        [HttpGet]
        public ActionResult<UserDTO> GetById()
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);
            
            UserDTO? user = _userService.Get(userId);

            return user == null ? NotFound() : Ok(user);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UserDTO> Post(UserForm form)
        {
            UserDTO? userDto = _userService.Create(form);
            return userDto == null ? BadRequest() : Ok(userDto);
        }

        // PUT: api/User/5
        [HttpPut]
        public ActionResult<bool> Update(UpdateUserForm form)
        {
            return _userService.Update(form) ? Ok() : BadRequest();
        }

        // DELETE: api/User/5
        [HttpDelete]
        public ActionResult<bool> Delete()
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);
            return _userService.Delete(userId) ? Ok("User deleted") : BadRequest();
        }
        
        // PATCH: api/UpdateWallet
        [HttpPatch("{amount}")]
        public ActionResult<bool> UpdateWallet(double amount)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            return _userService.UpdateWallet(amount, userId) ? Ok("Wallet Updated") : BadRequest();
        }
    }
}
