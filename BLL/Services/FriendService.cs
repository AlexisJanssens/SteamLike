using BLL.Interface;
using BLL.Mappers;
using BLL.Models;
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

        Friend friendship = new Friend
        {
            UserAskerId = askerId,
            UserReceiverId = receiverId,
            CreationDate = DateTime.Now,
            Status = 1
        };

        _friendRepository.Create(friendship);

        return true;
    }

    public bool AcceptFriendRequest(int askerId, int receiverId)
    {
        User? askerUser = _userRepository.Get(askerId);
        User? receiverUser = _userRepository.Get(receiverId);
        
        if (askerUser == null || receiverUser == null)
        {
            return false;
        }
        
        Friend? friendship = _friendRepository.GetFriendship(askerUser, receiverUser);
        friendship!.Status = 2;
        
        return _friendRepository.Update(friendship);
    }

    public bool DeleteFriendship(int user1, int user2)
    {
        User? askerUser = _userRepository.Get(user1);
        User? receiverUser = _userRepository.Get(user2);
        
        if (askerUser == null || receiverUser == null)
        {
            return false;
        }

        return _friendRepository.Delete(user1, user2);
    }

    public IEnumerable<FriendDTO> GetAllFriends(int id)
    {
        User? user = _userRepository.Get(id);

        if (user == null)
        {
            return new List<FriendDTO>();
        }
        
        _friendRepository.GetAll(user);

        return _friendRepository.GetAll(user).Select(x => x.ToFriendDTO());
    }
}