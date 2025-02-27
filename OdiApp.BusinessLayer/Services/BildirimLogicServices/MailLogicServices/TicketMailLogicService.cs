using Microsoft.AspNetCore.Hosting;
using OdiApp.BusinessLayer.Core;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.MailDTOs.TicketMails;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MailLogicServices
{
    public class TicketMailLogicService : ITicketMailLogicService
    {
        private IWebHostEnvironment _environment;
        public TicketMailLogicService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public OdiResponse<bool> TicketYanitlandiMail(TicketYanitlandiMailDTO mail)
        {
            string body = Fonksiyonlar.HtmlToString(_environment.ContentRootPath + "/HTMLMails/TicketYanitlandiMail.html");
            body = body.Replace("{AdSoyad}", mail.AdSoyad);
            body = body.Replace("{Konu}", mail.TicketKonusu);
            body = body.Replace("{Mesaj}", mail.TicketMesaji);
            body = body.Replace("{Tarih}", mail.TicketYanitTarihi.ToString("dd MMMM yyyy HH:mm"));

            if (body != string.Empty)
            {
                try
                {
                    Fonksiyonlar.MailSender(body, "ODIAPP Destek Talebiniz Yanıtlandı", mail.MailTo);
                    return OdiResponse<bool>.Success("Mail Gönderildi", true, 200);
                }
                catch (Exception ex)
                {

                    return OdiResponse<bool>.Fail("Mail Gönderilemedi", ex.Message, 400);
                }

            }
            else
            {
                return OdiResponse<bool>.Fail("Mail Gönderilemedi", "HTML Body alınamadı", 400);

            }
        }

        public OdiResponse<bool> TicketKapandiMail(TicketKapatildiMailDTO mail)
        {
            string body = Fonksiyonlar.HtmlToString(_environment.ContentRootPath + "/HTMLMails/TicketKapandi.html");
            body = body.Replace("{AdSoyad}", mail.AdSoyad);

            if (body != string.Empty)
            {
                try
                {
                    Fonksiyonlar.MailSender(body, "ODIAPP Destek Talebiniz Kapandı", mail.MailTo);
                    return OdiResponse<bool>.Success("Mail Gönderildi", true, 200);
                }
                catch (Exception ex)
                {

                    return OdiResponse<bool>.Fail("Mail Gönderilemedi", ex.Message, 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Mail Gönderilemedi", "HTML Body alınamadı", 400);

            }
        }
    }
}