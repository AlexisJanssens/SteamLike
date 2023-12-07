using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IPriceRepository : IRepository<int, Price>
{
    Price? Get(int gameId, DateTime date);
}