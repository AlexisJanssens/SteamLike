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
        throw new NotImplementedException();
    }

    public UserDTO Get(int id)
    {
        throw new NotImplementedException();
    }

    public UserDTO Create(UserForm form)
    {
       return _userRepository.Create(form.ToUser()).ToUserDTO();
    }

    public bool Update(User user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(User user)
    {
        throw new NotImplementedException();
    }
}