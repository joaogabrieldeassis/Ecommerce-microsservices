using EShop.Identity.Dtos;
using EShop.Shared.Api.Controllers;
using EShop.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EShop.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(INotifier notifier,
                                      UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                      ILogger<AuthenticationController> logger,
                                      IConfiguration configuration) : MainController(notifier)
{
    private readonly ILogger _logger = logger;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserDto userDto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = userDto.Email,
            Email = userDto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return CustomResponse(await GenerateJwt(userDto.Email));
        }

        foreach (var error in result.Errors)
        {
            NotifyError(error.Description);
        }

        return CustomResponse(userDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDto loginUser)
    {
        if (!ModelState.IsValid)
            return CustomResponse();

        var result = await _signInManager.PasswordSignInAsync(
            loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
        {
            return CustomResponse(await GenerateJwt(loginUser.Email));
        }
        else if (result.IsLockedOut)
        {
            NotifyError("User locked out due to multiple failed attempts.");
            return CustomResponse(loginUser);
        }

        NotifyError("Invalid username or password.");
        return CustomResponse(loginUser);
    }

    [NonAction]
    private async Task<LoginResponseDto> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await CreateClaims(user!);

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "identity",
            Audience = "http://localhost",
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encodedToken = tokenHandler.WriteToken(token);

        return  BuildLoginResponse(user!, encodedToken, claims);
    }

    [NonAction]
    private LoginResponseDto BuildLoginResponse(
        IdentityUser user, string encodedToken, IList<Claim> claims)
    {
        var response = new LoginResponseDto
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(2).TotalSeconds,
            UserTokenDto = new UserTokenDto
            {
                Id = user.Id,
                Email = user.Email!,
                ClaimDtos = claims.Select(c => new ClaimDto
                {
                    Type = c.Type,
                    Value = c.Value
                })
            }
        };

        return response;
    }

    [NonAction]
    private async Task<IList<Claim>> CreateClaims(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var role in userRoles)
        {
            claims.Add(new Claim("role", role));
        }

        return claims;
    }

    private static long ToUnixEpochDate(DateTime dateTime)
        => (long)Math.Round((dateTime.ToUniversalTime() -
            new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}