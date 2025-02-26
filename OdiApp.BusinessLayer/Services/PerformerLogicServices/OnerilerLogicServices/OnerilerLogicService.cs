using AutoMapper;
using OdiApp.DataAccessLayer.PerformerDataServices.OnerilerDataServices;
using OdiApp.DTOs.PerformerDTOs.OnerilerDTOs;
using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.OnerilerModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.OnerilerLogicServices;

public class OnerilerLogicService : IOnerilerLogicService
{
    private readonly IOnerilerDataService _onerilerDataService;
    private readonly IMapper _mapper;

    public OnerilerLogicService(IOnerilerDataService onerilerDataService, IMapper mapper)
    {
        _onerilerDataService = onerilerDataService;
        _mapper = mapper;
    }

    public async Task<OdiResponse<NoContent>> OneriTalepEt(OneriTalepEtDTO model, OdiUser user)
    {
        OneriTalepleri oneriTalep = _mapper.Map<OneriTalepleri>(model);

        DateTime date = DateTime.Now;

        oneriTalep.TalepTarihi = date;

        oneriTalep.EklenmeTarihi = date;
        oneriTalep.Ekleyen = user.AdSoyad;
        oneriTalep.EkleyenId = user.Id;

        oneriTalep.GuncellenmeTarihi = date;
        oneriTalep.Guncelleyen = user.AdSoyad;
        oneriTalep.GuncelleyenId = user.Id;

        await _onerilerDataService.YeniOneriTalep(oneriTalep);

        return OdiResponse<NoContent>.Success("Talep gönderildi.", 200);
    }

    public async Task<OdiResponse<NoContent>> OneriGonder(OneriGonderDTO model, OdiUser user)
    {
        List<MenajerPerformerOnerileri> menajerPerformerOnerileriList = new List<MenajerPerformerOnerileri>();

        DateTime date = DateTime.Now;

        foreach (var performerId in model.PerformerIdListesi)
        {
            MenajerPerformerOnerileri menajerPerformerOnerileri = new MenajerPerformerOnerileri();

            menajerPerformerOnerileri.ProjeId = model.ProjeId;
            menajerPerformerOnerileri.RolId = model.RolId;
            menajerPerformerOnerileri.MenajerId = model.MenajerId;
            menajerPerformerOnerileri.OneriTarihi = date;

            menajerPerformerOnerileri.PerformerId = performerId;

            menajerPerformerOnerileri.EklenmeTarihi = date;
            menajerPerformerOnerileri.Ekleyen = user.AdSoyad;
            menajerPerformerOnerileri.EkleyenId = user.Id;

            menajerPerformerOnerileri.GuncellenmeTarihi = date;
            menajerPerformerOnerileri.Guncelleyen = user.AdSoyad;
            menajerPerformerOnerileri.GuncelleyenId = user.Id;

            menajerPerformerOnerileriList.Add(menajerPerformerOnerileri);
        }

        await _onerilerDataService.YeniMenajerPerformerOneri(menajerPerformerOnerileriList);

        return OdiResponse<NoContent>.Success("Öneriler gönderildi.", 200);
    }

    public async Task<OdiResponse<List<PerformerDisplayInfoDTO>>> MenajerPerformerOneriListele(MenajerPerformerOneriListeleInputDTO model)
    {
        List<PerformerDisplayInfoDTO> list = await _onerilerDataService.MenajerPerformerOneriListesiGetir(model.ProjeId, model.MenajerId);
        return OdiResponse<List<PerformerDisplayInfoDTO>>.Success("Performer önerileri getirildi", list, 200);
    }
}