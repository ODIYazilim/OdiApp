using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.DataAccessLayer.Token;
using OdiApp.DTOs.GlobalDTOs;
using OdiApp.DTOs.IdentityDTOs;
using OdiApp.DTOs.TokenDTOs;
using OdiApp.Entity.Identity;
using OdiApp.Entity.Token;

namespace OdiApp.BusinessLayer.Services.Token
{
    public interface IAuthenticationService
    {
        Task<OdiResponse<UserTokenDTO>> CreateUserTokenAsync(LoginDTO loginDTO);
        OdiResponse<ClientTokenDTO> CreateClientTokenAsync(ClientLoginDTO clientLoginDTO);
        Task<OdiResponse<UserTokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<OdiResponse<NoData>> RevokeRefreshTokenAsync(string refreshToken);

    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients = TokenOptionService.GetClients();
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRefreshTokenDataService _userRefreshTokenDataService;

        public AuthenticationService(ITokenService tokenService, UserManager<ApplicationUser> userManager, IUserRefreshTokenDataService userRefreshTokenDataService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _userRefreshTokenDataService = userRefreshTokenDataService;
        }

        public async Task<OdiResponse<UserTokenDTO>> CreateUserTokenAsync(LoginDTO loginDTO)
        {
            if (loginDTO == null) throw new ArgumentNullException(nameof(loginDTO));

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == loginDTO.PhoneNumber);
            if (user == null) return OdiResponse<UserTokenDTO>.Fail("Telefon numarası yada şifre yanlış", "Not Found", 400);
            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password)) return OdiResponse<UserTokenDTO>.Fail("Telefon numarası yada şifre yanlış", "Not Found", 400);
            var token = await _tokenService.CreateTokenByUser(user);

            var userRefreshToken = await _userRefreshTokenDataService.UserRefreshTokenGetir(user.Id);
            if (userRefreshToken == null)
            {
                await _userRefreshTokenDataService.UserRefreshTokenEkle(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expration = token.RefreshTokenExpiration;
                await _userRefreshTokenDataService.UserRefreshTokenGuncelle(userRefreshToken);
            }

            return OdiResponse<UserTokenDTO>.Success("User Token oluşturuldu", token, 200);
        }

        //burdayım
        public OdiResponse<ClientTokenDTO> CreateClientTokenAsync(ClientLoginDTO clientLoginDTO)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDTO.Id && x.Secret == clientLoginDTO.Secret);
            if (client == null)
            {
                return OdiResponse<ClientTokenDTO>.Fail("Client bilgileri hatalı", "Bad Request", 400);
            }
            var token = _tokenService.CreateTokenByClient(client);
            return OdiResponse<ClientTokenDTO>.Success("Client Token oluşturuldu", token, 200);
        }


        public async Task<OdiResponse<UserTokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenDataService.UserRefreshTokenGetir(refreshToken);
            if (existRefreshToken == null) return OdiResponse<UserTokenDTO>.Fail("Refresh Token bulunamadı", "Not Found", 404);
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return OdiResponse<UserTokenDTO>.Fail("Kullanıcı bulunamadı", "Not Found", 404);
            }
            var tokenDTO = await _tokenService.CreateTokenByUser(user);

            existRefreshToken.Code = tokenDTO.RefreshToken;
            existRefreshToken.Expration = tokenDTO.RefreshTokenExpiration;
            await _userRefreshTokenDataService.UserRefreshTokenGuncelle(existRefreshToken);
            return OdiResponse<UserTokenDTO>.Success("Yeni Token oluşturuldu", tokenDTO, 200);
        }

        public async Task<OdiResponse<NoData>> RevokeRefreshTokenAsync(string refreshToken)
        {
            UserRefreshToken refToken = await _userRefreshTokenDataService.UserRefreshTokenGetir(refreshToken);
            if (refToken == null) return OdiResponse<NoData>.Fail("Refresh Token bulunamadı", "Not Found", 404);
            await _userRefreshTokenDataService.UserRefreshTokenSil(refToken.UserId);
            return OdiResponse<NoData>.Success("Refresh Token silindi", 200);
        }


    }
}
