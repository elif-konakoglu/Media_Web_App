using Alpata.API.Business.Interfaces;
using Alpata.Entity;
using Alpata.Model;
using System.Net;
using System.Net.Mail;

namespace Alpata.API.Business;

public class MailHelper : IMailHelper
{
    private readonly string senderEmail;
    private readonly string senderPassword;
    private readonly AlpataAPIDbContext _dbContext;

    public MailHelper(AlpataAPIDbContext dbContext)
    {
        senderEmail = "kutay.avci.4870@gmail.com";
        senderPassword = "ancvcnipxylwydet";
        _dbContext = dbContext;
    }

    public bool SendMeetingMail(Meeting meeting)
	{
        User user = _dbContext.Users.Find(meeting.UserId);

        if(user == null)
        {
            throw new ArgumentNullException("User has not found.");
        }

        string recipientEmail = user.Email;

        MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
        mailMessage.Subject = "Meeting";
        mailMessage.Body = String.Format("Meeting Name: {0}\n" +
            "Start Date: {1}\n" +
            "End Date: {2}\n" +
            "Description: {3}",meeting.Name,meeting.StartDate,meeting.EndDate,meeting.Description);

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(senderEmail , senderPassword);
        smtpClient.EnableSsl = true;

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }

        return true;
	}

    public bool SendMailAfterRegister(SaveUserRequestDto saveUserRequestDto)
    {
        string recipientEmail = saveUserRequestDto.Email;

        MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
        mailMessage.Subject = "Welcome";
        mailMessage.Body = "Welcome to the Octapull.";

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
        smtpClient.EnableSsl = true;

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }

        return true;
    }
}


