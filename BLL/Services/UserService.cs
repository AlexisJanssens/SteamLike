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

    public User? GetByMail(string mail)
    {
        User? user = _userRepository.GetByMail(mail);
        return user;
    }

    public bool UpdateWallet(double amount, int userId)
    {
        double actualWallet = _userRepository.Get(userId)!.Wallet;
        double newWallet = actualWallet + amount; 
        
        return _userRepository.UpdateWallet(newWallet, userId);
    }

    public UserDTO? Create(UserForm form)
    {
        if (MailAlreadyExist(form.Mail))
        {
            return null;
        }

        form.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);

        UserDTO userDto = _userRepository.Create(form.ToUser()).ToUserDTO();

        return userDto;
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