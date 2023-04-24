using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.JWT
{
    public class Token
    {
        public string AccessToken { get; set; }//token
        public DateTime Expirition { get; set; }//token süresi
        public string? RefreshToken { get; set; }//refresh token
    }
}
