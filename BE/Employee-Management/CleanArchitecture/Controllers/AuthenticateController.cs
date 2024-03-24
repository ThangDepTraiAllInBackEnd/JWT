using CleanArchitecture.Core.Auth;
using CleanArchitecture.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;

		public AuthenticateController(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}


		/// <summary>	
		/// revevice username and password and check is login info is valid or not
		/// </summary>
		/// <param name="model">login model contain username and password</param>
		/// <returns>access token and refresh token</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = CreateToken(authClaims);
				var refreshToken = GenerateRefreshToken();

				_ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

				user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

				return StatusCode(200, new ServiceResult
				{
					Success = true,
					Code = System.Net.HttpStatusCode.OK,
					Data = new
					{
						Token = new JwtSecurityTokenHandler().WriteToken(token),
						RefreshToken = refreshToken,
						Expiration = token.ValidTo,
						ExpirationRefreshToken = user.RefreshTokenExpiryTime
					},
					DevMsg = Core.Resources.MsgResource_VN.AuthenticateSuccess,
					UserMsg = Core.Resources.MsgResource_VN.AuthenticateSuccess,
				});
			}
			return StatusCode(401, new
				ServiceResult
			{
				Success = false,
				Code = System.Net.HttpStatusCode.Unauthorized,
				DevMsg = Core.Resources.MsgResource_VN.CannotAuthorize,
				UserMsg = Core.Resources.MsgResource_VN.CannotAuthorize,
			});
		}


		/// <summary>	
		/// revevice username and password and email to create new account
		/// </summary>
		/// <param name="model">register model contain username, password and email</param>
		/// <returns>register success or not</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			var userExists = await _userManager.FindByNameAsync(model.Username);
			// user exist
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, new ServiceResult
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					DevMsg = Core.Resources.MsgResource_VN.UserAlreadySuccess,
					UserMsg = Core.Resources.MsgResource_VN.UserAlreadySuccess,
				}); ;

			ApplicationUser user = new()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.Username
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new
			ServiceResult
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					DevMsg = Core.Resources.MsgResource_VN.UserCreateFailed,
					UserMsg = Core.Resources.MsgResource_VN.UserCreateFailed
				});

			return StatusCode(201, new
			ServiceResult
			{
				Success = true,
				Code = System.Net.HttpStatusCode.Created,
				DevMsg = Core.Resources.MsgResource_VN.UserCreateSuccess,
				UserMsg = Core.Resources.MsgResource_VN.UserCreateSuccess
			});
		}


		/// <summary>	
		/// revevice username and password and email to create new account for admin
		/// </summary>
		/// <param name="model">register model contain username, password and email</param>
		/// <returns>register success or not</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[HttpPost]
		[Route("RegisterAdmin")]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
		{
			var userExists = await _userManager.FindByNameAsync(model.Username);
			// user already exist
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, new ServiceResult
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					DevMsg = Core.Resources.MsgResource_VN.UserAlreadySuccess,
					UserMsg = Core.Resources.MsgResource_VN.UserAlreadySuccess,
				}); ;

			ApplicationUser user = new()
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.Username
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			// server error
			if (!result.Succeeded)
				return StatusCode(StatusCodes.Status500InternalServerError, new
				ServiceResult
				{
					Success = false,
					Code = System.Net.HttpStatusCode.BadRequest,
					DevMsg = Core.Resources.MsgResource_VN.UserCreateFailed,
					UserMsg = Core.Resources.MsgResource_VN.UserCreateFailed
				});

			if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
			if (!await _roleManager.RoleExistsAsync(UserRoles.User))
				await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.Admin);
			}
			if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
			{
				await _userManager.AddToRoleAsync(user, UserRoles.User);
			}
			return StatusCode(201, new
				ServiceResult
			{
				Success = true,
				Code = System.Net.HttpStatusCode.Created,
				DevMsg = Core.Resources.MsgResource_VN.UserCreateSuccess,
				UserMsg = Core.Resources.MsgResource_VN.UserCreateSuccess
			});
		}

		/// <summary>	
		/// create new access token and refresh token bass current tokens
		/// </summary>
		/// <param name="tokenModel">token model contain access token and refresh token</param>
		/// <returns>register success or not</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[HttpPost]
		[Route("RefereshToken")]
		public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
		{
			if (tokenModel is null)
			{
				return BadRequest(Core.Resources.MsgResource_VN.TokenModelIsNull);
			}

			string? accessToken = tokenModel.AccessToken;
			string? refreshToken = tokenModel.RefreshToken;

			var principal = GetPrincipalFromExpiredToken(accessToken);
			if (principal == null)
			{
				return BadRequest(Core.Resources.MsgResource_VN.InvalidToken);
			}
			string username = principal.Identity.Name;

			var user = await _userManager.FindByNameAsync(username);
			// invalid user || invalid refresh token
			if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
			{
				return BadRequest(Core.Resources.MsgResource_VN.InValidRefreshToken);
			}

			var newAccessToken = CreateToken(principal.Claims.ToList());
			var newRefreshToken = GenerateRefreshToken();

			user.RefreshToken = newRefreshToken;
			await _userManager.UpdateAsync(user);

			return StatusCode(200, new ServiceResult
			{
				Code = System.Net.HttpStatusCode.OK,
				Success = true,
				Data
				 = new
				 {
					 accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
					 refreshToken = newRefreshToken
				 },
				DevMsg = Core.Resources.MsgResource_VN.TokenCreationSuccess,
				UserMsg = Core.Resources.MsgResource_VN.TokenCreationSuccess
			});
		}

		/// <summary>	
		/// revoke refresh token by username
		/// </summary>
		/// <param name="username">username whose token was revoked</param>
		/// <returns>register success or not</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[Authorize]
		[HttpPost]
		[Route("Revoke/{username}")]
		public async Task<IActionResult> Revoke(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			// invalid user
			if (user == null) return BadRequest(Core.Resources.MsgResource_VN.InvalidUserName);
			user.RefreshToken = null;
			await _userManager.UpdateAsync(user);
			return NoContent();
		}

		/// <summary>	
		/// revoke refresh token from all user
		/// </summary>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		[Authorize]
		[HttpPost]
		[Route("RevokeAll")]
		public async Task<IActionResult> RevokeAll()
		{
			var users = _userManager.Users.ToList();
			foreach (var user in users)
			{
				user.RefreshToken = null;
				await _userManager.UpdateAsync(user);
			}
			return NoContent();
		}


        /// <summary>	
        /// create new access token 
        /// </summary>
        /// <param name="authClaims">token's claims</param>
        /// <returns>new access token</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/26/2
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


        /// <summary>	
        /// create new refresh token 
        /// </summary>
        /// <returns>new refresh token</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/26/2
        private static string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}


		/// <summary>	
		/// Validate a token to extract user information
		/// </summary>
		/// <param name="token">token to extract</param>
		/// <returns>User information as a principal</returns>
		///  created by: Nguyễn Thiện Thắng
		///  created at: 2024/26/2
		private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException(Core.Resources.MsgResource_VN.InvalidToken);
			return principal;

		}
	}
}
