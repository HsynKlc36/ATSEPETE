using AtSepete.Dtos.Dto.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.JWT
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token ResetPasswordToken(int hour,ForgetPasswordEmailDto emailDto)
        {
            var claims = new List<Claim>()
                    {   
                        new Claim(ClaimTypes.Email, emailDto.Email),
                        new Claim(ClaimTypes.Role, "ForgetPassword")
                    };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));// security key'in simetriğini alıyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);//şifrelenmiş kimlik oluşturduk 
            token.Expirition = DateTime.UtcNow.AddHours(hour);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expirition,
                notBefore: DateTime.UtcNow,//ne zamandan itibaren geçerli olmalı
                signingCredentials: signingCredentials,
                claims:principal.Claims
                );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
        public Token CreateAccessToken(int hour,ClaimsPrincipal claimsPrincipal)
        {

            Token token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));// security key'in simetriğini alıyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);//şifrelenmiş kimlik oluşturduk 

            //oluşturulacak token ayarlarını vereceğiz!
            token.Expirition = DateTime.UtcNow.AddHours(hour);
            //ürettiğimiz token içeriği
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expirition,
                notBefore: DateTime.UtcNow,//ne zamandan itibaren geçerli olmalı
                signingCredentials: signingCredentials,
                claims:claimsPrincipal.Claims // dışarıdan aldığımız claimsleri burada atadık token'de.
                );

            //token oluşturucu sınıfından örnek alalım
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
