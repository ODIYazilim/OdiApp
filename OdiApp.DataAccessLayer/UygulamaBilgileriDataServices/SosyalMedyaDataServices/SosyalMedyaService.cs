using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SosyalMedyaDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SosyalMedyaDataServices
{
    public class SosyalMedyaService : ISosyalMedyaService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        // private readonly IAmazonS3Service _amazonS3Service;

        public SosyalMedyaService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SosyalMedya>> SosyalMedyaEkle(SosyalMedya model)
        {
            await _dbContext.SosyalMedyalar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return await SosyalMedyaListesi();
        }

        public async Task<List<SosyalMedya>> SosyalMedyaGuncelle(SosyalMedya model)
        {
            _dbContext.SosyalMedyalar.Update(model);
            await _dbContext.SaveChangesAsync();

            return await SosyalMedyaListesi();
        }

        public async Task<List<SosyalMedya>> SosyalMedyaListesi(bool onlyAktif = false)
        {
            IQueryable<SosyalMedya> queryable = _dbContext.SosyalMedyalar.OrderBy(s => s.Sira);

            if (onlyAktif)
            {
                queryable = queryable.Where(s => s.Aktif);
            }

            var list = await queryable.ToListAsync();

            return list;
        }

        public async Task<List<SosyalMedyaOutputDTO>> SosyalMedyaOutputListesi()
        {
            List<SosyalMedyaOutputDTO> list = _mapper.Map<List<SosyalMedyaOutputDTO>>(await SosyalMedyaListesi(true));

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.Icon))
                {
                    //  item.Icon = _amazonS3Service.GetPreSignedUrl(item.Icon);
                }
                else
                {
                    item.Icon = string.Empty;
                }
            }

            return list;
        }
    }
}