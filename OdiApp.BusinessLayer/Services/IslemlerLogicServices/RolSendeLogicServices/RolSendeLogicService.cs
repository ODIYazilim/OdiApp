using AutoMapper;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DataAccessLayer.IslemlerDataServices.RolSendeDataServices;
using OdiApp.DTOs.IslemlerDTOs.RolSendeDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;
using OdiApp.EntityLayer.IslemlerModels.RolSendeModels;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.RolSendeLogicServices
{
    public class RolSendeLogicService : IRolSendeLogicService
    {
        private readonly IMapper _mapper;
        private readonly IRolSendeDataService _rolSendeDataService;
        private readonly IConfiguration _configuration;
        private readonly IUseOtherService _useOtherService;

        public RolSendeLogicService(IMapper mapper, IRolSendeDataService rolSendeDataService, IConfiguration configuration, IUseOtherService useOtherService)
        {
            _mapper = mapper;
            _rolSendeDataService = rolSendeDataService;
            _configuration = configuration;
            _useOtherService = useOtherService;
        }

        public async Task<OdiResponse<NoContent>> YeniRolSende(RolSendeCreateDTO model, OdiUser user, string jwtToken)
        {
            RolSende rolSende = _mapper.Map<RolSende>(model);

            DateTime date = DateTime.Now;

            rolSende.EklenmeTarihi = date;
            rolSende.Ekleyen = user.AdSoyad;
            rolSende.EkleyenId = user.Id;

            rolSende.GuncellenmeTarihi = date;
            rolSende.Guncelleyen = user.AdSoyad;
            rolSende.GuncelleyenId = user.Id;

            await _rolSendeDataService.YeniRolSende(rolSende);

            ProjeRolPerformerCreateDTO projeRolPerformerCreateDTO = new ProjeRolPerformerCreateDTO();

            projeRolPerformerCreateDTO.ProjeId = model.ProjeId;
            projeRolPerformerCreateDTO.RolId = model.RolId;
            projeRolPerformerCreateDTO.PerformerId = model.PerformerId;
            projeRolPerformerCreateDTO.MenajerId = model.MenajerId;

            var dynamicData = await _useOtherService.PostMethod(projeRolPerformerCreateDTO, _configuration.GetSection("ProjeServerUrl").Value + "/yeni-proje-rol-performer", jwtToken);

            return OdiResponse<NoContent>.Success("Yeni rol sende oluşturuldu.", 200);
        }
    }
}