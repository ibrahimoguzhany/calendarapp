using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.ENTITIES.DTOs;
using CalendarApp.ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.BLL.ManagerServices.Concretes
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;
        private TokenOptions _tokenOptions = new TokenOptions();
        protected IHttpContextAccessor _accessor;
        public TokenService(IConfiguration config, IHttpContextAccessor accessor)
        {
            _config = config;
            _tokenOptions.Issuer = _config.GetSection("TokenOptions").GetSection("Issuer").Value;
            _tokenOptions.AccessTokenExpiration = Convert.ToInt32(_config.GetSection("TokenOptions").GetSection("AccessTokenExpiration").Value);
            _tokenOptions.Audience = _config.GetSection("TokenOptions").GetSection("Audience").Value;
            _tokenOptions.SecurityKey = _config.GetSection("TokenOptions").GetSection("SecurityKey").Value;
            _accessor = accessor;
        }
        public string GenerateJWT(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.FirstName + " " + userInfo.LastName),
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,userInfo.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
        public string GetToken()
        {
            var token = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                return token.Substring(7); // "Bearer " önekini kaldır
            }

            return null;
        }
        public UserClaim GetClaims()
        {
            var result = new UserClaim();
            result.IsSuccess = false;

            var token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidAudience = _tokenOptions.Audience,
                    IssuerSigningKey = securityKey
                };

                try
                {
                    var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                    var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
                    if (claimsIdentity != null && claimsIdentity.Claims != null && claimsIdentity.Claims.Count() > 0)
                    {
                        result.IsSuccess = true;
                        result.Claim = new UserClaimType();

                        IList<Claim> claims = claimsIdentity.Claims.ToList();

                        result.Claim.Id = new Guid(claims[0].Value);
                        result.Claim.FullName = claims[1].Value;
                        result.Claim.Email = claims[2].Value;
                        result.Claim.UserName = claims[3].Value;

                    }
                }
                catch (Exception ex)
                {
                    //Token doğrulama hatası
                    //
                }
            }
            return result;
        }
    }
}

