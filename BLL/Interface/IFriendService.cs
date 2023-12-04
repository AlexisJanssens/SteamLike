using BLL.Models;

namespace BLL.Interface;

public interface IFriendService
{
    bool AskFriend(int askerId, int receiverId);
    bool AcceptFriendRequest(int askerId, int receiverId);
    bool DeleteFriendship(int user1, int user2);

    IEnumerable<FriendDTO> GetAllFriends(int id);

}