namespace EShop.Identity.Dtos;

public class LoginResponseDto
{

    public string AccessToken { get; set; } = string.Empty;

    public double ExpiresIn { get; set; }

    public UserTokenDto UserTokenDto { get; set; } = new();
}
