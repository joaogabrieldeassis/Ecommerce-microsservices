namespace EShop.Identity.Dtos;

public class UserTokenDto
{

    public string Id { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;


    public IEnumerable<ClaimDto> ClaimDtos { get; set; } = [];
}
