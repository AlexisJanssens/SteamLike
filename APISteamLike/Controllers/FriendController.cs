using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<FriendDTO>> GetAllFriend (int id)
        {
            return Ok(_friendService.GetAllFriends(id));
        }

        // // GET: api/Friend/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Friend
        [HttpPost("{askedId}/{receiverId}")]
        public ActionResult<bool> AskFriend(int askedId, int receiverId)
        {
            bool friendship = _friendService.AskFriend(askedId, receiverId);
            return  friendship? Ok(friendship) : BadRequest(friendship);
        }

        // PUT: api/Friend/5
        [HttpPut("{askerId}/{receiverId}")]
        public ActionResult<bool> AcceptFriendRequest(int askerId, int receiverId)
        {
            return _friendService.AcceptFriendRequest(askerId, receiverId) ? Ok("request accepted") : BadRequest();
        }

        // DELETE: api/Friend/5
        [HttpDelete("{askerId}/{receiverId}")]
        public ActionResult<bool> Delete(int askerId, int receiverId)
        {
            return _friendService.DeleteFriendship(askerId, receiverId) ? Ok("friendship deleted") : BadRequest();
        }
    }
}
