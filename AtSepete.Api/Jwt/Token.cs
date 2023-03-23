namespace AtSepete.Api.Jwt
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expirition { get; set; }
        public string RefreshToken { get; set; }
    }
}
