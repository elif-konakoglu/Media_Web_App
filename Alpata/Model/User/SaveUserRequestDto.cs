namespace Alpata.Model;

public class SaveUserRequestDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public IFormFile Photo { get; set; } //Validasyon yapılacak
}

