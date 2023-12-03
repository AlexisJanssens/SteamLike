using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IUserRepository : IRepository<int, User>
{
    User? GetByMail(string mail);
}