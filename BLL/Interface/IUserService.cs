using BLL.Models;
using DAL.Entities;

namespace BLL.Interface;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    UserDTO? Get(int id);
    UserDTO Create(UserForm form);
    bool Update(UpdateUserForm form);
    bool Delete(int id);
    bool MailAlreadyExist(string mail);
}