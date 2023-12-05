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
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<UserDTO> GetById(int id)
        {
            UserDTO? user = _userService.Get(id);

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
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            return _userService.Delete(id) ? Ok("User deleted") : BadRequest();
        }
    }
}
