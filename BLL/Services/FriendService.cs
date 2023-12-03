using BLL.Interface;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class FriendService : IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;

    public FriendService(IFriendRepository friendRepository, IUserRepository userRepository)
    {
        _friendRepository = friendRepository;
        _userRepository = userRepository;
    }

    public bool AskFriend(int askerId, int receiverId)
    {
        User? askerUser = _userRepository.Get(askerId);
        User? receiverUser = _userRepository.Get(receiverId);

        if (askerUser == null || receiverUser == null)
        {
            return false;
        }

        if (_friendRepository.GetFriendship(askerUser, receiverUser) != null)
        {
            return false;
        }

        Friend friendship = new Friend()
        {
            UserAskerId = askerId,
            UserReceiverId = receiverId,
            CreationDate = DateTime.Now,
            Status = 1
        };

        _friendRepository.Create(friendship);

        return true;
    }
}