using Alpata.Entity;
using Alpata.Model;

namespace Alpata.API.Business.Interfaces;

public interface IUserService
{
    public User Save(SaveUserRequestDto saveUserRequestDto);
    public User Login(LoginRequestDto loginRequestDto);
}