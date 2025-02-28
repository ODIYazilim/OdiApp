using OdiApp.DTOs.UygulamaBilgileriDTOs.SabitMetinDTOs;
using OdiApp.EntityLayer.UygulamaBilgileriModels;

namespace OdiApp.DataAccessLayer.UygulamaBilgileriDataServices.SabitMetinDataServices
{
    public interface ISabitMetinService
    {
        public Task<List<SabitMetin>> SabitMetinEkle(SabitMetin model);
        public Task<List<SabitMetin>> SabitMetinGuncelle(SabitMetin model);
        public Task<List<SabitMetin>> SabitMetinListesi(int dilId = -1, string kayitGrubu = "", int metinTipi = -1);
        public Task<List<SabitMetinOutputDTO>> SabitMetinOutputListesi(SabitMetinListeInputDTO model, int dilId = -1);
    }
}