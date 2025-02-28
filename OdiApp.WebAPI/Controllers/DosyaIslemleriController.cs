using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/dosya-islemleri")]
    [ApiController]
    public class DosyaIslemleriController : ControllerBase
    {
        private readonly IAmazonS3Service _amazonS3Service;
        public DosyaIslemleriController(IAmazonS3Service amazonS3Service)
        {
            _amazonS3Service = amazonS3Service;
        }

        [HttpPost("upload-presigned-url")]
        public async Task<IActionResult> DosyaYuklemeLinkiAl(Dosya dosya)
        {

            string path = _amazonS3Service.GetFolderPath(dosya);
            string url = _amazonS3Service.GetUploadPreSignedUrl(path);
            DosyaResponse resp = new DosyaResponse { preSignedUrl = url, DosyaYolu = path };
            return Ok(OdiResponse<DosyaResponse>.Success("Presigned URL Oluşturuldu. Url geçerliliği  12 saattir.", resp, 200));
        }

        [HttpPost("upload-presigned-url-list")]
        public async Task<IActionResult> DosyaYuklemeLinkListesiAl(List<Dosya> dosyalar)
        {
            List<DosyaResponse> respList = new List<DosyaResponse>();
            foreach (var dosya in dosyalar)
            {
                string path = _amazonS3Service.GetFolderPath(dosya);
                string url = _amazonS3Service.GetUploadPreSignedUrl(path);
                DosyaResponse resp = new DosyaResponse { preSignedUrl = url, DosyaYolu = path };
                respList.Add(resp);
            }

            return Ok(OdiResponse<List<DosyaResponse>>.Success("Presigned URL Oluşturuldu. Url geçerliliği  12 saattir.", respList, 200));
        }
    }
}