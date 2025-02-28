using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OdiApp.DTOs.UygulamaBilgileriDTOs.UlkeDTOs;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.UlkeServices
{
    public class UlkeService : IUlkeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UlkeService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<List<UlkeDTO>> UlkeListesi()
        {
            var query = "SELECT * FROM UlkelerView";
            var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            var result = await connection.QueryAsync<UlkeDTO>(query);

            return result.ToList();
        }
    }
}