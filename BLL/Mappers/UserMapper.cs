using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers;

public static class UserMapper
{
    public static UserDTO ToUserDTO(this User user)
    {
        
        
        return new UserDTO()
        {
            UserID = user.UserId,
            NickName = user.NickName,
            Mail = user.Email,
            Wallet = user.Wallet,
            Editor = user.EditorName,
            Role = user.Role,
            Status = user.Status
        };
    }
    
    public static User ToUser(this UserForm form)
    {
        return new User()
        {
            UserId = 0,
            NickName = form.NickName,
            Email = form.Mail,
            Password = form.Password,
            Wallet = form.Wallet,
            EditorName = form.Editor,
            Role = form.Role,
            Status = form.Status
        };
    }

    public static User ToUser(this UpdateUserForm form)
    {
        return new User()
        {
            UserId = form.UserId,
            NickName = form.NickName,
            Email = form.Mail,
            Password = form.Password,
            Wallet = form.Wallet,
            EditorName = form.Editor,
            Role = form.Role,
            Status = form.Status
        };
    }
}