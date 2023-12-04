using BLL.Interface;
using BLL.Models;
using DAL.Entities;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }
    
    public User? Login(LoginForm form)
    {
        User? user = _userService.GetByMail(form.Email);

        if (user == null)
        {
            return null;
        }

        if (BCrypt.Net.BCrypt.Verify(form.Password, user.Password))
        {
            return user;
        }

        return null;


    }
}