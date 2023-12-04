using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IAuthService
{
    User? Login(LoginForm form);
}