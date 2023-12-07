using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IGameService
{
    GameDTO? CreateGame(GameForm form);
    IEnumerable<GameDTO> GetAll();
    Game? GetById(int gameId);
    BuyingRecapDTO? BuyingGame(int gameId, int userId);
    BuyingRecapDTO? BuyingGame(int gameId, int userId, int receiverId);
    RefundDTO? RefundGame(int userId, int gameId);
    IEnumerable<GameOfLibrary> GetMyGames(int userId);
    GameDTO? UpdateGame(GameForm form, int gameId);
    bool UpdatePrice(PriceForm form);
    IEnumerable<SoldGame> GetSales(int devId);
    BuyingRecapDTO? AddWhish(int gameId, int userId);
    bool EnterInGame(int userId, int gameId);
    bool QuitInGame(int userId, int gameId);



}