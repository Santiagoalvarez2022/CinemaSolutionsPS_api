using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using CinemaSolutionApi.Entities;
using System.Security.Claims;
using System.Text;

namespace CinemaSolutionApi.Helpers;

public class JWT
{
	private readonly IConfiguration _configuration;
	public JWT(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string CreateToken(User user, IList<string> roles)
	{
		var userClaims = new List<Claim>
		{
			new Claim("UserId", user.Id),
			new Claim("username", user.UserName),
		};

		userClaims.AddRange(roles.Select(role => new Claim("role", role)));

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