using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IGameRepository : IRepository<int, Game>
{
    bool BuyGame(GameOfGameList game);
    GameOfGameList? GetById(int gameId, int userId);
}