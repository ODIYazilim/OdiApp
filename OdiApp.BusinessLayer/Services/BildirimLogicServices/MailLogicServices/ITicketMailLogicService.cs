using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.MailDTOs.TicketMails;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.MailLogicServices
{
    public interface ITicketMailLogicService
    {
        OdiResponse<bool> TicketYanitlandiMail(TicketYanitlandiMailDTO mail);
        OdiResponse<bool> TicketKapandiMail(TicketKapatildiMailDTO mail);
    }
}
