using OdiApp.DTOs.TokenDTOs;

namespace OdiApp.BusinessLayer.Core.Services
{
    public static class TokenOptionService
    {
        public static CustomTokenOption GetTokenOption()
        {
            return new CustomTokenOption
            {
                Audience = new List<string> { "http://localhost:5000" },
                Issuer = "http://localhost:5000",
                AccessTokenExpiration = 30,
                RefreshTokenExpiration = 60,
                SecurityKey = "46ec3608-290d-45a2-ab35-53d4770391ec"
            };
        }
        public static List<Client> GetClients()
        {
            return new List<Client>
                {
                    new Client
                    {
                        Id = "FlutterApp",
                        Secret = "46ec3608-290d-45a2-ab35-53d4770391ec",
                        Audiences = new List<string> { "http://localhost:5000" }
                    }
                };
        }
    }
}
