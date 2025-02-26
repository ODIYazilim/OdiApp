using OdiApp.DTOs.PerformerDTOs.SetCard;

namespace OdiApp.DataAccessLayer.PerformerDataServices.SetCard;

public interface ISetCardDataService
{
    Task<SetCardKisiselBilgilerDTO> KisiselBilgilerGetir(string kullaniciId, int dilId);
    Task<List<SetCardFizikselOzellikDTO>> FizikselOzellikleriGetir(string kullaniciId, int dilId);
    Task<List<SetCardKisiselOzellikDTO>> KisiselOzellikleriGetir(string kullaniciId, int dilId);
    Task<List<SetCardEgitimBilgileriDTO>> EgitimBilgileriGetir(string kullaniciId, int dilId);
    Task<List<SetCardYetenekBilgileriDTO>> YetenekBilgileriGetir(string kullaniciId, int dilId);
    Task<List<SetCardDeneyimBilgileriDTO>> DeneyimBilgileriGetir(string kullaniciId, int dilId);
    Task<List<SetCardAlbumVeFotografDTO>> AlbumVeFotograflariGetir(string kullaniciId, int dilId);
}