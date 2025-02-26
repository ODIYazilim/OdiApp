using AutoMapper;
using Odi.Shared.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.SetCard;
using OdiApp.DTOs.PerformerDTOs.SetCard;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.SetCard;

public class SetCardLogicService : ISetCardLogicService
{
    private readonly IMapper _mapper;
    private readonly ISetCardDataService _callbackDataService;
    private readonly IAmazonS3Service _amazonS3Service;

    public SetCardLogicService(IMapper mapper, ISetCardDataService callbackDataService, IAmazonS3Service amazonS3Service)
    {
        _mapper = mapper;
        _callbackDataService = callbackDataService;
        _amazonS3Service = amazonS3Service;
    }

    public async Task<OdiResponse<List<SetCardOutputDTO>>> SetCardGetir(List<KullaniciIdDTO> idList, int dilId)
    {
        List<SetCardOutputDTO> result = new List<SetCardOutputDTO>();

        foreach (var id in idList)
        {
            SetCardOutputDTO dto = new SetCardOutputDTO();

            dto.FizikselOzellikList = await _callbackDataService.FizikselOzellikleriGetir(id.KullaniciId, dilId);
            dto.KisiselOzellikList = await _callbackDataService.KisiselOzellikleriGetir(id.KullaniciId, dilId);
            dto.KisiselBilgiler = await _callbackDataService.KisiselBilgilerGetir(id.KullaniciId, dilId);
            dto.EgitimBilgileriList = await _callbackDataService.EgitimBilgileriGetir(id.KullaniciId, dilId);
            dto.YetenekBilgileriList = await _callbackDataService.YetenekBilgileriGetir(id.KullaniciId, dilId);

            List<SetCardDeneyimBilgileriDTO> deneyimBilgileriList = await _callbackDataService.DeneyimBilgileriGetir(id.KullaniciId, dilId);

            dto.DeneyimBilgileriList = deneyimBilgileriList
                                        .GroupBy(d => new { d.CVDeneyimId, d.DeneyimAdi })
                                        .Select(g => new SetCardDeneyimBilgileriGroupedDTO
                                        {
                                            DeneyimAdi = g.Key.DeneyimAdi,
                                            CVDeneyimId = g.Key.CVDeneyimId,
                                            Ozellikler = g.Select(d => new SetCardDeneyimBilgileriGroupedOzellikDTO
                                            {
                                                DeneyimOzellikAdi = d.DeneyimOzellikAdi,
                                                Deger = d.Deger
                                            }).ToList()
                                        })
                                        .ToList();

            List<SetCardAlbumVeFotografDTO> albumVeFotograflar = await _callbackDataService.AlbumVeFotograflariGetir(id.KullaniciId, dilId);

            dto.AlbumList = albumVeFotograflar
            .GroupBy(a => new { a.AlbumId, a.AlbumAdi, a.AlbumTipiId, a.AlbumTipiLabel })
            .Select(g => new SetCardAlbumDTO
            {
                AlbumId = g.Key.AlbumId,
                AlbumAdi = g.Key.AlbumAdi,
                AlbumTipiId = g.Key.AlbumTipiId,
                AlbumTipiLabel = g.Key.AlbumTipiLabel,
                FotografList = g.Select(f => new SetCardAlbumFotograf
                {
                    FotoId = f.FotoId,
                    FotoAdi = f.FotoAdi,
                    FotoUrl = !string.IsNullOrEmpty(f.FotoDosyaYolu) ? _amazonS3Service.GetPreSignedUrl(f.FotoDosyaYolu) : string.Empty,
                    FotoDosyaYolu = f.FotoDosyaYolu,
                    Sira = f.Sira,
                    Yatay = f.Yatay
                }).ToList()
            }).ToList();

            result.Add(dto);
        }

        return OdiResponse<List<SetCardOutputDTO>>.Success("Set card bilgileri getirildi.", result, 200);
    }
}