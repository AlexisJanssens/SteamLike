using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamLike.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        [HttpPost("/CreateGame")]
        [Authorize(Roles = "1")]
        public ActionResult<GameDTO?> CreateGame(GameForm form)
        {
            GameDTO? gameDto = _gameService.CreateGame(form);

            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int devId = Convert.ToInt32(claim.Value);
            form.DevId = devId;

            return gameDto is null ? BadRequest() : Ok(gameDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameDTO>> GetStore()
        {
            return Ok(_gameService.GetAll());
        }

        [HttpPost("/BuyGame/{gameId}")]
        [AllowAnonymous]
        public ActionResult<BuyingRecapDTO> BuyingGame(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            return Ok(_gameService.BuyingGame(gameId, userId));
        }
    }
}
