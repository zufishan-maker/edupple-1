using EDUPPLE.INFRASTRUCTURE.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EDUPPLE.APPLICATION.Authentication.Extensions
{
    public static  class TokenExtensions
    {
        public static string CreateToken(
            IOptions<PrincipalConfiguration> principalOptions,
            IOptions<HostingConfiguration> hostingOptions,
            DOMAIN.Entities.User user,           
            IList<string> roles)
        {
            var key = Base64UrlTextEncoder.Decode(principalOptions.Value.AudienceSecret);
            var sharedKey = new SymmetricSecurityKey(key);
            var issued = DateTime.UtcNow;
            var expires = issued.Add(principalOptions.Value.TokenExpire);

            var signinCredentials = new SigningCredentials(sharedKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[] { new Claim(TokenConstants.Claims.UserId, user.Id.ToString()),
            new Claim(TokenConstants.Claims.Username, user.UserName) };

            var token = new JwtSecurityToken(
                   hostingOptions.Value.ClientDomain,
                   principalOptions.Value.AudienceId,
                   claims,
                   issued,
                   expires,
                   signinCredentials);
            token.Payload[TokenConstants.Claims.Roles] = roles;            
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            return jwt;

        }
        public static string HashToken(string token)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(token);
            byte[] hashBytes;

            using (var sha = new SHA512Managed())
                hashBytes = sha.ComputeHash(bytes);

            var hash = Convert.ToBase64String(hashBytes);

            return hash;
        }
    }
}
