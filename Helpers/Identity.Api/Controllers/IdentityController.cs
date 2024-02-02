using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class IdentityController : ControllerBase
{
    private const string TokenSecret = "TO SECURE";
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
    }
}
