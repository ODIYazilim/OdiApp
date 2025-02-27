using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MutluCellSmsLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.BildirimDTOs.SMSBildirimDTOs;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/bildirim")]
    [ApiController]
    public class SMSBildirimController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IMutluCellSmsLogicService _mutluCellSmsLogicService;

        public SMSBildirimController(IPushNotificationService pushNotificationService, IMutluCellSmsLogicService mutluCellSmsLogicService)
        {
            _pushNotificationService = pushNotificationService;
            _mutluCellSmsLogicService = mutluCellSmsLogicService;
        }

        [HttpPost("sms-gonder")]
        public async Task<IActionResult> SMSGonder(SMSDto sms)
        {
            OdiResponse<MutluCellSmsAyarlari> mutluCellSmsAyarlari = await _mutluCellSmsLogicService.AyarlariGetir();
            MutluCellService smsServ = new MutluCellService(mutluCellSmsAyarlari.Data.KullaniciAdi, mutluCellSmsAyarlari.Data.Parola, mutluCellSmsAyarlari.Data.OrganizasyonAdi, DateTime.Now);
            List<string> numbers = new List<string>();
            numbers.Add(sms.TelefonNumarasi);
            smsServ.AddSms(sms.Mesaj, numbers.ToArray());
            string result = await smsServ.SendAsync(); // sms gönderildi
            SMSResultDTO smsResult = ResultToSMSResult(result);
            if (smsResult.Durum) return Ok(OdiResponse<NoContent>.Success("", 200));
            else return BadRequest(OdiResponse<SMSResultDTO>.Fail("", result, 400));
        }

        private SMSResultDTO ResultToSMSResult(string result)
        {
            SMSResultDTO resp = new SMSResultDTO();
            result = result.Trim();
            char firstChar = result[0];
            if (firstChar == '$')
            {
                resp.Durum = true;
                resp.DonenKod = result;
                resp.Hata = "";
                resp.HataKodu = "";
            }
            else
            {
                resp.Durum = false;
                resp.HataKodu = result;
                int Val = 0;
                if (int.TryParse(result, out Val))
                    resp.Hata = ErrorCode(Convert.ToInt32(result));
                else
                    resp.Hata = "Tanımsız Hata";
            }

            return resp;
        }

        private string ErrorCode(int code)
        {
            string error = "";

            switch (code)
            {
                case 20:
                    error = "Post edilen xml eksik veya hatalı.";
                    break;
                case 21:
                    error = "Kullanılan originatöre sahip değilsiniz";
                    break;
                case 22:
                    error = "Kontörünüz yetersiz";
                    break;
                case 23:
                    error = "Kullanıcı adı ya da parolanız hatalı.";
                    break;
                case 24:
                    error = "Şu anda size ait başka bir işlem aktif.";
                    break;
                case 25:
                    error = "SMSC Stopped (Bu hatayı alırsanız, işlemi 1-2 dk sonra tekrar deneyin)";
                    break;
                case 30:
                    error = "Hesap Aktivasyonu sağlanmamış";
                    break;
                default:
                    error = "Tanımsız Hata";
                    break;
            }

            return error;
        }
    }
}