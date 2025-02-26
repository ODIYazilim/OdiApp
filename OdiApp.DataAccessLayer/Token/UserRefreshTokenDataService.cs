using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.Token;

namespace OdiApp.DataAccessLayer.Token
{
    public interface IUserRefreshTokenDataService
    {
        Task<UserRefreshToken> UserRefreshTokenGetir(string userId);
        Task<UserRefreshToken> UserRefreshTokenEkle(UserRefreshToken refreshToken);
        Task<UserRefreshToken> UserRefreshTokenGuncelle(UserRefreshToken refreshToken);
        Task UserRefreshTokenSil(string userId);
    }
    public class UserRefreshTokenDataService : IUserRefreshTokenDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRefreshTokenDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRefreshToken> UserRefreshTokenGetir(string userId)
        {
            return await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<UserRefreshToken> UserRefreshTokenEkle(UserRefreshToken refreshToken)
        {
            await _dbContext.UserRefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<UserRefreshToken> UserRefreshTokenGuncelle(UserRefreshToken refreshToken)
        {
            _dbContext.UserRefreshTokens.Update(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        public async Task UserRefreshTokenSil(string userId)
        {
            var refToken = _dbContext.UserRefreshTokens.FirstOrDefault(u => u.UserId == userId);
            if (refToken != null)
            {
                _dbContext.UserRefreshTokens.Remove(refToken);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
