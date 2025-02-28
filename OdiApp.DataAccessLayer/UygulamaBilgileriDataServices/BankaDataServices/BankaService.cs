using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdiApp.DTOs.UygulamaBilgileriDTOs.BankaDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.BankaDataServices
{
    public class BankaService : IBankaService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        // private readonly IAmazonS3Service _amazonS3Service;

        public BankaService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Banka>> BankaEkle(Banka model)
        {
            await _dbContext.Bankalar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return await BankaListesi();
        }

        public async Task<List<Banka>> BankaGuncelle(Banka model)
        {
            _dbContext.Bankalar.Update(model);
            await _dbContext.SaveChangesAsync();

            return await BankaListesi();
        }

        public async Task<List<Banka>> BankaListesi(bool onlyAktif = false)
        {
            IQueryable<Banka> queryable = _dbContext.Bankalar.OrderBy(s => s.Sira);

            if (onlyAktif)
            {
                queryable = queryable.Where(s => s.Aktif);
            }

            var list = await queryable.ToListAsync();

            return list;
        }

        public async Task<List<BankaOutputDTO>> BankaOutputListesi()
        {
            List<BankaOutputDTO> list = _mapper.Map<List<BankaOutputDTO>>(await BankaListesi(true));

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.Logo))
                {
                    // item.Logo = _amazonS3Service.GetPreSignedUrl(item.Logo);
                }
                else
                {
                    item.Logo = string.Empty;
                }
            }

            return list;
        }
    }
}