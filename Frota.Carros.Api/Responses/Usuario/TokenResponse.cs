namespace Frota.Carros.Api.Responses.Usuario
{
    public class TokenResponse
    {
        public string Token { get; private set; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
