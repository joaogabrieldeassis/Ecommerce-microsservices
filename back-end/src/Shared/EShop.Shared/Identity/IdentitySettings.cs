using System.Text;

namespace EShop.Shared.Identity;

public class IdentitySettings(string jwtSecrect)
{
    public byte[] JwtSecret { get; set; } = Encoding.ASCII.GetBytes(jwtSecrect);
}