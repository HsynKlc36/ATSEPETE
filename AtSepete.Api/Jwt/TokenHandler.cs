using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace AtSepete.Api.Jwt
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token CreateAccessToken(int minute)
        {
            Token token = new ();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));// security key'in simetriğini alıyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);//şifrelenmiş kimlik oluşturduk 

            //oluşturulacak token ayarlarını vereceğiz!
            token.Expirition = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expirition,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                );
               
            //token oluşturucu sınıfından örnek alalım
            JwtSecurityTokenHandler tokenHandler = new();
                token.AccessToken=tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number=new byte[32];
            using  RandomNumberGenerator random=RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
