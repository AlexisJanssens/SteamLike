namespace BLL.Interface;

public interface IFriendService
{
    bool AskFriend(int askerId, int receiverId);
    
}