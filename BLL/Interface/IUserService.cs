using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    UserDTO Get(int id);
    UserDTO Create(UserForm form);
    bool Update(User user);
    bool Delete(User user);
}