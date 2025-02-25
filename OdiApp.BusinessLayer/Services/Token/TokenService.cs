
using Microsoft.IdentityModel.Tokens;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.DataAccessLayer.Token;
using OdiApp.DTOs.TokenDTOs;
using OdiApp.Entity.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace OdiApp.BusinessLayer.Services.Token
{
    public interface ITokenService
    {
        Task<UserTokenDTO> CreateTokenByUser(ApplicationUser user);
        ClientTokenDTO CreateTokenByClient(Client client);
    }
    public class TokenService : ITokenService
    {
        //private readonly CustomTokenOption customTokenOption;
        private readonly CustomTokenOption customTokenOption = TokenOptionService.GetTokenOption();
        private readonly IUserRefreshTokenDataService _userRefreshTokenDataService;
        public TokenService(IUserRefreshTokenDataService userRefreshTokenDataService)
        {
            _userRefreshTokenDataService = userRefreshTokenDataService;
        }

        #region privates
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var rnd = System.Security.Cryptography.RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
        private IEnumerable<Claim> GetClaimByUser(ApplicationUser user, List<string> audiences)
        {
            //Kullanmak için User.Claims.First(x=>x.Type=="AdSoyad")
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim("AdSoyad", user.AdSoyad),
                new Claim("KayitGrubuKodu", user.KayitGrubuKodu),
                new Claim("KayitTuruKodu",user.KayitTuruKodu),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }
        private IEnumerable<Claim> GetClaimByClient(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()),
            };
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }
        #endregion

        public async Task<UserTokenDTO> CreateTokenByUser(ApplicationUser user)
        {

            var accesTokenExpiration = DateTime.Now.AddMinutes(customTokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(customTokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(customTokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: customTokenOption.Issuer,
                claims: GetClaimByUser(user, customTokenOption.Audience),
                expires: accesTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDTO = new UserTokenDTO
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accesTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDTO;
        }
        public ClientTokenDTO CreateTokenByClient(Client client)
        {
            var accesTokenExpiration = DateTime.Now.AddMinutes(customTokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(customTokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: customTokenOption.Issuer,
                claims: GetClaimByClient(client),
                expires: accesTokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDTO = new ClientTokenDTO
            {
                AccessToken = token,
                AccessTokenExpiration = accesTokenExpiration,
            };
            return tokenDTO;
        }
    }
}
