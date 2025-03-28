using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Alpata.Model;
using Microsoft.AspNetCore.Mvc;

namespace Alpata.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
	private readonly IUserService _userService;
    private readonly IMailHelper _mailHelper;

	public UserController(IUserService userService, IMailHelper mailHelper)
	{
		_userService = userService;
        _mailHelper = mailHelper;
	}

    [HttpPost("save")]
    [ProducesDefaultResponseType(typeof(bool))]
    public User Save([FromForm] SaveUserRequestDto saveUserRequestDto)
    {
        var user = _userService.Save(saveUserRequestDto);

        _mailHelper.SendMailAfterRegister(saveUserRequestDto);

        return user;
    }
    
    [HttpPost("login")]
    [ProducesDefaultResponseType(typeof(User))]
    public User Login([FromBody] LoginRequestDto loginRequestDto)
    {
        return _userService.Login(loginRequestDto);
    }
}


