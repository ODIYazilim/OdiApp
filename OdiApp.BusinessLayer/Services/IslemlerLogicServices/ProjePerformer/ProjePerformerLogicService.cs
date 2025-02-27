using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DTOs.IslemlerDTOs.ProjePerformer;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.ProjePerformer
{
    public class ProjePerformerLogicService : IProjePerformerLogicService
    {
        private readonly IUseOtherService _useOtherService;
        private readonly IConfiguration _configuration;

        private readonly IAmazonS3Service _amazonS3Service;

        public ProjePerformerLogicService(IUseOtherService useOtherService, IConfiguration configuration, IAmazonS3Service amazonS3Service)
        {
            _useOtherService = useOtherService;
            _configuration = configuration;

            _amazonS3Service = amazonS3Service;
        }

        public async Task<OdiResponse<List<PerformerDisplayInfoDTO>>> ProjeOnerilenPerformerList(string jwtToken)
        {
            string url = _configuration.GetSection("PerformerServerUrl").Value + "/proje-onerilen-performer-list";
            var dynamicData = await _useOtherService.GetMethod(url, jwtToken);
            string jsonString = JsonConvert.SerializeObject(dynamicData);
            List<PerformerDisplayInfoDTO> list = JsonConvert.DeserializeObject<List<PerformerDisplayInfoDTO>>(jsonString);

            foreach (var item in list)
            {
                item.ProfilResmiDosyaYolu = item.ProfilResmi;
                item.ProfilResmi = _amazonS3Service.GetPreSignedUrl(item.ProfilResmi);

                if (item.MenajerProfilResmiDosyaYolu != null)
                {
                    item.MenajerProfilResmiDosyaYolu = item.MenajerProfilResmi;
                    item.MenajerProfilResmi = _amazonS3Service.GetPreSignedUrl(item.MenajerProfilResmi);
                }
            }

            return OdiResponse<List<PerformerDisplayInfoDTO>>.Success("Önerilen Oyunucular Getirildi", list, 200);
        }
    }
}