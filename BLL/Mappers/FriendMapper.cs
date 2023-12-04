using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers;

public static class FriendMapper
{
    public static FriendDTO ToFriendDTO(this FriendOfFriendList friend)
    {
        return new FriendDTO()
        {
            FriendId = friend.UserAskerId,
            CreationDate = friend.CreationDate,
            NickName = friend.NickName,
        };
    }
}