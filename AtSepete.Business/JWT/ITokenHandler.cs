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
        Token CreateAccessToken(int minute, ClaimsPrincipal claimsPrincipal);
        string CreateRefreshToken();
    }
}
