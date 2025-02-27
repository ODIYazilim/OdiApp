using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MailLogicServices;
using OdiApp.DTOs.SharedDTOs.MailDTOs.TicketMails;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailBildirimController : ControllerBase
    {
        private readonly ITicketMailLogicService _ticketMailLogicService;
        public MailBildirimController(ITicketMailLogicService ticketMailLogicService)
        {
            _ticketMailLogicService = ticketMailLogicService;
        }
        [HttpPost("ticket-yanitlandi-mail-gonder")]
        public async Task<IActionResult> TicketMailLogicService(TicketYanitlandiMailDTO mail)
        {
            return Ok(_ticketMailLogicService.TicketYanitlandiMail(mail));
        }

        [HttpPost("ticket-kapatildi-mail-gonder")]
        public async Task<IActionResult> TicketKapatildiMailGonder(TicketKapatildiMailDTO mail)
        {
            return Ok(_ticketMailLogicService.TicketKapandiMail(mail));
        }
    }
}