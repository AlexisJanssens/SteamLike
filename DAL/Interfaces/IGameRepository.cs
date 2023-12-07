using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IGameRepository : IRepository<int, Game>
{
    bool BuyGame(GameOfGameList game);
    GameOfGameList? GetById(int gameId, int userId);
    bool UpdateGameList(GameOfGameList game);
    IEnumerable<GameOfLibrary> GetMyGames(int userId);
    IEnumerable<SoldGame> GetAllSales(int devId);
    bool AddWhish(GameOfGameList game);
    bool EnterInGame(int userId, int gameId);
    bool QuitInGame(int userId, int gameId);
    bool AddGameTime(int userId, int gameId);



}