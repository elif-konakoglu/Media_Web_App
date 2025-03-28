using Alpata.Entity;
using Alpata.Model;

namespace Alpata.API.Business.Interfaces;

public interface IMailHelper
{
    public bool SendMeetingMail(Meeting meeting);
    public bool SendMailAfterRegister(SaveUserRequestDto saveUserRequestDto);
}