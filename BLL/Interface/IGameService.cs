using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IGameService
{
    GameDTO? CreateGame(GameForm form);
    IEnumerable<GameDTO> GetAll();
    Game? GetById(int gameId);
    BuyingRecapDTO? BuyingGame(int gameId, int userId);



}