﻿using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.DataAccessLayer.BildirimDataServices.KullaniciBasicDataServices
{
    public class KullaniciBasicDataService : IKullaniciBasicDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public KullaniciBasicDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KullaniciBasic> KullaniciEkle(KullaniciBasic kullaniciBasic)
        {
            await _dbContext.KullaniciBasic.AddAsync(kullaniciBasic);
            await _dbContext.SaveChangesAsync();

            return kullaniciBasic;
        }

        public async Task<KullaniciBasic> KullaniciGetir(string kullaniciId)
        {
            return await _dbContext.KullaniciBasic.AsNoTracking().FirstOrDefaultAsync(f => f.KullaniciId == kullaniciId);
        }

        public async Task<List<KullaniciBasic>> KullaniciListesiGetir(List<string> kullaniciId)
        {
            return await _dbContext.KullaniciBasic.AsNoTracking().Where(f => kullaniciId.Contains(f.KullaniciId)).ToListAsync();
        }

        public async Task<List<KullaniciBasic>> KullaniciListesiGetirByKayitGrubu(string kayitGrubu)
        {
            return await _dbContext.KullaniciBasic.AsNoTracking().Where(f => f.KayitGrubuKodu.Contains(kayitGrubu)).ToListAsync();
        }

        public async Task<KullaniciBasic> KullaniciGuncelle(KullaniciBasic kullaniciBasic)
        {
            _dbContext.KullaniciBasic.Update(kullaniciBasic);
            await _dbContext.SaveChangesAsync();

            return kullaniciBasic;
        }
    }
}