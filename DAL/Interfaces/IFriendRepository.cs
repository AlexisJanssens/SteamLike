using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IFriendRepository : IRepository<int, Friend>
{
 IEnumerable<Friend> GetAll(User user);
}