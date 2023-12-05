using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IGameService
{
    Game CreateGame(GameForm form);
    
}