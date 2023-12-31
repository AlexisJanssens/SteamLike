using System.Security.Claims;
using BLL.Interface;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamLike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {

        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        
        // GET: api/Friend
        [Authorize(Roles = "1")]
        
        [HttpGet("/GetFriends")]
        public ActionResult<IEnumerable<FriendDTO>> GetAllFriend ()
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int id = Convert.ToInt32(claim.Value);
            return Ok(_friendService.GetAllFriends(id));
        }

        // // GET: api/Friend/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Friend
        [HttpPost("/{receiverId}")]
        public ActionResult<bool> AskFriend(int receiverId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int askerId = Convert.ToInt32(claim.Value);
            
            bool friendship = _friendService.AskFriend(askerId, receiverId);
            return  friendship? Ok(friendship) : BadRequest(friendship);
        }

        // PUT: api/Friend/5
        [HttpPut("/{receiverId}")]
        public ActionResult<bool> AcceptFriendRequest(int receiverId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int askerId = Convert.ToInt32(claim.Value);
            
            return _friendService.AcceptFriendRequest(askerId, receiverId) ? Ok("request accepted") : BadRequest();
        }

        // DELETE: api/Friend/5
        [HttpDelete("/{receiverId}")]
        public ActionResult<bool> Delete(int receiverId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int askerId = Convert.ToInt32(claim.Value);
            return _friendService.DeleteFriendship(askerId, receiverId) ? Ok("friendship deleted") : BadRequest();
        }
    }
}
