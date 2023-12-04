using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IFriendRepository : IRepository<int, Friend>
{
 IEnumerable<FriendOfFriendList> GetAll(User user);
 Friend? GetFriendship(User user1, User user2);
 bool Delete(int id1, int id2);
}