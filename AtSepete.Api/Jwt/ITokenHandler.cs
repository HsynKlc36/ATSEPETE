namespace AtSepete.Api.Jwt
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
        string CreateRefreshToken();
    }
}
