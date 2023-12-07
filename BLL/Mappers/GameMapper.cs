using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers;

public static class GameMapper
{
    public static Game ToGame(this GameForm form)
    {
        return new Game()
        {
            GameId = 0,
            Name = form.Name,
            DevId = form.DevId,
            Version = form.Version
        };
    }

    public static GameDTO ToGameDTO(this Game entity)
    {
        return new GameDTO()
        {
            GameId = entity.GameId,
            Name = entity.Name,
            Version = entity.Version
        };
    }
}