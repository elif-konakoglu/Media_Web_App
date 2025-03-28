using System.Net;
using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Alpata.Model;

namespace Alpata.API.Business;

public class UserManager : IUserService
{
	private readonly AlpataAPIDbContext _dbContext;
	private readonly ICryptographyHelper _cryptographyHelper;
	private readonly IFileHelper _fileHelper;

    public UserManager(AlpataAPIDbContext dbContext, ICryptographyHelper cryptographyHelper, IFileHelper fileHelper)
	{
		_dbContext = dbContext;
		_cryptographyHelper = cryptographyHelper;
		_fileHelper = fileHelper;
	}

    public User Save(SaveUserRequestDto saveUserRequestDto)
    {
		var entity = _dbContext
			.Users
			.FirstOrDefault(x => x.Email == saveUserRequestDto.Email);

		if (entity != null)
		{
            throw new Exception("This email is already registered.");
        }

        entity = new User
		{
			Name = saveUserRequestDto.Name,
			Surname = saveUserRequestDto.Surname,
			Email = saveUserRequestDto.Email,
			Phone = saveUserRequestDto.Phone,
			Password = _cryptographyHelper.HashPassword(saveUserRequestDto.Password),
			Photo = _fileHelper.UploadFile(saveUserRequestDto.Photo)
    };

		_dbContext.Add(entity);
		_dbContext.SaveChanges();

		return entity;
    }

	public User Login(LoginRequestDto loginRequestDto)
	{
		var entity = _dbContext
			.Users
			.FirstOrDefault(x => x.Email == loginRequestDto.Email &&
								 x.Password == _cryptographyHelper.HashPassword(loginRequestDto.Password));

		if(entity == null)
		{
			throw new ArgumentNullException("Credentials are not correct.");
		}

		return entity;
	}
}