using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OdiApp.DTOs.UygulamaBilgileriDTOs.SabitMetinDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;
using System.Net;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SabitMetinDataServices
{
    public class SabitMetinService : ISabitMetinService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SabitMetinService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SabitMetin>> SabitMetinEkle(SabitMetin model)
        {
            model.Metin = WebUtility.HtmlEncode(model.Metin);

            await _dbContext.SabitMetinler.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return await SabitMetinListesi();
        }

        public async Task<List<SabitMetin>> SabitMetinGuncelle(SabitMetin model)
        {
            model.Metin = WebUtility.HtmlEncode(model.Metin);

            _dbContext.SabitMetinler.Update(model);
            await _dbContext.SaveChangesAsync();

            return await SabitMetinListesi();
        }

        public async Task<List<SabitMetin>> SabitMetinListesi(int dilId = -1, string kayitGrubu = "", int metinTipi = -1)
        {
            IQueryable<SabitMetin> query = _dbContext.SabitMetinler;

            if (dilId >= 0)
            {
                query = query.Where(x => x.DilId == dilId);
            }

            if (!string.IsNullOrEmpty(kayitGrubu))
            {
                query = query.Where(x => x.KayitGrubu == kayitGrubu);
            }

            if (metinTipi >= 0)
            {
                query = query.Where(x => x.MetinTipi == metinTipi);
            }

            List<SabitMetin> list = await query.ToListAsync();

            foreach (var item in list)
            {
                item.Metin = WebUtility.HtmlDecode(item.Metin);
            }

            return list;
        }

        public async Task<List<SabitMetinOutputDTO>> SabitMetinOutputListesi(SabitMetinListeInputDTO model, int dilId = -1)
        {
            return _mapper.Map<List<SabitMetinOutputDTO>>(await SabitMetinListesi(dilId, model.KayitGrubu, model.MetinTipi));
        }
    }
}