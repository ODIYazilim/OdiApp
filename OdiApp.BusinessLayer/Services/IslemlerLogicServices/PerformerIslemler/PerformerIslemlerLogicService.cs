using OdiApp.DataAccessLayer.IslemlerDataServices.CallbackIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OpsiyonIslemler;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.PerformerIslemler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerIslemler
{
    public class PerformerIslemlerLogicService : IPerforlerIslemlerLogicService
    {
        IOdiIslemDataService _odiIslemDataService;
        IOpsiyonDataService _opsiyonDataService;
        ICallbackDataService _callbackDataService;
        public PerformerIslemlerLogicService(IOdiIslemDataService odiIslemDataService, IOpsiyonDataService opsiyonDataService, ICallbackDataService callbackDataService)
        {
            _odiIslemDataService = odiIslemDataService;
            _opsiyonDataService = opsiyonDataService;
            _callbackDataService = callbackDataService;
        }
        //performer için  için Detaylı proje bilgileri ile rol odi bilgilerini nin olduğu listesyi  getirir.
        public async Task<OdiResponse<List<PerformerIslemDTO>>> PerformerIslemListesi(PerformerIdDTO performerId)
        {
            List<PerformerIslemDTO> performerIslemList = new List<PerformerIslemDTO>();

            List<OdiTalepPerformerIslemOutputDTO> odiTalepList = await _odiIslemDataService.OdiTalepListesiGetirByPerformer(performerId.PerformerId);

            if (odiTalepList.Count == 0) return OdiResponse<List<PerformerIslemDTO>>.Success("Performer İşlem listesi getirildi", null, 200);

            foreach (OdiTalepPerformerIslemOutputDTO odiTalep in odiTalepList)
            {
                PerformerIslemDTO islem = new PerformerIslemDTO();
                int count = performerIslemList.Where(x => x.ProjeId == odiTalep.ProjeId).ToList().Count;
                if (count == 0)
                {
                    islem.PerformerId = performerId.PerformerId;
                    islem.ProjeId = odiTalep.ProjeId;
                    islem.OdiTalep = true;


                    List<OdiTalepPerformerIslemOutputDTO> projeOdiTalepList = odiTalepList.Where(x => x.ProjeId == odiTalep.ProjeId).ToList();//Bu projeye ait rolleri çekme için, bu projeid ye ait projeleri listeledik

                    List<RolOdiBilgisiDTO> rolodibilgisilist = new List<RolOdiBilgisiDTO>();
                    List<RolOpsiyonBilgisiDTO> rolOpsiyonBilgisiList = new List<RolOpsiyonBilgisiDTO>();
                    foreach (var otalep in projeOdiTalepList)
                    {
                        RolOdiBilgisiDTO rolodibilgisi = new RolOdiBilgisiDTO();
                        rolodibilgisi.ProjeRolId = otalep.ProjeRolId;
                        rolodibilgisi.OdiTalepId = otalep.OdiTalepId;
                        rolodibilgisi.MenajerOnerisi = otalep.MenajerOnerisi;
                        rolodibilgisilist.Add(rolodibilgisi);

                        RolOpsiyonBilgisiDTO ops = await _opsiyonDataService.RolOpsiyonBilgisiGetir(otalep.ProjeRolId);
                        if (ops != null) rolOpsiyonBilgisiList.Add(ops);

                    }
                    islem.RolOdiBilgileri = rolodibilgisilist;
                    islem.RolOpsiyonBilgileri = rolOpsiyonBilgisiList;
                    if (islem.RolOpsiyonBilgileri.Count > 0) islem.Opsiyon = true;

                    islem.Callback = await _callbackDataService.CallbackGonderilmismi(performerId.PerformerId, islem.ProjeId);
                    performerIslemList.Add(islem);
                }
            }
            return OdiResponse<List<PerformerIslemDTO>>.Success("performer işlemleri getirildi", performerIslemList, 200);
        }

        //Menajer için Detaylı proje bilgilgileri ile rol odi bilgilerini getirir.
        public async Task<OdiResponse<List<PerformerIslemDTO>>> MenajerProjeIslem(MenajerIslemInputDTO input)
        {
            List<PerformerIslemDTO> performerIslemList = new List<PerformerIslemDTO>();
            List<OdiTalep> odiTalepList = await _odiIslemDataService.OdiTalepListesiGetirByProjePerformer(input.PerformerId, input.ProjeId);

            foreach (OdiTalep odiTalep in odiTalepList)
            {
                PerformerIslemDTO islem = new PerformerIslemDTO();


                islem.PerformerId = input.PerformerId;
                islem.ProjeId = odiTalep.ProjeId;
                islem.OdiTalep = true;

                List<RolOdiBilgisiDTO> rolodibilgisilist = new List<RolOdiBilgisiDTO>();
                foreach (var otalep in odiTalepList)
                {
                    RolOdiBilgisiDTO rolodibilgisi = new RolOdiBilgisiDTO();
                    rolodibilgisi.ProjeRolId = otalep.ProjeRolId;
                    rolodibilgisi.OdiTalepId = otalep.Id;
                    rolodibilgisilist.Add(rolodibilgisi);


                    islem.RolOdiBilgileri = rolodibilgisilist;
                    performerIslemList.Add(islem);
                }
            }

            return OdiResponse<List<PerformerIslemDTO>>.Success("Menajer İşlem listesi getirildi", performerIslemList, 200);
        }
    }
}
