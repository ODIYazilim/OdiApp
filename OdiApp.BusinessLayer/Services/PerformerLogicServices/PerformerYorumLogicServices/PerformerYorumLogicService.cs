using AutoMapper;
using OdiApp.EntityLayer.Base;
using Odi.Shared.Services.Interface;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerYorumDataServices;
using OdiApp.DTOs.PerformerDTOs.PerformerYorumDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.SharedModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerYorumLogicServices;

public class PerformerYorumLogicService : IPerformerYorumLogicService
{
    private readonly IAmazonS3Service _amazonS3Service;
    private readonly IPerformerYorumDataService _performerYorumDataService;
    private readonly IMapper _mapper;
    private readonly IKullaniciBasicDataService _kullaniciBasicDataService;

    public PerformerYorumLogicService(IAmazonS3Service amazonS3Service, IPerformerYorumDataService performerYorumDataService, IMapper mapper, IKullaniciBasicDataService kullaniciBasicDataService)
    {
        _amazonS3Service = amazonS3Service;
        _performerYorumDataService = performerYorumDataService;
        _mapper = mapper;
        _kullaniciBasicDataService = kullaniciBasicDataService;
    }

    public async Task<OdiResponse<PerformerYorumOutputDTO>> PerformerYorumEkle(PerformerYorumCreateDTO model, OdiUser user)
    {
        PerformerYorum entity = _mapper.Map<PerformerYorum>(model);

        DateTime date = DateTime.Now;

        entity.EklenmeTarihi = date;
        entity.Ekleyen = user.AdSoyad;
        entity.EkleyenId = user.Id;

        entity.GuncellenmeTarihi = date;
        entity.Guncelleyen = user.AdSoyad;
        entity.GuncelleyenId = user.Id;

        entity = await _performerYorumDataService.YeniPerformerYorum(entity);

        return OdiResponse<PerformerYorumOutputDTO>.Success("Yeni yorum oluşturuldu.", await PerformerYorumGetir(entity), 200);
    }

    public async Task<OdiResponse<NoContent>> PerformerYorumSil(PerformerYorumIdDTO model)
    {
        PerformerYorum etiket = await _performerYorumDataService.PerformerYorumGetirById(model.PerformerYorumId);

        if (etiket == null) return OdiResponse<NoContent>.Fail("Bu id ile kayıt bulunamadı.", "Not Found", 404);

        await _performerYorumDataService.PerformerYorumSil(etiket);

        return OdiResponse<NoContent>.Success("Yorum silindi.", null, 200);
    }

    public async Task<OdiResponse<List<PerformerYorumOutputDTO>>> PerformerYorumListesiGetir(string performerId)
    {
        List<PerformerYorum> list = await _performerYorumDataService.PerformerYorumListesiGetir(performerId);

        List<PerformerYorumOutputDTO> outputList = new List<PerformerYorumOutputDTO>();

        foreach (var item in list)
        {
            outputList.Add(await PerformerYorumGetir(item));
        }

        return OdiResponse<List<PerformerYorumOutputDTO>>.Success("Yorum listesi getirildi.", outputList, 200);
    }

    private async Task<PerformerYorumOutputDTO> PerformerYorumGetir(PerformerYorum yorum)
    {
        PerformerYorumOutputDTO dto = _mapper.Map<PerformerYorumOutputDTO>(yorum);

        KullaniciBasic yorumYapan = await _kullaniciBasicDataService.KullaniciGetir(yorum.YorumYapanId);

        if (yorumYapan != null)
        {
            dto.YorumYapanAdSoyad = yorumYapan.KullaniciAdSoyad;
            dto.YorumYapanProfilFotografi = string.IsNullOrEmpty(dto.YorumYapanProfilFotografi) ? "" : _amazonS3Service.GetPreSignedUrl(dto.YorumYapanProfilFotografi);
            dto.YorumYapanKayitTuruListesi = yorumYapan.KayitTuruKodu.Split(',').ToList();
        }

        return dto;
    }
}