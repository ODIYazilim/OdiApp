using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SSSDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SSSDataServices
{
    public class SSSService : ISSSService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SSSService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SSS>> SSSEkle(SSS model)
        {
            await _dbContext.SSSLer.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return await SSSListesi(model.DilId);
        }

        public async Task<List<SSS>> SSSGuncelle(SSS model)
        {
            _dbContext.SSSLer.Update(model);
            await _dbContext.SaveChangesAsync();

            return await SSSListesi(model.DilId);
        }

        public async Task<List<SSS>> SSSListesi(int dilId = -1, string kayitGrubu = "", bool onlyAktif = false)
        {
            IQueryable<SSS> query = _dbContext.SSSLer;

            if (dilId >= 0)
            {
                query = query.Where(x => x.DilId == dilId);
            }

            if (!string.IsNullOrEmpty(kayitGrubu))
            {
                query = query.Where(x => x.KayitGrubu == kayitGrubu);
            }

            if (onlyAktif)
            {
                query = query.Where(x => x.Aktif);
            }

            List<SSS> list = await query.OrderBy(x => x.Sira).ToListAsync();

            return list;
        }

        public async Task<List<SSSOutputDTO>> SSSOutputListesi(SSSListeInputDTO model, int dilId = -1)
        {
            return _mapper.Map<List<SSSOutputDTO>>(await SSSListesi(dilId, model.KayitGrubuKodu, true));
        }
    }
}
