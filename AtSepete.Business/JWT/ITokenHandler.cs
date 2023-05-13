using AtSepete.Dtos.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.JWT
{
    public interface ITokenHandler
    {
        public Token ResetPasswordToken(int hour,ForgetPasswordEmailDto emailDto);
        Token CreateAccessToken(int hour, ClaimsPrincipal claimsPrincipal);
        string CreateRefreshToken();
    }
}
