using BLL.Interface;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public IEnumerable<UserDTO> GetAll()
    {
        return _userRepository.GetAll().Select(x => x.ToUserDTO());
    }

    public UserDTO? Get(int id)
    {
        User? user = _userRepository.Get(id);
        return user?.ToUserDTO();
    }

    public bool MailAlreadyExist(string mail)
    {
        User? userToCheck = _userRepository.GetByMail(mail);
        return userToCheck != null;
    }

    public UserDTO Create(UserForm form)
    {
       return _userRepository.Create(form.ToUser()).ToUserDTO();
    }

    public bool Update(UpdateUserForm form)
    {
        return _userRepository.Update(form.ToUser());
    }

    public bool Delete(int id)
    {
        return _userRepository.Delete(id);
    }
}