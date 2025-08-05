using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaSolutionApi.Entities;

namespace CinemaSolutionApi.Helpers;

public class JWT
{
	private readonly IConfiguration _configuration;
	public JWT(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string CreateToken(User user)
	{
		var userClaims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Username),
			new Claim(ClaimTypes.Email, user.Email)
		};
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

		var jwtConfig = new JwtSecurityToken(
			claims: userClaims,
			expires: DateTime.UtcNow.AddMinutes(120),
			signingCredentials: credentials
			);

		return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
	}

}