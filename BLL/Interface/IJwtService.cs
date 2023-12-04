using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IJwtService
{
    string GenerateToken(User user);
}