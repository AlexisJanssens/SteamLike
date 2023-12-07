using System.Security.Claims;
using BLL.Interface;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
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
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int devId = Convert.ToInt32(claim.Value);
            form.DevId = devId;
            
            GameDTO? gameDto = _gameService.CreateGame(form);

            return gameDto is null ? BadRequest() : Ok(gameDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameDTO>> GetStore()
        {
            return Ok(_gameService.GetAll());
        }

        [HttpPost("/BuyGame/{gameId}")]
        public ActionResult<BuyingRecapDTO> BuyingGame(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            BuyingRecapDTO? recap = _gameService.BuyingGame(gameId, userId);

            return recap is null ? BadRequest() : Ok(recap);
        }
        
        [HttpPost("/OfferGame/{gameId}/{receiverId}")]
        public ActionResult<BuyingRecapDTO> BuyingGame(int gameId, int receiverId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            BuyingRecapDTO? recap = _gameService.BuyingGame(gameId, userId, receiverId);

            return recap is null ? BadRequest() : Ok(recap);
        }

        [HttpPut("/Refund/{gameId}")]
        public ActionResult<RefundDTO> RefundGame(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            RefundDTO? refund = _gameService.RefundGame(userId, gameId);

            return refund is null ? BadRequest() : Ok(refund);
        }

        [HttpGet("/GetMyGames")]
        public ActionResult<GameOfLibrary> GetAllUserGames()
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            return Ok(_gameService.GetMyGames(userId));
        }

        [HttpPut("/UpdateGame/{gameId}")]
        public ActionResult<GameDTO> UpdateGame(GameForm form, int gameId)
        {
            GameDTO? updatedGame = _gameService.UpdateGame(form, gameId);

            return updatedGame is null ? BadRequest() : Ok(updatedGame);
        }

        [HttpPut("/UpdatePrice")]

        public ActionResult<bool> UpdatePrice(PriceForm form)
        {
            return _gameService.UpdatePrice(form) ? Ok() : BadRequest();
        }

        [HttpGet("/GetSales")]
        [Authorize(Roles = "1")]

        public ActionResult<SoldGame> GetSales()
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int devId = Convert.ToInt32(claim.Value);

            return Ok(_gameService.GetSales(devId));
        }

        [HttpPost("/AddWhish/{gameId}")]

        public ActionResult<BuyingRecapDTO> AddWhish(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            BuyingRecapDTO? recapDto = _gameService.AddWhish(gameId, userId);

            return recapDto is null ? BadRequest() : Ok(recapDto);
        }

        [HttpPatch("/EnterInGame/{gameId}")]
        public ActionResult<bool> EnterInGame(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            return _gameService.EnterInGame(userId, gameId) ? Ok("Enter in Game") : BadRequest();
        }
        
        [HttpPatch("/QuitInGame/{gameId}")]
        public ActionResult<bool> QuitInGame(int gameId)
        {
            var claim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            return _gameService.QuitInGame(userId, gameId) ? Ok("Quit in Game") : BadRequest();
        }


    }
}
